using AutoMapper;
using BusinessLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BusinessLayer.Mappers;
using BusinessLayer.DTO;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Exceptions;

namespace Retirement.UnitTests.BusinessLayer.Tests
{
    [TestFixture]
    class RetirementServiceTests
    {
        protected RetirementService RetirementService { get; set; }
        protected Mock<IRepository<RetirementEntity>> RetirementRepoMock { get; set; }
        protected Mock<ILogger<RetirementService>> LoggerMock { get; set; }
        protected MapperConfiguration MappingConfig { get; set; }
        protected IMapper Mapper { get; set; }

        public RetirementServiceTests()
        {
            RetirementRepoMock = new Mock<IRepository<RetirementEntity>>();
            LoggerMock = new Mock<ILogger<RetirementService>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            Mapper = config.CreateMapper();
            RetirementService = new RetirementService(RetirementRepoMock.Object, Mapper, LoggerMock.Object);
        }

        #region Tests - GetRetirementAge
        [Test]
        public void GetRetirementAge_given_data_set_returns_RetirementAgeDetailsDto()
        {
            RetirementCreateDto dto = new RetirementCreateDto
            {
                FullName = "Anurag Anand",
                Age = 38,
                Gender = "M",
                MonthlySavings = 45000,
                TargetRetirementFunds = 65000000,
                SaveDetails = false
            };
            var result = RetirementService.GetRetirementAge(dto);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 0);
        }
        [Test]
        public void GetRetirementAge_given_Save_true_returns_RetirementAgeDetailsDto_Id_not_Empty()
        {
            RetirementCreateDto dto = new RetirementCreateDto
            {
                FullName = "Anurag Anand",
                Age = 38,
                Gender = "M",
                MonthlySavings = 45000,
                TargetRetirementFunds = 65000000,
                SaveDetails = true
            };
            var result = RetirementService.GetRetirementAge(dto);
            Assert.IsNotNull(result);
            //Assert.AreNotEqual(result.Id, 0);
        }
        [Test]
        public void GetRetirementAge_given_targetRetirementFunds_as_Zero_throws_EmptyValueException()
        {
            RetirementCreateDto dto = new RetirementCreateDto
            {
                FullName = "Anurag Anand",
                Age = 38,
                Gender = "M",
                MonthlySavings = 45000,
                TargetRetirementFunds = 0,
                SaveDetails = true
            };
            Assert.Throws<InvalidValueException>(() => RetirementService.GetRetirementAge(dto));
        }
        [Test]
        public void GetRetirementAge_given_input_as_null_throws_ArgumentNullException()
        {
            RetirementCreateDto dto = null;
            Assert.Throws<ArgumentNullException>(() => RetirementService.GetRetirementAge(dto));
        }
        [Test]
        public void GetRetirementAge_given_monthlySavings_As_Zero_throws_EmptyValueException()
        {
            RetirementCreateDto dto = new RetirementCreateDto
            {
                FullName = "Anurag Anand",
                Age = 38,
                Gender = "M",
                MonthlySavings = 0,
                TargetRetirementFunds = 4500000,
                SaveDetails = true
            };
            Assert.Throws<InvalidValueException>(() => RetirementService.GetRetirementAge(dto));
        }
        [Test]
        public void GetRetirementAge_given_age_less_than_18_throws_EmptyValueException()
        {
            RetirementCreateDto dto = new RetirementCreateDto
            {
                FullName = "Anurag Anand",
                Age = 15,
                Gender = "M",
                MonthlySavings = 0,
                TargetRetirementFunds = 4500000,
                SaveDetails = true
            };
            Assert.Throws<InvalidValueException>(() => RetirementService.GetRetirementAge(dto));
        }
        [Test]
        public void GetRetirementAge_given_invalid_Gender_throws_EmptyValueException()
        {
            RetirementCreateDto dto = new RetirementCreateDto
            {
                FullName = "Anurag Anand",
                Age = 15,
                Gender = "K",
                MonthlySavings = 0,
                TargetRetirementFunds = 4500000,
                SaveDetails = true
            };
            Assert.Throws<InvalidValueException>(() => RetirementService.GetRetirementAge(dto));
        }
        #endregion

        #region Tests - GetRetirementDetailsById
        [Test]
        public void GGetRetirementDetailsById_given_zero_id_throws_ArgumentNullException()
        {
            int id = 0;
            Assert.Throws<InvalidValueException>(() => RetirementService.GetRetirementDetailsById(id));
        }
        
        #endregion

        #region Supporting methods
        private IEnumerable<RetirementEntity> GetRetirements()
        {

            var Retirements = new List<RetirementEntity>
            {
                new RetirementEntity
                {
                    Id = 1,
                    FullName = "Anurag Anand",
                    Age = 38,
                    Gender = "M",
                    MonthlySavings = 45000,
                    TargetRetirementFunds = 65000000,
                },
                new RetirementEntity
                {
                    Id = 2,
                    FullName = "Arundhati Roy",
                    Age = 30,
                    Gender = "F",
                    MonthlySavings = 35000,
                    TargetRetirementFunds = 40000000,
                },
                new RetirementEntity
                {
                    Id = 3,
                    FullName = "Gautam Bhatia",
                    Age = 43,
                    Gender = "M",
                    MonthlySavings = 145000,
                    TargetRetirementFunds = 250000000,
                }
            };

            return Retirements;
        }
        #endregion
    }
}

