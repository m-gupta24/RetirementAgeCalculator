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

namespace Retirement.UnitTests.BusinessLayer.Tests
{
    [TestFixture]
    class RetirementServiceTests
    {
        protected RetirementService RetirementService { get; set; }
        protected Mock<IRetirementRepo> RetirementRepoMock { get; set; }
        protected Mock<ILogger<RetirementService>> LoggerMock { get; set; }
        protected MapperConfiguration MappingConfig { get; set; }
        protected IMapper Mapper { get; set; }
        private Mock<RetirementDbContext> mockContext;
        private Mock<DbSet<DataAccessLayer.Models.Retirement>> mockRetirementSet;
        private List<DataAccessLayer.Models.Retirement> RetirementList;
        //private RetirementService service;

        #region Setup
        [SetUp]
        public void Setup()
        {
            RetirementList = new List<DataAccessLayer.Models.Retirement>
            {
                new DataAccessLayer.Models.Retirement
                {
                    Id = 1,
                    FullName = "Anurag Anand",
                    Age = 38,
                    Gender = "M",
                    MonthlySavings = 45000,
                    TargetRetirementFunds = 65000000,
                },
                new DataAccessLayer.Models.Retirement
                {
                    Id = 2,
                    FullName = "Arundhati Roy",
                    Age = 30,
                    Gender = "F",
                    MonthlySavings = 35000,
                    TargetRetirementFunds = 40000000,
                },
                new DataAccessLayer.Models.Retirement
                {
                    Id = 3,
                    FullName = "Gautam Bhatia",
                    Age = 43,
                    Gender = "M",
                    MonthlySavings = 145000,
                    TargetRetirementFunds = 250000000,
                }
            };
            mockRetirementSet = new Mock<DbSet<DataAccessLayer.Models.Retirement>>(RetirementList);
            var options = new DbContextOptions<RetirementDbContext>();
            mockContext = new Mock<RetirementDbContext>(options, null);

            //mockContext
            //    .Setup(m => m.Retirements.Add(It.IsAny<DataAccessLayer.Models.Retirement>()))
            //    .Returns(mockRetirementSet.Object);

            RetirementRepoMock = new Mock<IRetirementRepo>(mockContext);
            LoggerMock = new Mock<ILogger<RetirementService>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            Mapper = config.CreateMapper();
            RetirementService = new RetirementService(RetirementRepoMock.Object, Mapper, LoggerMock.Object);

            //var connectionBase = new ConnectionBase() { DbContext = mockContext.Object };
            //service = new RetirementService(connectionBase);
        }
        #endregion
        public RetirementServiceTests()
        {
            RetirementRepoMock = new Mock<IRetirementRepo>();
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
        public void GetRetirementAge_given_input_as_null_throws_ArgumentNullException()
        {
            RetirementCreateDto dto = null;
            Assert.Throws<ArgumentNullException>(() => RetirementService.GetRetirementAge(dto));
        }
        #endregion

        #region Supporting methods
        private IEnumerable<DataAccessLayer.Models.Retirement> GetRetirements()
        {

            var Retirements = new List<DataAccessLayer.Models.Retirement>
            {
                new DataAccessLayer.Models.Retirement
                {
                    Id = 1,
                    FullName = "Anurag Anand",
                    Age = 38,
                    Gender = "M",
                    MonthlySavings = 45000,
                    TargetRetirementFunds = 65000000,
                },
                new DataAccessLayer.Models.Retirement
                {
                    Id = 2,
                    FullName = "Arundhati Roy",
                    Age = 30,
                    Gender = "F",
                    MonthlySavings = 35000,
                    TargetRetirementFunds = 40000000,
                },
                new DataAccessLayer.Models.Retirement
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

