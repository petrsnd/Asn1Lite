namespace Petrsnd.Asn1Lite.TaggedTypes
{
    public class Asn1ExplicitType : Asn1KnownType
    {
        public Asn1ExplicitType(Asn1TypeClass typeClass, Asn1UniversalTagNumber underlyingType, int tag, Asn1Object value) :
            base(typeClass, underlyingType, tag, value)
        {
        }
    }
}
