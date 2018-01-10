namespace Petrsnd.Asn1Lite.TaggedTypes
{
    public class Asn1ImplicitType : Asn1KnownType
    {
        public Asn1ImplicitType(Asn1TypeClass typeClass, Asn1UniversalTagNumber underlyingType, int tag, Asn1Object value) :
            base(typeClass, underlyingType, tag, value)
        {
        }
    }
}
