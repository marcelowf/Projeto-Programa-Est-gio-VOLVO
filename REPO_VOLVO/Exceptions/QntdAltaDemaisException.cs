using System;

namespace TRABALHO_VOLVO
{
    public class QntdAltaDemaisException : Exception
    {
        public QntdAltaDemaisException()
        {
        }

        public QntdAltaDemaisException(string message)
            : base(message)
        {
        }

        public QntdAltaDemaisException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}