using Agro.Model.Dto.Intention;
using Agro.Model.Entities;
using Agro.Model.Enums;
using Agro.Model.Interfaces;
using Agro.Model.PaginatedList;
using AutoMapper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class IntentionService : IIntentionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ILogger<IntentionService> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IntentionService(IUnitOfWork unitOfWork, ILogger<IntentionService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<IntentionDto>> GetAllAsync()
        {
            //return _mapper.Map<List<IntentionDto>>
            //    (await _unitOfWork.IntentionRepository.GetAllAsync());

            //return _mapper.Map<List<IntentionDto>>
            //    (_unitOfWork.IntentionRepository.GetAll());

            var t = _unitOfWork.IntentionRepository.GetAll();
            var t2 = _mapper.Map<List<IntentionDto>>(t);
            return t2;
            
        }

        public async Task<PaginatedList<IntentionDto>> GetAllPagedAsync(int? p=1, int? pageSize=10)
        {
            if (p < 1) { 
                p= 1;
            }
            var result = await _unitOfWork.IntentionRepository.GetAllIntentionsPagedAsync(p);
            //List<IntentionDto> rDtoList = _mapper.Map<List<IntentionDto>>(result.ToList());
            //return await PaginatedList<IntentionDto>.CreateAsync(
            //    rDtoList.AsQueryable(), 
            //    p.Value, 
            //    pageSize.Value);

            var t2 = _mapper.Map<PaginatedList<IntentionDto>>(result);
            return t2;
        }

        public async Task<PaginatedList<Intention>> GetAllowedIntentionsByUserIdAsync(string userId, int? p)
        {
            var isAdmin = await _userService.IsUserAdmin(userId);
            if (isAdmin)
            {
                return await _unitOfWork.IntentionRepository.GetAllPagedAsync(p);
            }

            return await _unitOfWork.IntentionRepository.GetIntentionByUserId(userId, p);
        }

        public async Task<PaginatedList<Intention>> GetRequestsByStatusPagedAsync(IntentionStatus status, int? p)
        {
            return await _unitOfWork.IntentionRepository.GetIntentionsByStatusPagedAsync(status, p);
        }

        public async Task<IntentionDto> GetIntentionByIdAsync(int id)
        {
            var intention = await _unitOfWork.IntentionRepository.GetIntentionById(id);

            return _mapper.Map<IntentionDto>(
                intention
                );
            //return await _unitOfWork.IntentionRepository.GetIntentionById(id);
        }

        public async Task<bool> IsUsersRequest(int requestId, string usersId)
        {
            var request = await _unitOfWork.IntentionRepository.GetIntentionById(requestId);
            return request.UserId == usersId;
        }

        public async Task CreateAsync(IntentionDto intention)
        {
            var creatingIntention = _mapper.Map<Intention>(intention);
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            creatingIntention.UserId = userId;
            creatingIntention.IntentionStatus = IntentionStatus.Active;

            _unitOfWork.IntentionRepository.Add(creatingIntention);
            await _unitOfWork.Commit();
            _unitOfWork.RequestRepository.GroupIntentionsToRequest();
            await _unitOfWork.Commit();
        }
        public async Task Update(IntentionDto requestDto)
        {
            var updatedIntention = (await _unitOfWork.IntentionRepository.GetIntentionById(requestDto.Id));
            updatedIntention.Name = requestDto.Name;
            updatedIntention.CityId = requestDto.CityId;
            updatedIntention.Message = requestDto.Message;
            updatedIntention.IntentionStatus = requestDto.IntentionStatus;
            _unitOfWork.IntentionRepository.Update(updatedIntention);
            await _unitOfWork.Commit();
        }

        public async Task UpdateStatus(int id, IntentionStatus status)
        {
            var updatedIntention = (await _unitOfWork.IntentionRepository.GetIntentionById(id));
            updatedIntention.IntentionStatus = status;
            _unitOfWork.IntentionRepository.Update(updatedIntention);
            await _unitOfWork.Commit();
        }

        public async Task DeleteById(int id)
        {
            _unitOfWork.IntentionRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }
    }
}
