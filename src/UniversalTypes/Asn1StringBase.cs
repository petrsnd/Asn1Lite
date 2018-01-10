namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public abstract class Asn1StringBase : Asn1Object
    {
        protected Asn1StringBase(int tag, byte[] value) : base(tag)
        {
            Value = value;
        }

        public byte[] Value { get; }
    }
}
