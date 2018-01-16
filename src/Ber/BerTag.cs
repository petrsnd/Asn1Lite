using System.Collections.Generic;

namespace Petrsnd.Asn1Lite.Ber
{
    internal class BerTag
    {
        private readonly byte[] _tagData;

        public BerTag(byte[] tagData)
        {
            if (tagData == null || tagData.Length < 1)
                throw new BerParseException("BER tag data cannot be empty or null");
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
