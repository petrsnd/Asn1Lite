namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Real : Asn1Object
    {
        public Asn1Real(decimal value) : base((int)Asn1UniversalTagNumber.Real)
        {
            Value = value;
        }

        public decimal Value { get; }
    }
}
