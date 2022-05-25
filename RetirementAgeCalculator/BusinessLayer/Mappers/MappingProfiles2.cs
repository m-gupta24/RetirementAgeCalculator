using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public class MappingProfiles2 : Profile
    {
        public MappingProfiles2()
        {

            CreateMap<RetirementCreateDto, Retirement>();

             //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
             //.ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
             // .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
             //  .ForMember(dest => dest.MonthlySavings, opt => opt.MapFrom(src => src.MonthlySavings))
             //   .ForMember(dest => dest.TargetRetirementFunds, opt => opt.MapFrom(src => src.TargetRetirementFunds))
             //    .ForMember(dest => dest.MonthlyExpense, opt => opt.MapFrom(src => src.MonthlyExpense))
             //     .ForMember(dest => dest.ReturnOnCorpus, opt => opt.MapFrom(src => src.ReturnOnCorpus))
             //      .ForMember(dest => dest.ROIDuringAccumulation, opt => opt.MapFrom(src => src.ROIDuringAccumulation))
             //       .ForMember(dest => dest.InflationRate, opt => opt.MapFrom(src => src.InflationRate))
             //        .ForMember(dest => dest.CurrentAccumulation, opt => opt.MapFrom(src => src.CurrentAccumulation));


        }

    }
}
