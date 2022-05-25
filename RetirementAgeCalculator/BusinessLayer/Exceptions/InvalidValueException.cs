using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class InvalidValueException : BaseException
    {
       public InvalidValueException(string argumentName)
      : base($"{argumentName} should be greater than 0.")
        { }

        public override int ErrorCode =>
            ErrorCodes.ZeroValue;
    }
}
