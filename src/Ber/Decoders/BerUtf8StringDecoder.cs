using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerUtf8StringDecoder : BerStringDecoderBase<Asn1Utf8String>, IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.Utf8String;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var contents = DecodeStringContents(tag, length, data, ref index);
            return new Asn1Utf8String(contents.ToArray());
        }
    }
}
