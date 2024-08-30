using Agro.Model.Dto.City;
using Agro.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ICityService
    {
        Task Create(CityDto cityDto);
        Task Delete(CityDto cityDto);
        Task DeleteById(int id);
        Task<List<CityDto>> GetAllAsync();
        Task<CityDto> GetByIdAsync(int id);
        Task<PaginatedList<CityDto>> GetAllPagedAsync(int? p);
        Task<int> Update(CityDto cityDto);
    }
}