using System;

namespace TRABALHO_VOLVO
{
    public class FKNotFoundException : Exception
    {
        public FKNotFoundException()
        {
        }

        public FKNotFoundException(string message)
            : base(message)
        {
        }

        public FKNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}