using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Exceptions;
using BusinessLayer.Helpers;
using BusinessLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class RetirementService : IRetirementService
    {
        private readonly IRepository<RetirementEntity> _retirementRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RetirementService(IRepository<RetirementEntity> retirementRepo, IMapper mapper, ILogger<RetirementService> logger)
        {
            Guard.ArgumentNotNull(nameof(retirementRepo), retirementRepo);
            Guard.ArgumentNotNull(nameof(mapper), mapper);
            Guard.ArgumentNotNull(nameof(logger), logger);

            _retirementRepo = retirementRepo;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Calculates the Retirement Age and also saves the details to the DB if 
        /// SaveDetails parameter of the RetirementCreateDto is true
        /// </summary>
        /// <param name="retirementCreateDto">Retirement Details</param>
        /// <returns>
        ///             RetirementAgeDetailsDto : contains Id and RetirementAge
        ///             Id = 0 if the retirement details are not saved in the DB
        ///             Id = non zero if saved
        ///  </returns>
        public RetirementAgeDetailsDto GetRetirementAge(RetirementCreateDto retirementCreateDto)
        {
            int id = 0;
            retirementCreateDto.Validate(nameof(retirementCreateDto));

            retirementCreateDto.RetirementAge = Calculator.GetRetirementAge(retirementCreateDto.Age,
                                        retirementCreateDto.TargetRetirementFunds,
                                        retirementCreateDto.MonthlySavings);

            if (retirementCreateDto.SaveDetails)
            {
                var modelItem = _mapper.Map<RetirementEntity>(retirementCreateDto);
                bool isCreated = _retirementRepo.Create(modelItem);
                id = modelItem.Id;
            }
            var retirementAgeDetailsDto = new RetirementAgeDetailsDto
            {
                Id = id,
                RetirementAge = retirementCreateDto.RetirementAge
            };

            return retirementAgeDetailsDto;

        }

        /// <summary>
        /// Returns the Retirement details for the specified Id,
        /// Calculates the retirement age and sets the RetirementAge 
        /// parameter in the RetirementReadDto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RetirementReadDto GetRetirementDetailsById(int id)
        {
            id.Validate(nameof(id));

            var retirement = _retirementRepo.GetByID(id);

            retirement.Validate(id);

            var retirementReadDto = _mapper.Map<RetirementReadDto>(retirement);
            retirementReadDto.RetirementAge = Calculator.GetRetirementAge(retirementReadDto.Age,
                                                  retirementReadDto.TargetRetirementFunds,
                                                  retirementReadDto.MonthlySavings);
            return retirementReadDto;


        }
    }
}

