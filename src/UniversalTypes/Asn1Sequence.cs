namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Sequence : Asn1Object
    {
        public Asn1Sequence(Asn1Object[] value) : base((int)Asn1UniversalTagNumber.Sequence)
        {
            Value = value;
        }

        public Asn1Object[] Value { get; }
    }
}
