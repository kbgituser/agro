using Agro.Model.Entities;
using Agro.Model.Enums;
using PlatF.Model.Dto.Request;
using PlatF.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agro.Logic.Interfaces
{
    public interface IRequestService
    {
        Task Create(IntentionDto request);
        Task Delete(Request request);
        Task DeleteById(int id);
        Task<List<Request>> GetAllAsync();
        Task<PaginatedList<IntentionDto>> GetAllPagedAsync(int? p, int? pageSize = 10);
        Task<Request> GetRequestByIdAsync(int id);
        Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p);
        Task<bool> IsUsersRequest(int requestId, string usersId);
        Task Update(IntentionDto requestDto);
        Task UpdateStatus(int id, RequestStatus status);
    }
}