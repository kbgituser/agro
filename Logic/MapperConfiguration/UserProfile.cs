using Agro.Model.Dto.User;
using Agro.Model.Entities;
using AutoMapper;

namespace Logic.MapperConfiguration;

public class UserProfile : Profile
  {
      public UserProfile() : base()
      {
          CreateMap<ApplicationUser, UserDto>().ReverseMap();
      }
  }
