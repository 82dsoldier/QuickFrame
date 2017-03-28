using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuickFrame.Data.Common.Interfaces.Models;

namespace QuickFrame.Data.Common.Models
{
    public abstract class DataModelInt<TModelType> : DataModel<TModelType, int>, IDataModelInt
		where TModelType : class, IDataModel {
	}
}
