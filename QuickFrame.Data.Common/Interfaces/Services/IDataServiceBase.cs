using QuickFrame.Data.Common.Interfaces.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickFrame.Data.Common.Interfaces.Services
{
	public interface IDataServiceBase<TModelType> {

		void Create(TModelType model);

		void Create<TModel>(TModel model) where TModel : IDataTransferObjectCore;

		int GetCount(string searchColumn = "", string searchTerm = "", bool? isDeleted = false);
		int GetCount(string searchColumn, int searchTerm, bool? isDeleted = false);
		[Obsolete("Use one of the new GetList overrides")]
		IEnumerable<TModelType> GetList(string searchTerm, int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool includeDeleted);
		[Obsolete("Use one of the new GetList overrides")]
		IEnumerable<TResult> GetList<TResult>(string searchTerm, int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool isDeleted) where TResult : IDataTransferObjectCore;
		IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, string searchTerm, string searchColumn, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, string searchTerm, string searchColumn, bool? isDeleted = false, bool matchPartial = true);
		IEnumerable<TModelType> GetList(int page, int itemsPerPage, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(bool? isDeleted = false);
		IEnumerable<TModelType> GetList(string searchTerm, string searchColumn, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(int? searchTerm, string searchColumn, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(int page, int itemsPerPage, string searchTerm, string searchColumn, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(int page, int itemsPerPage, int? searchTerm, string searchColumn, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(string sortColumn, SortOrder sortOrder, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool? isDeleted = false);
		IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, int? searchTerm, string searchColumn, bool? isDeleted = false);
		IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, string searchTerm, string searchColumn, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(string searchTerm, string searchColumn, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(int? searchTerm, string searchColumn, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string searchTerm, string searchColumn, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, int? searchTerm, string searchColumn, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(string sortColumn, SortOrder sortOrder, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, int? searchTerm, string searchColumn, bool? isDeleted = false) where TResult : IDataTransferObjectCore;
		void Save(TModelType model);
		void Save<TModel>(TModel model) where TModel : IDataTransferObjectCore;
	}
}
