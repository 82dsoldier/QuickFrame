using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuickFrame.Data.Common.Interfaces;
using QuickFrame.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Data.Common
{
    public class TrackingContext : DbContext, ITrackingContext
    {
		protected IConfigurationRoot _configuration;
		protected string _connectionString;
		public bool TrackChanges { get; set; } = false;
		public bool Notify { get; set; } = false;
		public DbSet<AuditLog> AuditLogs { get; set; }
		public TrackingContext(IConfigurationRoot configuration)
			:base() {
			_configuration = configuration;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			new AuditLog().OnModelCreating(modelBuilder);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlServer(_configuration.GetConnectionString(_connectionString));
		}
	}
}
