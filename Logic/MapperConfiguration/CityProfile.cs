using Agro.Model.Dto.Category;
using Agro.Model.Dto.City;
using Agro.Model.Dto.Intention;
using Agro.Model.Dto.Request;
using Agro.Model.Dto.User;
using Agro.Model.Entities;
using AutoMapper;
using System.Linq;

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
            CreateMap<Request, RequestDto>().
                ForMember(dest=>dest.CityId, opt=>opt.MapFrom(src=>src.Intentions.First().CityId)).
                ForMember(dest=>dest.City, opt=>opt.MapFrom(src=>src.Intentions.First().City)).
                ReverseMap()
                ;

        }
    }
}
