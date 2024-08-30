using Agro.Model.Dto.Category;
using Agro.Model.PaginatedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ICategoryService
    {
        void Create(CategoryDto category);
        Task DeleteById(int id);
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<PaginatedList<CategoryDto>> GetCategoriesPagedAsync(int? p);
        Task<int> Update(CategoryDto category);
    }
}