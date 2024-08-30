using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Agro.Model.Dto.City;
using Agro.Model.Entities;

namespace Logic.MapperConfiguration
{
    public class MapperSetup
    {
        public AutoMapper.MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new AutoMapper.MapperConfiguration(
                cfg => 
                {
                    cfg.AddProfile<CityProfile>();
                    cfg.AddProfile<UserProfile>();
                });
            return configuration;
        }
    }
}
