using PlatF.Model.Dto.Request;
using PlatF.Model.Entities;
using PlatF.Model.Enums;
using PlatF.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IIntentionService
    {
        Task Create(IntentionDto request);
        Task Delete(Category category);
        Task DeleteById(int id);
        Task<List<Intention>> GetAllAsync();
        Task<PaginatedList<IntentionDto>> GetAllPagedAsync(int? p, int? pageSize = 10);
        Task<Intention> GetRequestByIdAsync(int id);
        Task<PaginatedList<Intention>> GetRequestsByStatusPagedAsync(IntentionStatus status, int? p);
        Task<bool> IsUsersRequest(int requestId, string usersId);
        Task Update(IntentionDto requestDto);
        Task UpdateStatus(int id, IntentionStatus status);
    }
}