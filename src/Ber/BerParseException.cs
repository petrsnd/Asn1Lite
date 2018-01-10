using System;
using System.Runtime.Serialization;

namespace Petrsnd.Asn1Lite.Ber
{
    public class BerParseException : Exception
    {
        public BerParseException()
            : base("Unknown BerParseException")
        {
        }

        public BerParseException(string message)
            : base(message)
        {
        }

        public BerParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BerParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
