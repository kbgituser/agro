﻿using Agro.Logic.Interfaces;
using Agro.Model.Entities;
using Agro.Model.Enums;
using AutoMapper;
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

        public async Task<List<Request>> GetAllAsync()
        {
            return await _unitOfWork.RequestRepository.GetAllAsync();
        }

        public async Task<PaginatedList<IntentionDto>> GetAllPagedAsync(int? p, int? pageSize = 10)
        {
            var result = await _unitOfWork.IntentionRepository.GetAllRequestsPagedAsync(p);
            List<IntentionDto> rDtoList = _mapper.Map<List<IntentionDto>>(result.ToList());
            //rDtoList.AsQueryable().AsAsyncEnumerable();
            return await PaginatedList<IntentionDto>.CreateAsync(
                rDtoList.OrderBy(t => t.StartDate).AsQueryable(),
                p.Value,
                pageSize.Value);
        }

        public async Task<PaginatedList<Request>> GetRequestsByStatusPagedAsync(RequestStatus status, int? p)
        {
            return await _unitOfWork.RequestRepository.GetRequestsByStatusPagedAsync(status, p);
        }

        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await _unitOfWork.RequestRepository.GetRequestById(id);
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

        public async Task UpdateStatus(int id, RequestStatus status)
        {
            var req = (await _unitOfWork.RequestRepository.GetRequestById(id));
            req.RequestStatus = status;
            _unitOfWork.RequestRepository.Update(req);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Request request)
        {
            _unitOfWork.RequestRepository.Delete(request);
            await _unitOfWork.Commit();
        }

        public async Task DeleteById(int id)
        {
            _unitOfWork.CategoryRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }
    }
}
