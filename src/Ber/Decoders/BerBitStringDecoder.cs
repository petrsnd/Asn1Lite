using System.Collections;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerBitStringDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.BitString;
        }

        private void UpdateContents(ref BitArray contents, byte initial, byte[] values)
        {
            var index = contents.Length;
            var numNew = (values.Length * 8) - initial;
            contents.Length += numNew;
            for (var i = 0; i < numNew; i++)
                contents.Set(index + i, (values[(i / 8)] & (0x80 >> (i % 8))) != 0x00);
        }

        private void UpdateContents(ref BitArray contents, BitArray bitArray)
        {
            var index = contents.Length;
            var numNew = bitArray.Length;
            contents.Length += numNew;
            for (var i = 0; i < numNew; i++)
                contents.Set(index + i, bitArray.Get(i));
        }

        private void DecodePrimitiveBitStringContents(ref BitArray contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            var initial = BerReader.ReadByte(data, ref index);
            if (length.Length == 1)
            {
                if (initial != 0x00)
                    throw new BerParseException("Decoder validation: bit string initial octet must be zero for empty bit array");
            }
            else
            {
                if (initial > 0x07)
                    throw new BerParseException("Decoder validation: bit string initial octet may not be greater than 7");
                var values = BerReader.ReadData(length.Length - 1, data, ref index);
                UpdateContents(ref contents, initial, values);    
            }
        }

        private void DecodeConstructedDefiniteBitStringContents(ref BitArray contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            BerReader.PeekData(length.Length, data, index);
            var lastIndex = index + length.Length;
            while (index < lastIndex)
            {
                var obj = BerDecoder.Decode(data, ref index);
                if (!(obj is Asn1BitString subBitString))
                    throw new BerParseException($"Decoder validation: Definite constructed encoding of bit string had sub object of tag = {obj.Tag}");
                UpdateContents(ref contents, subBitString.Value);
            }
        }

        private void DecodeConstructedIndefiniteBitStringContents(ref BitArray contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            while (true)
            {
                var obj = BerDecoder.Decode(data, ref index);
                if (obj is Asn1EndOfContents)
                    break;
                if (!(obj is Asn1BitString subBitString))
                    throw new BerParseException($"Decoder validation: indefinite constructed encoding of bit string had sub object of tag = {obj.Tag}");
                UpdateContents(ref contents, subBitString.Value);
            }
        }

        private void DecodeConstructedBitStringContents(ref BitArray contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            if (length.IsIndefinite)
                DecodeConstructedIndefiniteBitStringContents(ref contents, tag, length, data, ref index);
            else
                DecodeConstructedDefiniteBitStringContents(ref contents, tag, length, data, ref index);
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var contents = new BitArray(0);
            if (tag.IsPrimitive)
                DecodePrimitiveBitStringContents(ref contents, tag, length, data, ref index);
            else
                DecodeConstructedBitStringContents(ref contents, tag, length, data, ref index);
            return new Asn1BitString(contents);
        }
    }
}
