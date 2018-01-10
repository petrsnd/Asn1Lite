namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1OctetString : Asn1Object
    {
        public Asn1OctetString(byte[] value) : base((int)Asn1UniversalTagNumber.OctetString)
        {
            Value = value;
        }

        public byte[] Value { get; }
    }
}
