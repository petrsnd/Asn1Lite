using System.Collections.Generic;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal abstract class BerStringDecoderBase<T> where T : Asn1StringBase, new()
    {
        private void DecodePrimitiveStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            contents.AddRange(BerReader.ReadData(length.Length, data, ref index));
        }

        private void DecodeConstructedDefiniteStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            BerReader.PeekData(length.Length, data, index);
            var lastIndex = index + length.Length;
            while (index < lastIndex)
            {
                var obj = BerDecoder.Decode(data, ref index);
                if (!(obj is T subString))
                    throw new BerParseException($"Decoder validation: Definite constructed encoding of string had sub object of tag = {obj.Tag}, expected {new T().Tag}");
                contents.AddRange(subString.Value);
            }
        }

        private void DecodeConstructedIndefiniteStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            while (true)
            {
                var obj = BerDecoder.Decode(data, ref index);
                if (obj is Asn1EndOfContents)
                    break;
                if (!(obj is T subString))
                    throw new BerParseException($"Decoder validation: indefinite constructed encoding of string had sub object of tag = {obj.Tag}, expected {new T().Tag}");
                contents.AddRange(subString.Value);
            }
        }

        private void DecodeConstructedStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            if (length.IsIndefinite)
                DecodeConstructedIndefiniteStringContents(ref contents, tag, length, data, ref index);
            else
                DecodeConstructedDefiniteStringContents(ref contents, tag, length, data, ref index);
        }

        protected List<byte> DecodeStringContents(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var contents = new List<byte>();
            if (tag.IsPrimitive)
                DecodePrimitiveStringContents(ref contents, tag, length, data, ref index);
            else
                DecodeConstructedStringContents(ref contents, tag, length, data, ref index);
            return contents;
        }
    }
}
