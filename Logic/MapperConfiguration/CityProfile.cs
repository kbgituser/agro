using AutoMapper;
using Agro.Model.Dto.Category;
using Agro.Model.Dto.City;
using Agro.Model.Dto.Intention;
using Agro.Model.Dto.User;
using Agro.Model.Entities;
using Agro.Model.PaginatedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.MapperConfiguration
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            //CreateMap<CategoryDto, Category>().ReverseMap()
            //    .ForMember(
            //    categoryDto => categoryDto.ChildrenCategories,
            //    category => category.MapFrom(
            //        c => c.ChildrenCategories.Select(cc => cc.Id)));


            //.ReverseMap()
            ;
            CreateMap<IntentionDto, Intention>().ReverseMap();


        }
    }
}
