namespace Petrsnd.Asn1Lite.TaggedTypes
{
    public class ExplicitType : Asn1KnownType
    {
        public ExplicitType(Asn1TypeClass typeClass, Asn1UniversalTagNumber underlyingType, int tag, Asn1Object value) :
            base(typeClass, underlyingType, tag, value)
        {
        }
    }
}
