using AutoMapper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlatF.Model.Dto.Request;
using PlatF.Model.Entities;
using PlatF.Model.Enums;
using PlatF.Model.Interfaces;
using PlatF.Model.PaginatedList;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class IntentionService : IIntentionService
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILogger<IntentionService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IntentionService(IUnitOfWork unitOfWork, ILogger<IntentionService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Intention>> GetAllAsync()
        {
            return await _unitOfWork.IntentionRepository.GetAllAsync();
        }

        public async Task<PaginatedList<IntentionDto>> GetAllPagedAsync(int? p, int? pageSize=10)
        {
            var result = await _unitOfWork.IntentionRepository.GetAllRequestsPagedAsync(p);
            List<IntentionDto> rDtoList = _mapper.Map<List<IntentionDto>>(result.ToList());
            //rDtoList.AsQueryable().AsAsyncEnumerable();
            return await PaginatedList<IntentionDto>.CreateAsync(
                rDtoList.OrderBy(t => t.StartDate).AsQueryable(), 
                p.Value, 
                pageSize.Value);
        }

        public async Task<PaginatedList<Intention>> GetRequestsByStatusPagedAsync(IntentionStatus status, int? p)
        {
            return await _unitOfWork.IntentionRepository.GetIntentionsByStatusPagedAsync(status, p);
        }

        public async Task<Intention> GetRequestByIdAsync(int id)
        {
            return await _unitOfWork.IntentionRepository.GetIntentionById(id);
        }

        public async Task<bool> IsUsersRequest(int requestId, string usersId)
        {
            var request = await _unitOfWork.IntentionRepository.GetIntentionById(requestId);
            return request.UserId == usersId;
        }

        public async Task Create(IntentionDto request)
        {
            var creatingRequest = _mapper.Map<Intention>(request);
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            creatingRequest.UserId = userId;
            _unitOfWork.IntentionRepository.Add(creatingRequest);

            await _unitOfWork.Commit();
        }
        public async Task Update(IntentionDto requestDto)
        {
            //var request = _mapper.Map<Request>(requestDto);
            var req = (await _unitOfWork.IntentionRepository.GetIntentionById(requestDto.Id));
            req.Name = requestDto.Name;
            req.CityId = requestDto.CityId;
            req.Message = requestDto.Message;
            req.IntentionStatus = requestDto.IntentionStatus;
            _unitOfWork.IntentionRepository.Update(req);
            await _unitOfWork.Commit();
        }

        public async Task UpdateStatus(int id, IntentionStatus status)
        {
            var req = (await _unitOfWork.IntentionRepository.GetIntentionById(id));
            req.IntentionStatus = status;
            _unitOfWork.IntentionRepository.Update(req);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Category category)
        {
            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.Commit();
        }

        public async Task DeleteById(int id)
        {
            _unitOfWork.CategoryRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }
    }
}
