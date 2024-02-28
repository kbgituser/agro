using AutoMapper;
using Logic.Interfaces;
using Microsoft.Extensions.Logging;
using PlatF.Model.Dto.Category;
using PlatF.Model.Entities;
using PlatF.Model.Interfaces;
using PlatF.Model.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return _mapper
                .Map<List<CategoryDto>>
                (await _unitOfWork.CategoryRepository.GetAllAsync());

        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            return _mapper
                .Map<CategoryDto>
                (await _unitOfWork.CategoryRepository.GetByIdAsync(id));
        }

        public async Task<PaginatedList<CategoryDto>> GetCategoriesPagedAsync(int? p)
        {
            return _mapper.Map<PaginatedList<CategoryDto>>
                (await _unitOfWork.CategoryRepository.GetAllPagedAsync(p));
        }

        public void Create(CategoryDto category)
        {
            _unitOfWork.CategoryRepository.Add(_mapper.Map<Category>(category));
            _unitOfWork.Commit();
        }
        public async Task<int> Update(CategoryDto category)
        {
            _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(category));
            await _unitOfWork.Commit();
            return category.Id;
        }
        private async Task Delete(Category category)
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
