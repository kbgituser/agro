using Agro.Logic.Interfaces;
using Agro.Model.Dto.Request;
using Agro.Model.Entities;
using Agro.Model.Enums;
using Agro.Model.Interfaces;
using Agro.Model.PaginatedList;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class RequestService : IRequestService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILogger<RequestService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestService(IUnitOfWork unitOfWork, ILogger<RequestService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<RequestDto>> GetAllAsync()
        {
            return _mapper.Map<List<RequestDto>> 
                (await _unitOfWork.RequestRepository.GetAllAsync());
        }

        public async Task<PaginatedList<RequestDto>> GetAllPagedAsync(int? p, int? pageSize = 10)
        {
            var result = await _unitOfWork.RequestRepository.GetAllRequestsPagedAsync(p);
            List<RequestDto> rDtoList = _mapper.Map<List<RequestDto>>(result.ToList());
            return await PaginatedList<RequestDto>.CreateAsync(
                rDtoList.AsQueryable(),
                p.Value,
                pageSize.Value);
        }

        public async Task<PaginatedList<RequestDto>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p)
        {
            return _mapper.Map<PaginatedList<RequestDto>>
                (await _unitOfWork.RequestRepository.GetRequestsByStatusPagedAsync(status, p));
        }

        public async Task<RequestDto> GetRequestByIdAsync(int id)
        {
            return _mapper.Map<RequestDto>(await _unitOfWork.RequestRepository.GetRequestById(id));
        }

        public bool Release(int id)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _unitOfWork.RequestRepository.Release(id, userId);
        }

        public async Task<bool> IsUsersRequest(int requestId, string usersId)
        {
            var request = await _unitOfWork.IntentionRepository.GetIntentionById(requestId);
            return request.UserId == usersId;
        }

        public async Task CreateAsync(RequestDto request)
        {
            var creatingRequest = _mapper.Map<Request>(request);
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            creatingRequest.UserId = userId;
            _unitOfWork.RequestRepository.Add(creatingRequest);

            await _unitOfWork.Commit();
        }
        public async Task Update(RequestDto requestDto)
        {
            //var request = _mapper.Map<Request>(requestDto);
            var req = (await _unitOfWork.RequestRepository.GetRequestById(requestDto.Id));
            req.Name = requestDto.Name;
            req.CityId = requestDto.CityId;
            req.Status = requestDto.Status;
            //var intentions = _unitOfWork.IntentionRepository.GetAll()
            //    .Where(i => req.Intentions
            //    .Select(x=>x.Id).Contains(i.Id)).ToList();
            //req.Intentions = requestDto.Intentions;
            
            _unitOfWork.RequestRepository.Update(req);
            await _unitOfWork.Commit();
        }

        public async Task UpdateStatus(int id, RequestStatus status)
        {
            var req = (await _unitOfWork.RequestRepository.GetRequestById(id));
            req.Status = status;
            _unitOfWork.RequestRepository.Update(req);
            await _unitOfWork.Commit();
        }

        public async Task DeleteById(int id)
        {
            _unitOfWork.RequestRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }

        public async void GroupIntentionsToRequestAsync()
        {
            _unitOfWork.RequestRepository.GroupIntentionsToRequest();
            await _unitOfWork.Commit();
        }

        public async Task AssignToPerformerAsync(int id)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _unitOfWork.RequestRepository.AssignToPerformer(id, userId);
            await _unitOfWork.Commit();
        }

        public async Task ReleaseAsync(int id)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _unitOfWork.RequestRepository.Release(id, userId);
            await _unitOfWork.Commit();
        }
    }
}
