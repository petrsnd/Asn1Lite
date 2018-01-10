using System;
using System.Collections.Generic;
using System.Linq;

namespace Petrsnd.Asn1Lite.Ber
{
    public class BerTag
    {
        public static BerTag Read(byte[] data, ref int index)
        {
            if (index >= data.Length)
                throw new Exception("ASN.1 parsing exception, index out of bounds reading tag");
            var tag = new BerTag(new[] { data[index] });
            if (tag.IsLowForm)
            {
                index++;
                return tag;
            }
            for (var i = 1; i < data.Length; i++)
            {
                if (index + i >= data.Length)
                    throw new Exception("ASN.1 parsing exception, index out of bounds reading tag");
                if ((data[index + i] & 0x80) != 0)
                    continue;
                tag = new BerTag(data.Skip(index).Take(i).ToArray());
                index += 1;
                return tag;
            }
            throw new Exception("ASN.1 parsing exception, can't find tag--replace me with concrete type");
        }

        private readonly byte[] _tagData;

        public BerTag(byte[] tagData)
        {
            if (tagData == null || tagData.Length < 1)
                throw new Exception("ASN.1 parsing exception, tag data cannot be empty or null--replace me with concrete type");
            _tagData = tagData;
        }

        public BerTag(Asn1TypeClass typeClass, BerEncodingType encodingType, int tagNumber)
        {
            var tagDataBuilder = new List<byte>();
            var initialByte = (((int)typeClass) << 6) + (((int)encodingType) << 5);
            if (tagNumber < 0x1f)
            {
                tagDataBuilder.Add((byte)(initialByte + tagNumber));
            }
            else
            {
                while (tagNumber > 0)
                {
                    var nextByte = tagNumber & 0x7f;
                    tagNumber = tagNumber >> 7;
                    if (tagNumber > 0)
                        nextByte += 0x80;
                    tagDataBuilder.Add((byte)nextByte);
                }
                tagDataBuilder.Add((byte)(initialByte + 0x1f));
                tagDataBuilder.Reverse();
            }
            _tagData = tagDataBuilder.ToArray();
        }

        public Asn1TypeClass TypeClass => (Asn1TypeClass)((_tagData[0] & 0xc0) >> 6);

        public BerEncodingType EncodingType => (BerEncodingType)((_tagData[0] & 0x20) >> 5);

        public bool IsPrimitive => (_tagData[0] & 0x20) != 0;

        public bool IsLowForm => (_tagData[0] & 0x1f) != 0x1f;

        public int TagNumber
        {
            get
            {
                if (IsLowForm)
                {
                    return _tagData[0] & 0x1f;
                }
                var tagNumber = 0;
                for (var i = 1; i < _tagData.Length; i++)
                {
                    tagNumber = tagNumber << 7;
                    tagNumber += (_tagData[i] & 0x7f);
                    if ((_tagData[i] & 0x80) == 0)
                        break;
                }
                return tagNumber;
            }
        }

        public byte[] TagData => _tagData;
    }
}
