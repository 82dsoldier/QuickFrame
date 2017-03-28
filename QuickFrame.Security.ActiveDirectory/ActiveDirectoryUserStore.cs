using Microsoft.AspNetCore.Identity;
using QuickFrame.Security.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Options;
using QuickFrame.Data.Configuration;
using System.Security.Claims;
using QuickFrame.Security.Common.Interfaces.Models;
using QuickFrame.Data.Common.Interfaces;
using System.IO;
using QuickFrame.Security.ActiveDirectory.Models;
using System.Collections;
using System.Security.Principal;

namespace QuickFrame.Security.ActiveDirectory
{
	public class ActiveDirectoryUserStore : IUserStore<ISiteUser>, IUserClaimStore<ISiteUser> {
		private DataOptions _dataOptions;
		private SecurityContext _dbContext;
		public ActiveDirectoryUserStore(IOptions<DataOptions> dataOptionsAccessor, SecurityContext dbContext) {
			_dataOptions = dataOptionsAccessor.Value;
			_dbContext = dbContext;
		}

		public Task AddClaimsAsync(ISiteUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
			foreach(var claim in claims) {
				SiteClaim sc = claim;
				SiteClaim dbClaim = _dbContext.SiteClaims.Where(a => a.Claim == sc.Claim) as SiteClaim;
				if(dbClaim == null) {
					dbClaim = new SiteClaim {
						Claim = sc.Claim
					};
					_dbContext.SiteClaims.Add(dbClaim);
					_dbContext.SaveChanges();
				}
				_dbContext.UserClaims.Add(new UserClaim {
					ClaimId = dbClaim.Id,
					UserId = user.Sid
				});
				_dbContext.SaveChanges();
			}
			return Task.CompletedTask;
		}

		public Task<IdentityResult> CreateAsync(ISiteUser user, CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}

		public Task<IdentityResult> DeleteAsync(ISiteUser user, CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}

		public void Dispose() {
			
		}

		public Task<ISiteUser> FindByIdAsync(string userId, CancellationToken cancellationToken) {
			ISiteUser user = DirectoryEntry.FindById(_dataOptions.ConnectionString.AdSecurity, new System.Security.Principal.SecurityIdentifier(userId));
			return Task.FromResult(user);
		}

		public Task<ISiteUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
			ISiteUser user = DirectoryEntry.FindByName(_dataOptions.ConnectionString.AdSecurity, normalizedUserName);
			return Task.FromResult(user);
		}

		public Task<IList<Claim>> GetClaimsAsync(ISiteUser user, CancellationToken cancellationToken) {
			var claimList = new List<Claim>();
			foreach(var dbClaim in _dbContext.UserClaims.Where(c => c.UserId == user.Sid)) {
				claimList.Add(_dbContext.SiteClaims.First(c => c.Id == dbClaim.ClaimId) as SiteClaim);
			}
			return Task.FromResult((IList<Claim>)claimList);
		}

		public Task<string> GetNormalizedUserNameAsync(ISiteUser user, CancellationToken cancellationToken) {
			return Task.FromResult(user.UserName?.ToUpper());
		}

		public Task<string> GetUserIdAsync(ISiteUser user, CancellationToken cancellationToken) {
			return Task.Run(async () => {
				if(user.Sid == null || user.Sid.Length == 0) {
					var currentUser = await FindByNameAsync(await GetNormalizedUserNameAsync(user, new CancellationToken()), new CancellationToken());
					return new SecurityIdentifier(currentUser.Sid, 0).ToString();
				}

				return new SecurityIdentifier(user.Sid, 0).ToString();
			});
		}

		public Task<string> GetUserNameAsync(ISiteUser user, CancellationToken cancellationToken) {
			return Task.Run(async () => {
				if(String.IsNullOrEmpty(user.UserName)) {
					var currentUser = await FindByIdAsync(await GetNormalizedUserNameAsync(user, new CancellationToken()), new CancellationToken());
					return currentUser.UserName;
				}

				return user.UserName;
			});
		}

		public Task<IList<ISiteUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken) {
			return Task.Run(async () => {
				SiteClaim sc = (SiteClaim)claim;
				var userList = new List<ISiteUser>();
				var claimList = (from c in _dbContext.UserClaims
								 where c.ClaimId == (from cl in _dbContext.SiteClaims
													 where cl.Claim == sc.Claim
													 select cl.Id).First()
								 select c);
				foreach(var obj in claimList) {
					userList.Add(await FindByIdAsync(new SecurityIdentifier(obj.UserId, 0).ToString(), new CancellationToken()));
				}
				return userList as IList<ISiteUser>;
			});
		}

		public Task RemoveClaimsAsync(ISiteUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}

		public Task ReplaceClaimAsync(ISiteUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}

		public Task SetNormalizedUserNameAsync(ISiteUser user, string normalizedName, CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}

		public Task SetUserNameAsync(ISiteUser user, string userName, CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}

		public Task<IdentityResult> UpdateAsync(ISiteUser user, CancellationToken cancellationToken) {
			throw new NotImplementedException();
		}
	}
}
