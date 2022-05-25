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
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RetirementEntity, RetirementReadDto>();
            CreateMap<RetirementCreateDto, RetirementEntity>();
        }
    }
}
