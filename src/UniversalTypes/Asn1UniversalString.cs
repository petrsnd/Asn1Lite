namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1UniversalString : Asn1StringBase
    {
        public Asn1UniversalString() : base((int)Asn1UniversalTagNumber.UniversalString)
        {
        }

        public Asn1UniversalString(byte[] value) : base((int)Asn1UniversalTagNumber.UniversalString, value)
        {
        }
    }
}
