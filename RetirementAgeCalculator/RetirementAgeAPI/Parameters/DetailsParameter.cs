using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetirementAgeAPI.Parameters
{
    /// <summary>
    /// Input parameters
    /// </summary>
    public class DetailsParameter
    {
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public long MonthlySavings { get; set; }
        public long TargetRetirementFunds { get; set; }
        public bool SaveDetails { get; set; }
    }
}
