using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Entity
    {
        public Entity()
        {
            //Id = Guid.NewGuid();
        }
        [Key]
        public int Id { get; set; }
    }
}
