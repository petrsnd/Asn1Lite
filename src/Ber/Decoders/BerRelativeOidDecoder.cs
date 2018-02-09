using System.Collections.Generic;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerRelativeOidDecoder : IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.ObjectIdentifier;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if (!tag.IsPrimitive)
                throw new BerParseException($"Decoder validation: Relative OID encoding must be primitive");
            var values = BerReader.ReadData(length.Length, data, ref index);
            var ints = new List<int>();
            var cur = 0;
            foreach (var value in values)
            {
                if (value == 0x80)
                    throw new BerParseException("Decoder validation: Relative OID leading octet may not be 0x80 to be least possible number of octets");
                cur = cur << 7;
                cur += (value & 0x7f);
                if ((value & 0x80) == 0x00)
                {
                    ints.Add(cur);
                    cur = 0;
                }
            }
            if (cur != 0)
                throw new BerParseException($"Decoder validation: Relative OIDs remainder was left where bit 8 was not set to zero: {cur}");
            return new Asn1RelativeOid(ints.ToArray());
        }
    }
}
