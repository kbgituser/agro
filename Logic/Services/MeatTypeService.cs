using Agro.Model.Dto.City;
using Agro.Model.Dto.MeatType;
using Agro.Model.Entities;
using Agro.Model.Interfaces;
using AutoMapper;
using Logic.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agro.Logic.Services;

public class MeatTypeService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public MeatTypeService(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<List<MeatTypeDto>> GetAllAsync()
  {
    return _mapper.Map<List<MeatTypeDto>>(await _unitOfWork.MeatTypeRepository.GetAllAsync());
  }

  
  public async Task Create(MeatTypeDto meatTypeDto)
  {
    var meatType = _mapper.Map<MeatType>(meatTypeDto);
    
    _unitOfWork.MeatTypeRepository.Add(meatType);
    await _unitOfWork.Commit();
  }

  public async Task<int> Update(MeatTypeDto meatTypeDto)
  {
    var editedCity = _mapper.Map<MeatType>(meatTypeDto);
    var existingMeatType = await _unitOfWork.MeatTypeRepository.GetByIdAsync(meatTypeDto.Id);
    if (existingMeatType != null)
    {
      existingMeatType.Name = editedCity.Name;
    }
    _unitOfWork.MeatTypeRepository.Update(existingMeatType);
    await _unitOfWork.Commit();
    return existingMeatType.Id;
  }

  public async Task DeleteById(int id)
  {
    _unitOfWork.MeatTypeRepository.DeleteById(id);
    await _unitOfWork.Commit();
  }

  public async Task<MeatTypeDto> GetByIdAsync(int id)
  {
    return _mapper.Map<MeatTypeDto>(await _unitOfWork.MeatTypeRepository.GetByIdAsync(id));
  }
}
