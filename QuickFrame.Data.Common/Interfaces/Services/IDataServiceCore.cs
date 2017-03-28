using QuickFrame.Data.Common.Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Data.Common.Interfaces.Services
{
    public interface IDataServiceCore<TModelType, TIdType> : IDataServiceBase<TModelType>
    {
		void Delete(TIdType id);

		TModelType Get(TIdType id);

		TResult Get<TResult>(TIdType id) where TResult : IDataTransferObjectCore;
	}
}
