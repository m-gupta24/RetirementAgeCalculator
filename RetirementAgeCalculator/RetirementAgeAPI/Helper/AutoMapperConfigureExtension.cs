using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetirementAgeAPI.Helper
{
    public static class AutoMapperConfigureExtension
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Retirement, RetirementReadDto>();
                cfg.CreateMap<RetirementCreateDto, Retirement>();
            });

            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
