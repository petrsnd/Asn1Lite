namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1StringBase : Asn1Object
    {
        public Asn1StringBase(int tag, byte[] value) : base(tag)
        {
            Value = value;
        }

        public byte[] Value { get; }
    }
}
