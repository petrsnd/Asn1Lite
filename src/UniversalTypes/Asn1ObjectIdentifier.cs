namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1ObjectIdentifier : Asn1Object
    {
        public Asn1ObjectIdentifier(int[] value) : base((int)Asn1UniversalTagNumber.ObjectIdentifier)
        {
            Value = value;
        }

        public int[] Value { get; }
    }
}
