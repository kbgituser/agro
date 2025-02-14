using Agro.Model.Dto.Request;
using Agro.Model.Enums;
using Agro.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agro.Logic.Interfaces
{
    public interface IRequestService
    {
        Task CreateAsync(RequestDto request);
        Task DeleteById(int id);
        Task<List<RequestDto>> GetAllAsync();
        Task<PaginatedList<RequestDto>> GetAllRequestDtoPagedAsync(int? p, int? pageSize = 10);
        Task<RequestDto> GetRequestByIdAsync(int id);
        Task<PaginatedList<RequestDto>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p);
        Task<bool> IsUsersRequest(int requestId, string usersId);
        Task Update(RequestDto requestDto);
        Task UpdateStatus(int id, RequestStatus status);
        Task ReleaseAsync(int id);
        Task AssignToPerformerAsync(int id);

    }
}