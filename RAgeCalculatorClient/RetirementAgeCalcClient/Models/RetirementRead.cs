using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetirementAgeCalcClient.Models
{
    public class RetirementRead
    {

        public int Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public long MonthlySavings { get; set; }

        public long TargetRetirementFunds { get; set; }
        public int RetirementAge { get; set; }

    }
}
