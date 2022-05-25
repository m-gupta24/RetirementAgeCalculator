using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public class InvalidIdException : BaseException
    {
        public InvalidIdException(int id)
        : base($"Id: {id} does not exist.")
        { }

        public override int ErrorCode =>
            ErrorCodes.IdDoesNotExist;
    }
}
