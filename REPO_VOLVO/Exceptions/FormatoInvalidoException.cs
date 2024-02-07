using System;

namespace TRABALHO_VOLVO
{
    public class FormatoInvalidoException : Exception
    {
        public FormatoInvalidoException()
        {
        }

        public FormatoInvalidoException(string message)
            : base(message)
        {
        }

        public FormatoInvalidoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}