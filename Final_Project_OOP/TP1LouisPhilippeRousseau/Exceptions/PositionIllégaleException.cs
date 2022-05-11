using System;
using System.Runtime.Serialization;

namespace TP1LouisPhilippeRousseau
{
    [Serializable]
    internal class PositionIllégaleException : Exception
    {
        public PositionIllégaleException()
        {
        }

        public PositionIllégaleException(string message) : base(message)
        {
        }

        public PositionIllégaleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionIllégaleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}