using Microsoft.EntityFrameworkCore;
using QuickFrame.Data.Common.Interfaces;
using QuickFrame.Security.ActiveDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Security.ActiveDirectory.Interfaces
{
    public interface IAdSecurityContext : ISecurityContext
    {
		DbSet<UserClaim> UserClaims { get; set; }
    }
}
