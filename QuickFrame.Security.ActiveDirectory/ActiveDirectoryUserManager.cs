using QuickFrame.Security.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using QuickFrame.Security.Common.Interfaces.Models;

namespace QuickFrame.Security.ActiveDirectory
{
    public class ActiveDirectoryUserManager : UserManager<ISiteUser>
    {
		public ActiveDirectoryUserManager(IUserStore<ISiteUser> userStore, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ISiteUser> passwordHasher,
			IEnumerable<IUserValidator<ISiteUser>> userValidators, IEnumerable<IPasswordValidator<ISiteUser>> passwordValidators, ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errorDescriber, IServiceProvider serviceProvider, ILogger<UserManager<ISiteUser>> logger)
			: base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errorDescriber, serviceProvider, logger) {

		}
    }
}
