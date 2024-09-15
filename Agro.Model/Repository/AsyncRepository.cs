using Microsoft.EntityFrameworkCore;
using Agro.Model.Data;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using Agro.Model.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Agro.Model.Repository
{
	public class AsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity
	{

		protected ApplicationDbContext _dbContext;
		

        public AsyncRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public void Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
		}

		public void DeleteById(int id)
		{
			var delete = _dbContext.Set<T>().FirstOrDefault(x=>x.Id == id);
			_dbContext.Set<T>().Remove(delete);
		}

		public void Update(T entity)
		{
			_dbContext.Set<T>().Update(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			_dbContext.Set<T>().RemoveRange(entities);
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}


		public async Task<PaginatedList<T>> GetAllPagedAsync(int? page, int pageSize)
		{
			var t = _dbContext.Set<T>();
			pageSize = ((pageSize==0) ? 10: pageSize);
			int pN = (page ?? 1);
			return await PaginatedList<T>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);
	}

		public async Task<List<T>> GetAllUnDeletedAsync()
		{
			return await _dbContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
		}

		public async Task<List<T>> GetAsync(Func<T, bool> criteria)
		{
			return await _dbContext.Set<T>().Where(t => criteria(t)).ToListAsync();
		}

		public async Task<T> GetFirstAsync(Func<T, bool> criteria)
		{
			return await _dbContext.Set<T>().FirstOrDefaultAsync(t => criteria(t));
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<bool> AnyAsync(Func<T, bool> criteria)
		{
			return await _dbContext.Set<T>().AnyAsync(t => criteria(t));
		}

		public async Task<int> CountAsync(Func<T, bool> criteria)
		{
			return await _dbContext.Set<T>().CountAsync(t => criteria(t));
		}
        
    }

}
