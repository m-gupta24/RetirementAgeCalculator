using DataAccessLayer.Configurations;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        //public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DbSet<DataAccessLayer.Models.RetirementEntity> RetirementEntity { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    string connectionString = Configuration.GetConnectionString("WebApiDatabase");
                    if (connectionString is null) throw new ArgumentNullException("connectionString");

                    optionsBuilder.UseSqlServer(connectionString);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new RetirementEntityMap(modelBuilder.Entity<RetirementEntity>().ToTable("Retirement"));
        }
    }
}
