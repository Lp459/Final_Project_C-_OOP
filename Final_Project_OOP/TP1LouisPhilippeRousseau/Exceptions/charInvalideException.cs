using System;
using System.Runtime.Serialization;

namespace TP1LouisPhilippeRousseau
{
    [Serializable]
    internal class charInvalideException : Exception
    {
        public charInvalideException()
        {
        }

        public charInvalideException(string message) : base(message)
        {
        }

        public charInvalideException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected charInvalideException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}