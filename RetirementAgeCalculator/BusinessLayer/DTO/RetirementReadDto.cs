using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class RetirementReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public long MonthlySavings { get; set; }
        public long TargetRetirementFunds { get; set; }
        public int RetirementAge { get; set; }
        //public long MonthlyExpense { get; set; }
        //public long CurrentAccumulation { get; set; }
        //public decimal ReturnOnCorpus { get; set; }
        //public decimal ROIDuringAccumulation { get; set; }
        //public decimal InflationRate { get; set; }

    }
}
