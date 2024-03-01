using Agro.Model.Entities;
using Agro.Model.Enums;
using PlatF.Model.PaginatedList;
using System.Linq;
using System.Threading.Tasks;

namespace PlatF.Model.Interfaces
{
    public interface IRequestRepository : IAsyncRepository<Request>
    {
        IQueryable<Request> GetAll();
        Task<PaginatedList<Request>> GetAllRequestsPagedAsync(int? p);
        Task<Request> GetRequestById(int id);
        Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? id);
    }
}