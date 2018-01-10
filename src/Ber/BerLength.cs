using System.Collections.Generic;
using System.Linq;

namespace Petrsnd.Asn1Lite.Ber
{
    public class BerLength
    {
        public static BerLength Indefinite()
        {
            return new BerLength(new byte[] { 0x80 });
        }

        private readonly byte[] _lengthData;

        public BerLength(byte[] lengthData)
        {
            if (lengthData == null || lengthData.Length < 1)
                throw new BerParseException("Parser BER length data cannot be empty or null");
            _lengthData = lengthData;
        }

        public BerLength(int length)
        {
            if (length < 0x80)
            {
                _lengthData = new[] { (byte)length };
            }
            else
            {
                var lengthDataBuilder = new List<byte>();
                while (length > 0)
                {
                    lengthDataBuilder.Add((byte)(length & 0xff));
                    length = length >> 8;
                }
                lengthDataBuilder.Add((byte)(0x80 + lengthDataBuilder.Count));
                lengthDataBuilder.Reverse();
                _lengthData = lengthDataBuilder.ToArray();
            }
        }

        public bool IsShortForm => (_lengthData[0] & 0x80) == 0;

        public bool IsIndefinite => _lengthData.Length == 1 && _lengthData[0] == 0x80;

        public int Length
        {
            get
            {
                if (IsIndefinite)
                    return -1;
                if (IsShortForm)
                    return _lengthData[0];
                var length = 0;
                for (var i = 1; i <= (_lengthData[0] & 0x7f); i++)
                {
                    length = length << 8;
                    length += _lengthData[0];
                }
                return length;
            }
        }

        public byte[] LengthData => _lengthData;
    }
}
