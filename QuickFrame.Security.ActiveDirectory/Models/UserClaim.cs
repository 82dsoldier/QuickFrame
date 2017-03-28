using QuickFrame.Security.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuickFrame.Security.ActiveDirectory.Models
{
    public class UserClaim : IUserClaim<byte[]>
    {
		public byte[] UserId { get; set; }
		public int ClaimId { get; set; }

		public void OnModelCreating(ModelBuilder modelBuilder) {
			throw new NotImplementedException();
		}
	}
}
