using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Data;
using BusinessLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Exceptions;
using RetirementAgeAPI.Parameters;

namespace RetirementAgeAPI.Controllers
{
    [Authorize(Policy="user")]
    [Route("[controller]")]
    [ApiController]
    public class RetirementController : ControllerBase
    {
        private IRetirementService _service;
        private readonly ILogger<RetirementController> _logger;
        private readonly IMapper _mapper;

        public RetirementController(IRetirementService service, ILogger<RetirementController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// This Api retrieves the Retirement details (including the calculated 
        /// retirement age) for the specified Id.
        /// </summary>
        /// <param name="id">Retirement details Id</param>
        /// <returns>RetirementReadDto</returns>

        [HttpGet("{id}", Name = "GetRetirementById")]
        public ActionResult<RetirementReadDto> GetRetirementById(int id)
        {
            RetirementReadDto readDto = _service.GetRetirementDetailsById(id);

            if (readDto is null) return NotFound();

            return Ok(readDto);
        }
        /// <summary>
        /// This api uses the input parameter RetirementCreateDto
        /// and based on the details in the dto calculates the Retirement Age.
        /// It also persists the retirement details to the db if 
        /// SaveDetails parameter of the RetirementCreateDto is true.
        /// </summary>
        /// <param name="detailsParameter">Retirement Details</param>
        /// <returns>
        ///             RetirementAgeDetailsDto : contains Id and RetirementAge
        ///             Id = 0 if the retirement details are not saved in the DB
        ///             Id = non zero if saved
        ///  </returns>

        [HttpPost("GetRetirementAge")]
        public ActionResult<RetirementAgeDetailsDto> GetRetirementAge(DetailsParameter detailsParameter)
        {

            var mapper = _mapper.Map<RetirementCreateDto>(detailsParameter);
            RetirementAgeDetailsDto readDto = _service.GetRetirementAge(mapper);

            return Ok(readDto);

        }
    }
}
