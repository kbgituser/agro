using Agro.Model.Dto.Advertisement;
using Agro.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IAdvertisementService
    {
        Task Create(AdvertisementDto advertisementDto);
        Task DeleteById(int id);
        Task<List<AdvertisementDto>> GetAllAsync();
        Task<AdvertisementDto> GetByIdAsync(int id);
        Task<PaginatedList<AdvertisementDto>> GetCategoriesPagedAsync(int? p);
        Task<int> Update(AdvertisementDto advertisementDto);
    }
}