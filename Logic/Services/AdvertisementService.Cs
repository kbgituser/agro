using AutoMapper;
using Logic.Interfaces;
using Microsoft.Extensions.Logging;
using Agro.Model.Dto.Advertisement;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using Agro.Model.PaginatedList;

using System.Collections.Generic;

using System.Threading.Tasks;

namespace Logic.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AdvertisementService> _logger;
        private readonly IMapper _mapper;

        public AdvertisementService(IUnitOfWork unitOfWork, ILogger<AdvertisementService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<AdvertisementDto>> GetAllAsync()
        {
            return _mapper
                .Map<List<AdvertisementDto>>
                (await _unitOfWork.AdvertisementRepository.GetAllAsync());

        }

        public async Task<AdvertisementDto> GetByIdAsync(int id)
        {
            return _mapper
                .Map<AdvertisementDto>
                (await _unitOfWork.AdvertisementRepository.GetByIdAsync(id));
        }

        public async Task<PaginatedList<AdvertisementDto>> GetCategoriesPagedAsync(int? p)
        {
            return _mapper.Map<PaginatedList<AdvertisementDto>>
                (await _unitOfWork.AdvertisementRepository.GetAllPagedAsync(p));
        }

        public async Task Create(AdvertisementDto advertisementDto)
        {
            _unitOfWork.AdvertisementRepository.Add(_mapper.Map<Advertisement>(advertisementDto));
            await _unitOfWork.Commit();
        }
        public async Task<int> Update(AdvertisementDto advertisementDto)
        {
            _unitOfWork.AdvertisementRepository.Update(_mapper.Map<Advertisement>(advertisementDto));
            await _unitOfWork.Commit();
            return advertisementDto.Id;
        }
        private async Task Delete(AdvertisementDto advertisementDto)
        {
            _unitOfWork.AdvertisementRepository.Delete(_mapper.Map<Advertisement>(advertisementDto));
            await _unitOfWork.Commit();
        }

        public async Task DeleteById(int id)
        {
            _unitOfWork.AdvertisementRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }
    }
}