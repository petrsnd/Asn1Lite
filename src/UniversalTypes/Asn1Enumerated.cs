namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Enumerated : Asn1Object
    {
        public Asn1Enumerated(long value) : base((int)Asn1UniversalTagNumber.Enumerated)
        {
            Value = value;
        }

        public long Value { get; }
    }
}
