namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Boolean : Asn1Object
    {
        public Asn1Boolean(bool value) : base((int)Asn1UniversalTagNumber.Boolean)
        {
            Value = value;
        }

        public bool Value { get; }
    }
}
