using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFrame.Data.Common.Interfaces.Models;

namespace QuickFrame.Data.Common.Models
{
	public abstract class DataModelCore<TModelType> : IDataModelCore where TModelType : class {
		public abstract void OnModelCreating(ModelBuilder modelBuilder);
	}
}
