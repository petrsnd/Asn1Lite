namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1RelativeOid : Asn1Object
    {
        public Asn1RelativeOid(int[] value) : base((int)Asn1UniversalTagNumber.RelativeOid)
        {
            Value = value;
        }

        public int[] Value { get; }
    }
}
