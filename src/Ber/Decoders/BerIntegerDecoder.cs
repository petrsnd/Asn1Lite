using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerIntegerDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.Integer;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if (!tag.IsPrimitive)
                throw new BerParseException("Decoder validation: Integer encoding must be primitive");
            var size = length.Length;
            if (size < 1)
                throw new BerParseException($"Decoder validation: Integer length must be positive number, found {size}");
            if (size > 8)
                throw new BerParseException($"Decoder implementation: No support for integers larger than 8 bytes, found {size}");
            BerReader.PeekData(length.Length, data, index);
            if (size >= 2)
            {
                if (data[index] == 0xff && (data[index + 1] & 0x80) == 0x80)
                    throw new BerParseException(
                        "Decoder validation: Integer--All eight bits of first byte and bit 8 of second byte cannot be 1, see X.680 8.3.2");
                if (data[index] == 0x00 && (data[index + 1] & 0x80) == 0x00)
                    throw new BerParseException(
                        "Decoder validation: Integer--All eight bits of first byte and bit 8 of second byte cannot be 0, see X.680 8.3.2");
            }
            long contents = 0;
            for (var i = 0; i < size; i++)
            {
                contents = contents << 8;
                contents += (data[index + i]);
            }
            // handle  2's complement encoding
            if ((contents & (0x80 << (size * 8))) != 0x00)
                contents = (~contents) + 1;
            index += size;
            return new Asn1Integer(contents);
        }
    }
}
