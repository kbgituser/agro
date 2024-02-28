using PlatF.Model.Entities;
using PlatF.Model.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Interfaces
{
	public interface IAsyncRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);
		Task<List<T>> GetAllAsync();
		Task<PaginatedList<T>> GetAllPagedAsync(int? id, int page=10);
		Task<List<T>> GetAllUnDeletedAsync();
		Task<List<T>> GetAsync(Func<T, bool> criteria);
		void Add(T entity);
		void Delete(T entity);
		void DeleteById(int id);
		void Update(T entity);
		void DeleteRange(IEnumerable<T> entities);
		Task<T> GetFirstAsync(Func<T, bool> criteria);
		Task<bool> AnyAsync(Func<T, bool> criteria);
		Task<int> CountAsync(Func<T, bool> criteria);
	}
}
