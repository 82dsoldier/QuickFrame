using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickFrame.Data.Common.Interfaces.Models;

namespace QuickFrame.Data.Common.Models
{
	public abstract class DataModel<TModelType, TIdType> : DataModelCore<TModelType>, IDataModel<TIdType>
		where TModelType : class, IDataModel<TIdType> {
		public TIdType Id { get; set; }

		public bool IsDeleted { get; set; }
	}

	public abstract class DataModel<TModelType> : DataModelInt<TModelType>, IDataModel 
		where TModelType : class, IDataModel {

	}
}
