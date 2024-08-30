using Agro.Model.Entities;
using Agro.Model.Enums;
using Agro.Model.PaginatedList;
using System.Linq;
using System.Threading.Tasks;

namespace Agro.Model.Interfaces
{
    public interface IRequestRepository : IAsyncRepository<Request>
    {
        bool AssignToPerformer(int id, string userId);
        IQueryable<Request> GetAll();
        Task<PaginatedList<Request>> GetAllRequestsPagedAsync(int? p);
        Task<Request> GetRequestById(int id);
        Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? id);
        void GroupIntentionsToRequest();
        bool Release(int id, string userId);
    }
}