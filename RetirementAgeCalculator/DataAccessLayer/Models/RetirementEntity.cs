using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class RetirementEntity : Entity
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public long MonthlySavings { get; set; }
        [Required]
        public long TargetRetirementFunds { get; set; }

    }
}
