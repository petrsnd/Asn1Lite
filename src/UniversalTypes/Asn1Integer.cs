namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Integer : Asn1Object
    {
        public Asn1Integer(long value) : base((int)Asn1UniversalTagNumber.Integer)
        {
            Value = value;
        }

        public long Value { get; }
    }
}
