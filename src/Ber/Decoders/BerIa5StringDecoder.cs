using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerIa5StringDecoder : BerStringDecoderBase<Asn1Ia5String>, IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.Ia5String;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var contents = DecodeStringContents(tag, length, data, ref index);
            return new Asn1Ia5String(contents.ToArray());
        }
    }
}
