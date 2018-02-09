using System.Collections.Generic;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerOctetStringDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.OctetString;
        }

        private void DecodePrimitiveOctetStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            contents.AddRange(BerReader.ReadData(length.Length, data, ref index));
        }

        private void DecodeConstructedDefiniteOctetStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            BerReader.PeekData(length.Length, data, index);
            var lastIndex = index + length.Length;
            while (index < lastIndex)
            {
                var obj = BerDecoder.Decode(data, ref index);
                if (!(obj is Asn1OctetString subOctetString))
                    throw new BerParseException($"Decoder validation: Definite constructed encoding of octet string had sub object of tag = {obj.Tag}");
                contents.AddRange(subOctetString.Value);
            }
        }

        private void DecodeConstructedIndefiniteOctetStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            while (true)
            {
                var obj = BerDecoder.Decode(data, ref index);
                if (obj is Asn1EndOfContents)
                    break;
                if (!(obj is Asn1OctetString subOctetString))
                    throw new BerParseException($"Decoder validation: indefinite constructed encoding of octet string had sub object of tag = {obj.Tag}");
                contents.AddRange(subOctetString.Value);
            }
        }

        private void DecodeConstructedOctetStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            if (length.IsIndefinite)
                DecodeConstructedIndefiniteOctetStringContents(ref contents, tag, length, data, ref index);
            else
                DecodeConstructedDefiniteOctetStringContents(ref contents, tag, length, data, ref index);
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var contents = new List<byte>();
            if (tag.IsPrimitive)
                DecodePrimitiveOctetStringContents(ref contents, tag, length, data, ref index);
            else
                DecodeConstructedOctetStringContents(ref contents, tag, length, data, ref index);
            return new Asn1OctetString(contents.ToArray());
        }
    }
}
