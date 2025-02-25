using Agro.Model.Dto.Category;
using Agro.Model.Entities;
using AutoMapper;

namespace Logic.MapperConfiguration
{
  public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
