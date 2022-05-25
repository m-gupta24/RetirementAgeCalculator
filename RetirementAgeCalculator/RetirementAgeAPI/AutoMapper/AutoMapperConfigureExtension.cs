using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Mappers;
using Microsoft.Extensions.DependencyInjection;
using RetirementAgeAPI.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetirementAgeAPI.AutoMapper
{
    public static class AutoMapperConfigureExtension
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DetailsParameter, RetirementCreateDto>();
                cfg.AddProfile<MappingProfiles>();
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
