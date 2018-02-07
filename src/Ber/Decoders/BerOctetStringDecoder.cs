using System.Collections.Generic;
using System.Linq;
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
            contents.AddRange(data.Skip(index).Take(length.Length));
            index += length.Length;
        }

        private void DecodeConstructedDefiniteOctetStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            // TODO
        }

        private void DecodeConstructedIndefiniteOctetStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            // TODO
        }

        private void DecodeConstructedOctetStringContents(ref List<byte> contents, BerTag tag, BerLength length,
            byte[] data, ref int index)
        {
            if (length.IsIndefinite)
                DecodeConstructedDefiniteOctetStringContents(ref contents, tag, length, data, ref index);
            else
                DecodeConstructedDefiniteOctetStringContents(ref contents, tag, length, data, ref index);
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var contents = new List<byte>();
            if (tag.IsPrimitive)
            {
                DecodePrimitiveOctetStringContents(ref contents, tag, length, data, ref index);
            }
            else
            {
                DecodeConstructedOctetStringContents(ref contents, tag, length, data, ref index);
            }
            return new Asn1OctetString(contents.ToArray());
        }
    }
}
