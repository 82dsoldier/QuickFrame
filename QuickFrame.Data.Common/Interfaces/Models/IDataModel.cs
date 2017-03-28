using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Data.Common.Interfaces.Models
{
    public interface IDataModel<TIdType> : IDataModelCore, IDataModelDeletable
    {
		TIdType Id { get; set; }
    }

	public interface IDataModel : IDataModelInt {

	}
}
