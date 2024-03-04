using Microsoft.EntityFrameworkCore;
using PlatF.Model.Data;
using PlatF.Model.Entities;
using PlatF.Model.Interfaces;
using PlatF.Model.PaginatedList;
using System.Linq;
using System.Threading.Tasks;

namespace PlatF.Model.Repository
{
    public class IntentionRepository: AsyncRepository<Intention>, IIntentionRepository
    {
        public IntentionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Intention> GetAll()
        {
            return _dbContext.Intentions.Include(r => r.User);
        }

        public async Task<PaginatedList<Intention>> GetAllRequestsPagedAsync(int? p)
        {
            var t = _dbContext.Intentions.Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Intention>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);            
        }

        public async Task<Intention> GetIntentionById(int id)
        {
            return  await _dbContext.Intentions.AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.City)
                .Include(r=>r.Request)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<PaginatedList<Intention>> GetIntentionsByStatusPagedAsync(Enums.IntentionStatus status, int? p)
        {
            var t = _dbContext.Intentions.Where(x => x.IntentionStatus == status).Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Intention>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);
        }
    }
}
