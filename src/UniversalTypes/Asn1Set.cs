namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Set : Asn1Object
    {
        public Asn1Set(Asn1Object[] value) : base((int)Asn1UniversalTagNumber.Set)
        {
            Value = value;
        }

        public Asn1Object[] Value { get; }
    }
}
