using System;
using System.Runtime.Serialization;

namespace TP1LouisPhilippeRousseau
{
    [Serializable]
    internal class listeDeLigneVideException : Exception
    {
        public listeDeLigneVideException()
        {
        }

        public listeDeLigneVideException(string message) : base(message)
        {
        }

        public listeDeLigneVideException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected listeDeLigneVideException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}