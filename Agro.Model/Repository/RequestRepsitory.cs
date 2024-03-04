using Agro.Model.Entities;
using Agro.Model.Enums;
using Microsoft.EntityFrameworkCore;
using PlatF.Model.Data;
using PlatF.Model.Interfaces;
using PlatF.Model.PaginatedList;
using System.Linq;
using System.Threading.Tasks;

namespace PlatF.Model.Repository
{
    public class RequestRepsitory : AsyncRepository<Request>, IRequestRepository
    {
        public RequestRepsitory(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Request> GetAll()
        {
            return _dbContext.Requests.Include(r => r.User);
        }

        public async Task<PaginatedList<Request>> GetAllRequestsPagedAsync(int? p)
        {
            var t = _dbContext.Requests.Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Request>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);
        }

        public async Task<Request> GetRequestById(int id)
        {
            return  await _dbContext.Requests.AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.City)
                .Include(r=>r.Intentions)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p)
        {
            var t = _dbContext.Requests.Where(x => x.RequestStatus == status).Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Request>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);
        }
    }
}
