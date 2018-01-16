using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerBooleanDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.Boolean;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if (!tag.IsPrimitive)
                throw new BerParseException("Decoder validation: Boolean encoding must be primitive");
            if (length.Length != 1)
                throw new BerParseException($"Decoder validation: Boolean length must be 1, found {length.Length}");
            BerReader.PeekData(length.Length, data, index);
            var obj = new Asn1Boolean(data[0] != 0x00);
            index++;
            return obj;
        }
    }
}
