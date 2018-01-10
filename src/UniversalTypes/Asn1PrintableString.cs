namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1PrintableString : Asn1StringBase
    {
        public Asn1PrintableString(byte[] value) : base((int)Asn1UniversalTagNumber.PrintableString, value)
        {
        }
    }
}
