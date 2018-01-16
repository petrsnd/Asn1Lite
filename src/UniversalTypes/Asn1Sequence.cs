namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Sequence : Asn1StructuredTypeBase
    {
        public Asn1Sequence(Asn1Object[] value) : base((int)Asn1UniversalTagNumber.Sequence, value)
        {
        }
    }
}
