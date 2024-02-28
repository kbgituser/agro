using AutoMapper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlatF.Logic.Helpers;
using PlatF.Model.Dto.Request;
using PlatF.Model.Entities;
using PlatF.Model.Enums;
using PlatF.Model.Interfaces;
using PlatF.Model.PaginatedList;
using PlatF.Model.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PaginatedList<RequestDto>> GetAllPagedAsync(int? p, int? pageSize=10)
        {
            var result = await _unitOfWork.RequestRepository.GetAllRequestsPagedAsync(p);
            List<RequestDto> rDtoList = _mapper.Map<List<RequestDto>>(result.ToList());
            //rDtoList.AsQueryable().AsAsyncEnumerable();
            return await PaginatedList<RequestDto>.CreateAsync(
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
            var request = await _unitOfWork.RequestRepository.GetRequestById(requestId);
            return request.UserId == usersId;
        }






        public async Task Create(RequestDto request)
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
            req.Title = requestDto.Title;
            req.CityId = requestDto.CityId;
            req.Price = requestDto.Price;
            req.Message = requestDto.Message;
            req.StartDate = requestDto.StartDate;
            req.EndDate = requestDto.EndDate;
            req.RequestStatus = requestDto.RequestStatus;
            _unitOfWork.RequestRepository.Update(req);
            await _unitOfWork.Commit();
        }

        public async Task UpdateStatus(int id, RequestStatus status)
        {
            var req = (await _unitOfWork.RequestRepository.GetRequestById(id));
            req.RequestStatus = status;
            _unitOfWork.RequestRepository.Update(req);
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
