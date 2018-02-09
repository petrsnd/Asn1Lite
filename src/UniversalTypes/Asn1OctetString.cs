using System;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1OctetString : Asn1StringBase
    {
        public Asn1OctetString() : base((int)Asn1UniversalTagNumber.OctetString)
        {
        }

        public Asn1OctetString(byte[] value) : base((int)Asn1UniversalTagNumber.OctetString, value)
        {
        }

        public override string ValueAsString => BitConverter.ToString(Value).Replace("-", " ");
    }
}
