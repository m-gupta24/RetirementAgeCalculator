using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class InvalidNameException : BaseException
    {
        public InvalidNameException(string argumentName)
       : base($"{argumentName} is not valid.")
        { }

        public override int ErrorCode =>
            ErrorCodes.ZeroValue;
    }
}
