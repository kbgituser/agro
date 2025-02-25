using System.Collections.Generic;
using System.Threading.Tasks;
using Agro.Model.Dto.City;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using Agro.Model.PaginatedList;
using AutoMapper;
using Logic.Interfaces;
using Microsoft.Extensions.Logging;

namespace Logic.Services;

public class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CityService> _logger;
    private readonly IMapper _mapper;

    public CityService(IUnitOfWork unitOfWork, ILogger<CityService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<CityDto>> GetAllAsync()
    {
        return _mapper.Map<List<CityDto>>(await _unitOfWork.CityRepository.GetAllAsync());
    }

    public async Task<PaginatedList<CityDto>> GetAllPagedAsync(int? p)
    {
        var t = await _unitOfWork.CityRepository.GetAllPagedAsync(p);
        //var citydtopl = t.Select(x => _mapper.Map<CityDto>(x));
        //citydtopl
        //PaginatedList<CityDto> cityDtos = t.Select(x=>_mapper.Map<CityDto>(x));
        var t2 = _mapper.Map<PaginatedList<CityDto>>(t);
        return t2;
        //return _mapper.Map<PaginatedList<CityDto>>
        //    (await _unitOfWork.CityRepository.GetAllPagedAsync(p));
    }

    public async Task Create(CityDto cityDto)
    {
        var city = _mapper.Map<City>(cityDto);
        _unitOfWork.CityRepository.Add(city);
        await _unitOfWork.Commit();
    }

    public async Task<int> Update(CityDto cityDto)
    {
        var editedCity = _mapper.Map<City>(cityDto);
        var existingCity = await _unitOfWork.CityRepository.GetByIdAsync(cityDto.Id);
        if (existingCity != null)
        {
            existingCity.Code = editedCity.Code;
            existingCity.Name = editedCity.Name;
        }
        _unitOfWork.CityRepository.Update(existingCity);
        await _unitOfWork.Commit();
        return existingCity.Id;
    }

    public async Task Delete(CityDto cityDto)
    {
        var city = _mapper.Map<City>(cityDto);
        _unitOfWork.CityRepository.Delete(city);
        await _unitOfWork.Commit();
    }

    public async Task DeleteById(int id)
    {
        _unitOfWork.CityRepository.DeleteById(id);
        await _unitOfWork.Commit();
    }

    public async Task<CityDto> GetByIdAsync(int id)
    {
        return _mapper.Map<CityDto>(await _unitOfWork.CityRepository.GetByIdAsync(id));
    }
}
