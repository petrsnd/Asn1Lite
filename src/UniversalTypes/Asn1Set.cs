namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Set : Asn1StructuredTypeBase
    {
        public Asn1Set(Asn1Object[] value) : base((int) Asn1UniversalTagNumber.Set, value)
        {
        }
    }
}
