using PlatF.Model.Entities;
using PlatF.Model.Enums;
using PlatF.Model.PaginatedList;
using System.Linq;
using System.Threading.Tasks;

namespace PlatF.Model.Interfaces
{
    public interface IIntentionRepository: IAsyncRepository<Intention>
    {
        IQueryable<Intention> GetAll();
        Task<PaginatedList<Intention>> GetAllRequestsPagedAsync(int? p);
        Task<Intention> GetIntentionById(int id);
        Task<PaginatedList<Intention>> GetIntentionsByStatusPagedAsync(IntentionStatus status, int? id);
        
    }
}