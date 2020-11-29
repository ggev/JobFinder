using System;

namespace JobFinder.Domain.Exceptions
{
    public class SmartException : ApplicationException
    {
        public SmartException(string message = "Smart Exception was thrown") : base(message)
        {
        }
    }
}