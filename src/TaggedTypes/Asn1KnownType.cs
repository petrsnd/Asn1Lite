namespace Petrsnd.Asn1Lite.TaggedTypes
{
    public abstract class Asn1KnownType : Asn1Object
    {
        protected Asn1KnownType(Asn1TypeClass typeClass, Asn1UniversalTagNumber underlyingType, int tag, Asn1Object value) : base(tag)
        {
            TypeClass = typeClass;
            UnderlyingType = underlyingType;
            Value = value;
        }

        public Asn1TypeClass TypeClass { get; }

        public Asn1UniversalTagNumber UnderlyingType { get; }

        public Asn1Object Value { get; }
    }
}
