using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickFrame.Security.Common.Interfaces;
using QuickFrame.Data.Common.Models;
using System.Security.Claims;
using System.IO;

namespace QuickFrame.Security.ActiveDirectory.Models
{
	public class SiteClaim : DataModel<SiteClaim>, ISiteClaim {
		public byte[] Claim { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder) {
			var builder = modelBuilder.Entity<SiteClaim>();
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id).IsRequired().UseSqlServerIdentityColumn();
			builder.Property(a => a.Claim).IsRequired();
		}

		public static implicit operator SiteClaim(Claim value) {
			using(var ms = new MemoryStream()) {
				using(BinaryWriter writer = new BinaryWriter(ms)) {
					value.WriteTo(writer);
					writer.Flush();
					ms.Position = 0;
					return new SiteClaim {
						Claim = ms.ToArray()
					};
				}
			}
		}

		public static implicit operator Claim(SiteClaim value) {
			using(var ms = new MemoryStream(value.Claim)) {
				using(BinaryReader reader = new BinaryReader(ms)) {
					return new Claim(reader);
				}
			}
		}
	}
}
