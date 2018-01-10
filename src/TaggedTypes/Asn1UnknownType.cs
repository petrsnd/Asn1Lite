namespace Petrsnd.Asn1Lite.TaggedTypes
{
    public class Asn1UnknownType : Asn1Object
    {
        public Asn1UnknownType(Asn1TypeClass typeClass, int tag, byte[] value) : base(tag)
        {
            TypeClass = typeClass;
            Value = value;
        }

        public Asn1TypeClass TypeClass { get;  }

        public byte[] Value { get; }
    }
}
