using QuickFrame.Data.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickFrame.Data.Common.Interfaces.Dtos;
using Microsoft.EntityFrameworkCore;
using ExpressMapper;
using QuickFrame.Data.Common.Interfaces.Models;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace QuickFrame.Data.Common.Services
{
	public class DataServiceBase<TContext, TModelType> : IDataServiceBase<TModelType> 
		where TContext : DbContext
		where TModelType : class, IDataModelCore {
		protected TContext _dbContext;

		public void Create(TModelType model) {
			_dbContext.Set<TModelType>().Add(model);
			_dbContext.Entry(model).State = EntityState.Added;
			_dbContext.SaveChanges();
		}

		public void Create<TModel>(TModel model) where TModel : IDataTransferObjectCore {
			Create(Mapper.Map<TModel, TModelType>(model));
		}

		public int GetCount(string searchColumn, int searchTerm, bool? isDeleted = false) {
			var query = _dbContext.Set<TModelType>().Where($"{searchColumn} == @0", searchTerm);
			if(isDeleted != null)
				query = query.IsDeleted((bool)isDeleted);
			return query.Count();
		}

		public int GetCount(string searchColumn = "", string searchTerm = "", bool? isDeleted = false) {
			var query = _dbContext.Set<TModelType>().AsQueryable();
			if(!string.IsNullOrEmpty(searchTerm))
				query = _dbContext.Set<TModelType>().Where($"{searchColumn}.Contains(@0)", searchTerm);
			if(isDeleted != null)
				if(typeof(IDataModelDeletable).IsAssignableFrom(typeof(TModelType)))
					query = query.IsDeleted((bool)isDeleted);
			return query.Count();
		}

		public IEnumerable<TModelType> GetList(bool? isDeleted = false) =>
			GetList(0, 0, string.Empty, SortOrder.Ascending, string.Empty, string.Empty, isDeleted);

		public IEnumerable<TModelType> GetList(int? searchTerm, string searchColumn, bool? isDeleted = false) =>
			GetList(0, 0, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted);

		public IEnumerable<TModelType> GetList(string sortColumn, SortOrder sortOrder, bool? isDeleted = false) =>
			GetList(0, 0, sortColumn, sortOrder, string.Empty, string.Empty, isDeleted);

		public IEnumerable<TModelType> GetList(string searchTerm, string searchColumn, bool? isDeleted = false) =>
			GetList(0, 0, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted);

		public IEnumerable<TModelType> GetList(int page, int itemsPerPage, bool? isDeleted = false) =>
			GetList(page, itemsPerPage, string.Empty, SortOrder.Ascending, string.Empty, string.Empty, isDeleted);

		public IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool? isDeleted = false) {
			var query = _dbContext.Set<TModelType>().AsQueryable();

			if(typeof(IDataModelDeletable).IsAssignableFrom(typeof(TModelType)))
				if(isDeleted != null)
					query = query.IsDeleted((bool)isDeleted);

			if(!string.IsNullOrEmpty(sortColumn)) {
				if(sortOrder == SortOrder.Descending)
					query = query.OrderByDescending(sortColumn);
				else
					query = query.OrderBy(sortColumn);

				if(itemsPerPage > 0) {
					if(page > 1)
						query = query.Skip((page - 1) * itemsPerPage);
					query = query.Take(itemsPerPage);
				}
			}

			return query.AsNoTracking();
		}

		public IEnumerable<TModelType> GetList(int page, int itemsPerPage, int? searchTerm, string searchColumn, bool? isDeleted = false)
			 =>	GetList(page, itemsPerPage, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted);

		public IEnumerable<TModelType> GetList(int page, int itemsPerPage, string searchTerm, string searchColumn, bool? isDeleted = false) 
			=> GetList(page, itemsPerPage, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted);

		public IEnumerable<TModelType> GetList(string searchTerm, int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool includeDeleted) {
			var query = default(IQueryable<TModelType>);

			if(string.IsNullOrEmpty(searchTerm))
				query = _dbContext.Set<TModelType>();
			else
				query = _dbContext.Set<TModelType>().Where($"{sortColumn}.Contains(@0)", searchTerm);

			if(!includeDeleted && typeof(IDataModelDeletable).IsAssignableFrom(typeof(TModelType)))
				query = query.IsDeleted(false);

			if(!string.IsNullOrEmpty(sortColumn)) {
				if(sortOrder == SortOrder.Descending)
					query = query.OrderByDescending(sortColumn);
				else
					query = query.OrderBy(sortColumn);
			}

			if(itemsPerPage > 0) {
				if(page > 1)
					query = query.Skip((page - 1) * itemsPerPage);
				query = query.Take(itemsPerPage);
			}

			return query.AsNoTracking();
		}

		public IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, int? searchTerm, string searchColumn, bool? isDeleted = false) {
			var query = GetList(page, itemsPerPage, sortColumn, sortOrder, isDeleted);

			if(string.IsNullOrEmpty(searchColumn))
				searchColumn = "Id";
			if(searchTerm == null)
				query = _dbContext.Set<TModelType>().Where($"{searchColumn} == NULL", searchTerm);
			else
				query = _dbContext.Set<TModelType>().Where($"{searchColumn} == (@0)", searchTerm);

			return query;
		}

		public IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, string searchTerm, string searchColumn, bool? isDeleted = false)
			=> GetList(page, itemsPerPage, sortColumn, sortOrder, searchTerm, searchColumn, isDeleted, true);

		public IEnumerable<TModelType> GetList(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, string searchTerm, string searchColumn, bool? isDeleted = false, bool matchPartial = true) {
			var query = GetList(page, itemsPerPage, sortColumn, sortOrder, isDeleted);

			if(!string.IsNullOrEmpty(searchTerm)) {
				if(string.IsNullOrEmpty(searchColumn))
					searchColumn = "Name";
				if(!searchColumn.Contains(",")) {
					query = _dbContext.Set<TModelType>().Where($"{searchColumn}.Contains(@0)", searchTerm);
				} else {
					var columns = searchColumn.Split(',');
					foreach(var column in columns) {
						query = _dbContext.Set<TModelType>().Where($"{column}.Contains(@0)", searchTerm);
					}
				}
			}

			return query;
		}

		public IEnumerable<TResult> GetList<TResult>(bool? isDeleted = false) 
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(0, 0, string.Empty, SortOrder.Ascending, string.Empty, string.Empty, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(string sortColumn, SortOrder sortOrder, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(0, 0, sortColumn, sortOrder, string.Empty, string.Empty, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(string searchTerm, string searchColumn, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(0, 0, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(int? searchTerm, string searchColumn, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(0, 0, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(page, itemsPerPage, string.Empty, SortOrder.Ascending, string.Empty, string.Empty, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(page, itemsPerPage, sortColumn, sortOrder, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, int? searchTerm, string searchColumn, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(page, itemsPerPage, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string searchTerm, string searchColumn, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(page, itemsPerPage, string.Empty, SortOrder.Ascending, searchTerm, searchColumn, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(string searchTerm, int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, bool isDeleted)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(searchTerm, page, itemsPerPage, sortColumn, sortOrder, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, int? searchTerm, string searchColumn, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(page, itemsPerPage, sortColumn, sortOrder, isDeleted));

		public IEnumerable<TResult> GetList<TResult>(int page, int itemsPerPage, string sortColumn, SortOrder sortOrder, string searchTerm, string searchColumn, bool? isDeleted = false)
			where TResult : IDataTransferObjectCore
			=> Mapper.Map<IEnumerable<TModelType>, IEnumerable<TResult>>(GetList(page, itemsPerPage, sortColumn, sortOrder, searchTerm, searchColumn, isDeleted));

		public void Save(TModelType model) {
			_dbContext.Set<TModelType>().Attach(model);
			_dbContext.Entry(model).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public void Save<TModel>(TModel model) 
			where TModel : IDataTransferObjectCore
			=> Save(Mapper.Map<TModel, TModelType>(model));
	}
}
