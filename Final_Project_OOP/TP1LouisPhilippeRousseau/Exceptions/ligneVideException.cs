using System;
using System.Runtime.Serialization;

namespace TP1LouisPhilippeRousseau
{
    [Serializable]
    internal class ligneVideException : Exception
    {
        public ligneVideException()
        {
        }

        public ligneVideException(string message) : base(message)
        {
        }

        public ligneVideException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ligneVideException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}