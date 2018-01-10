using System;
using System.Runtime.Serialization;

namespace Petrsnd.Asn1Lite.Ber
{
    public class BerParseNotImplementedException : Exception
    {
        public BerParseNotImplementedException()
            : base("Unknown BerParseException")
        {
        }

        public BerParseNotImplementedException(Asn1UniversalTagNumber tag)
            : base($"ASN.1 Type {tag} parsing not implemented")
        {
        }

        public BerParseNotImplementedException(Asn1UniversalTagNumber tag, Exception innerException)
            : base($"ASN.1 Type {tag} parsing not implemented", innerException)
        {
        }

        protected BerParseNotImplementedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
