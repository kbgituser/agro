using Microsoft.EntityFrameworkCore;
using PlatF.Model.Data;
using PlatF.Model.Dto.Request;
using PlatF.Model.Entities;
using PlatF.Model.Enums;
using PlatF.Model.Interfaces;
using PlatF.Model.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Repository
{
    public class RequestRepository: AsyncRepository<Request>, IRequestRepository
    {
        public RequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Request> GetAll()
        {
            return _dbContext.Requests.Include(c=>c.Offers);
        }

        public async Task<PaginatedList<Request>> GetAllRequestsPagedAsync(int? p)
        {
            var t = _dbContext.Requests.Include(r => r.Category).Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Request>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);            
        }

        public async Task<Request> GetRequestById(int id)
        {
            return  await _dbContext.Requests.AsNoTracking()
                .Include(r => r.Category)
                .Include(r => r.User)
                .Include(r => r.City)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p)
        {
            var t = _dbContext.Requests.Where(x => x.RequestStatus == status).Include(r => r.Category).Include(r => r.User).Include(x => x.City);
            int pageSize = 10;
            int pN = (p ?? 1);
            return await PaginatedList<Request>.CreateAsync(t.OrderByDescending(x => x.EntryDate).AsNoTracking(), pN, pageSize);
        }




        public Task<Request> GetRequestsByStatusPagedAsync(RequestStatus status, int id)
        {
            throw new NotImplementedException();
        }
    }
}
