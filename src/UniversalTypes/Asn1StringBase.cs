namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public abstract class Asn1StringBase : Asn1Object
    {
        protected Asn1StringBase(int tag) : base(tag)
        {
            Value = new byte[0];
        }

        protected Asn1StringBase(int tag, byte[] value) : base(tag)
        {
            Value = value;
        }

        public byte[] Value { get; }

        public abstract string ValueAsString { get; }
    }
}
