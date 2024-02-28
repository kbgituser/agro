using AutoMapper;
using PlatF.Model.Dto.User;
using PlatF.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.MapperConfiguration
{
    public class UserProfile : Profile
    {
        public UserProfile() : base()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
