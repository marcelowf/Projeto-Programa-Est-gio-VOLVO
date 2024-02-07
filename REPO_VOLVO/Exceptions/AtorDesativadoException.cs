using System;

namespace TRABALHO_VOLVO
{
    public class AtorDesativadoException : Exception
    {
        public AtorDesativadoException()
        {
        }

        public AtorDesativadoException(string message)
            : base(message)
        {
        }

        public AtorDesativadoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}