using System;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerRealDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.Real;
        }

        private Asn1Real DecodeBase10(byte[] values)
        {
            try
            {
                var str = System.Text.Encoding.ASCII.GetString(values);
                return new Asn1Real(double.Parse(str));
            }
            catch (Exception e)
            {
                throw new BerParseException("Real decoder only supports naive decoding strategy for base 10 format for now", e);
            }
        }

        private Asn1Real DecodeBaseX(byte initial, byte[] values)
        {
            throw new BerParseException("Base 2, 8, 16 real parsing is not implemented yet");
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if (!tag.IsPrimitive)
                throw new BerParseException("Decoder validation: Real encoding must be primitive");
            var size = length.Length;
            if (size == 0)
                return new Asn1Real(0.0);
            var initial = BerReader.ReadByte(data, ref index);
            if (size == 1)
            {
                switch (initial)
                {
                    case 0x40: //01000000 == positive infinity
                        return new Asn1Real(double.PositiveInfinity);
                    case 0x41: //01000001 == negative infinity
                        return new Asn1Real(double.NegativeInfinity);
                    case 0x42: //01000010 == not a number
                        return new Asn1Real(double.NaN);
                    case 0x43: //01000011 == negative zero
                        return new Asn1Real(-0.0);
                }
                throw new BerParseException("Decoder validation: Real encoding must be longer than one byte except special values");
            }
            var values = BerReader.ReadData(length.Length - 1, data, ref index);
            if ((initial & 0x80) == 0x80)
                return DecodeBaseX(initial, values);
            if ((initial & 0x80) == 0x00 && (initial & 0x40) == 0x00)
                return DecodeBase10(values);
            throw new BerParseException("Decoder validation: Real encoding bit 8 and bit 7 are 01 but not a special value");
        }
    }
}
