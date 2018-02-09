using System.Collections.Generic;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerObjectIdentifierDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.ObjectIdentifier;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if (!tag.IsPrimitive)
                throw new BerParseException("Decoder validation: Object identifier encoding must be primitive");
            var values = BerReader.ReadData(length.Length, data, ref index);
            var ints = new List<int>();
            var cur = 0;
            foreach (var value in values)
            {
                if (value == 0x80)
                    throw new BerParseException("Decoder validation: Object identifier leading octet may not be 0x80 to be least possible number of octets");
                cur = cur << 7;
                cur += (value & 0x7f);
                if ((value & 0x80) == 0x00)
                {
                    if (ints.Count != 0)
                        ints.Add(cur);
                    else
                    {
                        ints.Add(cur / 40);
                        ints.Add(cur % 40);
                    }
                    cur = 0;
                }
            }
            if (cur != 0)
                throw new BerParseException($"Decoder validation: Object identifier remainder was left where bit 8 was not set to zero: {cur}");
            return new Asn1ObjectIdentifier(ints.ToArray());
        }
    }
}
