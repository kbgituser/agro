using Agro.Model.Entities;
using Agro.Model.Enums;
using Agro.Model.PaginatedList;
using System.Linq;
using System.Threading.Tasks;

namespace Agro.Model.Interfaces
{
    public interface IIntentionRepository: IAsyncRepository<Intention>
    {
        IQueryable<Intention> GetAll();
        Task<PaginatedList<Intention>> GetAllIntentionsPagedAsync(int? p);
        Task<Intention> GetIntentionById(int id);
        Task<PaginatedList<Intention>> GetIntentionByUserId(string id, int? p);
        Task<PaginatedList<Intention>> GetIntentionsByStatusPagedAsync(IntentionStatus status, int? id);
    }
}