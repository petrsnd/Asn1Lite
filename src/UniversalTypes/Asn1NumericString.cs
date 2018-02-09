namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1NumericString : Asn1StringBase
    {
        public Asn1NumericString() : base((int)Asn1UniversalTagNumber.NumericString)
        {
        }

        public Asn1NumericString(byte[] value) : base((int)Asn1UniversalTagNumber.NumericString, value)
        {
        }
    }
}
