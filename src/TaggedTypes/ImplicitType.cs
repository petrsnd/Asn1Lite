namespace Petrsnd.Asn1Lite.TaggedTypes
{
    public class ImplicitType : Asn1KnownType
    {
        public ImplicitType(Asn1TypeClass typeClass, Asn1UniversalTagNumber underlyingType, int tag, Asn1Object value) :
            base(typeClass, underlyingType, tag, value)
        {
        }
    }
}
