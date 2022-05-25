using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity

    {
        protected readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
     
        public bool Create(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException("entity");
            
            _entities.Add(entity);
            return SaveChanges();
        }
        public TEntity GetByID(int Id)
        {
            var result = _entities?.FirstOrDefault(r => r.Id == Id);
            return result;
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {  //log: could not save 
                return false;
            }
        }
    }
}
