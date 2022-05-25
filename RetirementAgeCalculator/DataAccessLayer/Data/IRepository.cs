using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public interface IRepository <TEntity> where TEntity : class
    {
        TEntity GetByID(int Id);
        bool Create(TEntity record);
        bool SaveChanges();
    }
}
