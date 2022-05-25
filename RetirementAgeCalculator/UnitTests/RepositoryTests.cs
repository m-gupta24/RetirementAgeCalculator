using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests.DataAccessLayer
{
    public class RepositoryTests
    {
        //private readonly ApplicationDbContext context;

        //public RepositoryTests() 
        //{
        //    DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
        //            .UseInMemoryDatabase(
        //            Guid.NewGuid().ToString() // Use GUID so every test will use a different db
        //        );
            
        //    context = new ApplicationDbContext(dbOptions.Options);
        //}
        
        //[Fact]
        //public void Create_given_data_adds_entity_returns_true()
        //{
        //    // Arrange
        //    var repository = new Repository<RetirementEntity>(context);
        //    var retirementEntity = new RetirementEntity
        //    {
        //        FullName = "Anurag Anand",
        //        Age = 27,
        //        Gender = "M",
        //        MonthlySavings = 45000,
        //        TargetRetirementFunds = 65000000,
        //    };

        //    // Act
        //    bool result = repository.Create(retirementEntity);

        //    // Assert
        //    List<RetirementEntity> retirementEntityList = context.RetirementEntity.ToList();
        //    Assert.True(result);
        //    Assert.Single(retirementEntityList);
        //}
        //[Fact]
        //public void Create_given_null_adds_entity_throws_ArgumentNullException()
        //{
        //    // Arrange
        //    var repository = new Repository<RetirementEntity>(context);
        //    RetirementEntity retirementEntity = null;

        //    Assert.Throws<ArgumentNullException>(() => repository.Create(retirementEntity));
        //}

        //[Fact]
        //public void GetByID_given_id_retrieves_entity()
        //{
        //    // Arrange
        //    var repository = new Repository<RetirementEntity>(context);
        //    var expectedRetirementEntity = new RetirementEntity
        //    {
        //        FullName = "Anurag Anand",
        //        Age = 27,
        //        Gender = "M",
        //        MonthlySavings = 45000,
        //        TargetRetirementFunds = 65000000,
        //    };

        //    bool result = repository.Create(expectedRetirementEntity);
        //    List<RetirementEntity> retirementEntityList = context.RetirementEntity?.ToList();

        //    //act
        //    var resultEntity = repository.GetByID(retirementEntityList[0].Id);
        //    Assert.NotNull(resultEntity);

        //}
        //[Fact]
        //public void GetByID_given_Zero_id_returns_null()
        //{
        //    // Arrange
        //    var repository = new Repository<RetirementEntity>(context);

        //    var resultEntity = repository.GetByID(0);
        //    Assert.Null(resultEntity);
        //}
    }
}