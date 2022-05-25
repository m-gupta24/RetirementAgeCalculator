using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Enums
{
    public enum GenderEnum
    {
        [Description("M")]
        Male = 1,
        [Description("F")]
        Female = 2,
        [Description("O")]
        Other = 3
    }
}
