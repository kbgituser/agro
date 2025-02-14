using Agro.Model.Dto.Intention;
using Agro.Model.Entities;
using Agro.Model.Enums;
using Agro.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces;

public interface IIntentionService
{
    Task CreateAsync(IntentionDto request);
    Task DeleteById(int id);
    Task<List<IntentionDto>> GetAllAsync();
    Task<PaginatedList<IntentionDto>> GetAllPagedAsync(int? p, int? pageSize = 10);
    Task<IntentionDto> GetIntentionByIdAsync(int id);
    Task<PaginatedList<Intention>> GetRequestsByStatusPagedAsync(IntentionStatus status, int? p);
    Task<bool> IsUsersRequest(int requestId, string usersId);
    Task Update(IntentionDto requestDto);
    Task UpdateStatus(int id, IntentionStatus status);
}