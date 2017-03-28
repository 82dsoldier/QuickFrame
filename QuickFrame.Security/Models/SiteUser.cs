using QuickFrame.Security.Common.Interfaces;
using QuickFrame.Security.Common.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickFrame.Data.Common.Models;

namespace QuickFrame.Security.Models
{
    public class SiteUser : DataModel<SiteUser>, ISiteUser
    {
		public byte[] Sid { get; set; }
		public string DisplayName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string UserName { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder) {
			var builder = modelBuilder.Entity<SiteUser>();
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id).IsRequired().UseSqlServerIdentityColumn();
			builder.Property(a => a.Sid).IsRequired();
			builder.Property(a => a.DisplayName).IsRequired().HasMaxLength(512);
			builder.Property(a => a.FirstName).HasMaxLength(256);
			builder.Property(a => a.LastName).HasMaxLength(256);
			builder.Property(a => a.Email).HasMaxLength(256);
			builder.Property(a => a.Phone).HasMaxLength(256);
			builder.Property(a => a.UserName).HasMaxLength(256);
		}
	}
}
