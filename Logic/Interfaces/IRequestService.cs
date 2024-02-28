using PlatF.Model.Dto.Request;
using PlatF.Model.Entities;
using PlatF.Model.Enums;
using PlatF.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IRequestService
    {
        Task Create(RequestDto request);
        Task Delete(Category category);
        Task DeleteById(int id);
        Task<List<Request>> GetAllAsync();
        Task<PaginatedList<RequestDto>> GetAllPagedAsync(int? p, int? pageSize = 10);
        Task<Request> GetRequestByIdAsync(int id);
        Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p);
        Task<bool> IsUsersRequest(int requestId, string usersId);
        Task Update(RequestDto requestDto);
        Task UpdateStatus(int id, RequestStatus status);
    }
}