using System;

namespace BusinessLayer.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message)
            : base(message)
        {  }

        public abstract int ErrorCode { get; }
    }
}
