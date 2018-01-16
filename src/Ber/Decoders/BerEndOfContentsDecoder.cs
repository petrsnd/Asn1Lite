using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerEndOfContentsDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.EndOfContents;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if (length.Length != 0)
                throw new BerParseException("Decoder validation: End of contents length must be zero");
            return new Asn1EndOfContents();
        }
    }
}
