using System;

namespace TRABALHO_VOLVO
{
    public class DadosInsuficientesException : Exception
    {
        public DadosInsuficientesException()
        {
        }

        public DadosInsuficientesException(string message)
            : base(message)
        {
        }

        public DadosInsuficientesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}