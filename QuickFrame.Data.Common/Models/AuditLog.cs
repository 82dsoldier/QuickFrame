using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuickFrame.Data.Common.Models
{
    public class AuditLog : DataModelLong {

		///<summary>Gets or sets the unique identifier for the user requesting the changes.</summary>
		public string UserId { get; set; }

		///<summary>Gets or sets the date and time when the changes were requested.</summary>
		public DateTime EventDate { get; set; }

		///<summary>Gets or sets an integer value determining the type of change.  Generated from the EntityState enumeration.</summary>
		public int EventType { get; set; }

		///<summary>Gets or sets the name of the table for which the changes are being made.</summary>
		public string TableName { get; set; }

		///<summary>Gets or sets a unique identifier for the record being changed.</summary>
		public string RecordId { get; set; }

		///<summary>Gets or sets the name of the column being changed.</summary>
		public string ColumnName { get; set; }

		///<summary>Gets or sets the original value (if any) of the column being changed.  Non-string values are serialized as json objects before saving.</summary>
		public string OriginalValue { get; set; }

		///<summary>Gets or sets the new value (if any) to be used in the column.  Non-string values are serialized as json objects before saving.</summary>
		public string NewValue { get; set; }

		public override void OnModelCreating(ModelBuilder modelBuilder) {
			var builder = modelBuilder.Entity<AuditLog>();
			builder.Property(a => a.UserId).IsRequired().HasMaxLength(128);
			builder.Property(a => a.EventDate).IsRequired();
			builder.Property(a => a.EventType).IsRequired();
			builder.Property(a => a.TableName).IsRequired();
		}
	}
}
