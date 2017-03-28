using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickFrame.Security.Common.Interfaces;
using QuickFrame.Data.Common.Interfaces;
using QuickFrame.Security.Common.Interfaces.Models;
using QuickFrame.Data.Common;
using Microsoft.Extensions.Configuration;
using QuickFrame.Security.ActiveDirectory.Models;
using QuickFrame.Security.ActiveDirectory.Interfaces;

namespace QuickFrame.Security
{
	public class SecurityContext : TrackingContext, IAdSecurityContext {
		public DbSet<ISiteClaim> SiteClaims { get; set; }

		public DbSet<ISiteUser> SiteUsers { get; set; }
		public DbSet<UserClaim> UserClaims { get; set; }
		public SecurityContext(IConfigurationRoot configuration) 
			: base(configuration) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			new SiteClaim().OnModelCreating(modelBuilder);
			
		}
	}
}
