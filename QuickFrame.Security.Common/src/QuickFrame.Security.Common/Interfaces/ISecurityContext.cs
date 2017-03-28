using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickFrame.Security.Common.Interfaces;


namespace QuickFrame.Data.Common.Interfaces
{
	public interface ISecurityContext {
		DbSet<ISiteUser> SiteUsers { get; set; }
		DbSet<ISiteClaim> SiteClaims { get; set; }
    }
}
