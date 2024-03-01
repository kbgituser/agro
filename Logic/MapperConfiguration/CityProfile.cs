using AutoMapper;
using PlatF.Model.Dto.Category;
using PlatF.Model.Dto.City;
using PlatF.Model.Dto.Request;
using PlatF.Model.Dto.User;
using PlatF.Model.Entities;
using PlatF.Model.PaginatedList;
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
