﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Petrsnd.Asn1Lite.Ber
{
    public class BerLength
    {
        public static BerLength Read(byte[] data, ref int index)
        {
            if (index >= data.Length)
                throw new Exception("ASN.1 parsing exception, index out of bounds reading length");
            var len = new BerLength(new[] { data[index] });
            if (len.IsIndefinite || len.IsShortForm)
            {
                index++;
                return len;
            }
            var numBytes = data[0] & 0x7f;
            if (index + numBytes >= data.Length)
                throw new Exception("ASN.1 parsing exception, index out of bounds reading length");
            len = new BerLength(data.Skip(index).Take(numBytes + 1).ToArray());
            index += (numBytes + 1);
            return len;
        }

        public static BerLength Indefinite()
        {
            return new BerLength(new byte[] { 0x80 });
        }

        private readonly byte[] _lengthData;

        public BerLength(byte[] lengthData)
        {
            if (lengthData == null || lengthData.Length < 1)
                throw new Exception("ASN.1 parsing exception, length data cannot be empty or null--replace me with concrete type");
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
