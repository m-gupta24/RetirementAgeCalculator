using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class RetirementEntityMap
    {
        public RetirementEntityMap(EntityTypeBuilder<RetirementEntity> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Age).IsRequired();
            entityBuilder.Property(t => t.FullName).IsRequired();
            entityBuilder.Property(t => t.Gender).IsRequired();
            entityBuilder.Property(t => t.MonthlySavings).IsRequired();
            entityBuilder.Property(t => t.TargetRetirementFunds).IsRequired();
        }
    }
}
