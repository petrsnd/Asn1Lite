using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerVisibleStringDecoder : BerStringDecoderBase<Asn1VisibleString>, IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.VisibleString;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var contents = DecodeStringContents(tag, length, data, ref index);
            return new Asn1VisibleString(contents.ToArray());
        }
    }
}
