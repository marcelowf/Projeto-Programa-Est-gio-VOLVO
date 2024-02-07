using System;

namespace TRABALHO_VOLVO
{
    public class DuplicateUniqueValueException : Exception
    {
        public DuplicateUniqueValueException()
        {
        }

        public DuplicateUniqueValueException(string message)
            : base(message)
        {
        }

        public DuplicateUniqueValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}