using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Mappers;
using AutoMapper;
using DataAccessLayer.Models;
using BusinessLayer.DTO;

namespace RetirementAgeAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Extension method for Injecting services and Mapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, 
                                                          IConfiguration configuration)
        {
            services.AddTransient<IRetirementService, RetirementService>();
            services.AddDbContext<ApplicationDbContext>();
            services.AddTransient<IRepository<DataAccessLayer.Models.RetirementEntity>, Repository<DataAccessLayer.Models.RetirementEntity>>();

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<MappingProfiles>();
            //});

            //var mapper = config.CreateMapper();
            //services.AddSingleton(mapper);
            return services;
        }
    }
}
