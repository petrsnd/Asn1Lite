using System;
using System.Linq;

namespace Petrsnd.Asn1Lite.Ber
{
    internal static class BerReader
    {
        public static BerTag PeekTag(byte[] data, int index)
        {
            var localIndex = index;
            return ReadTag(data, ref localIndex);
        }

        public static BerTag ReadTag(byte[] data, ref int index)
        {
            if (index >= data.Length)
                throw new BerParseException("Decoder index out of bounds while reading tag");
            var tag = new BerTag(new[] { data[index] });
            if (tag.IsLowForm)
            {
                index++;
                return tag;
            }
            for (var i = 1; i < data.Length; i++)
            {
                if (index + i >= data.Length)
                    throw new BerParseException("Decoder index out of bounds while reading high form tag");
                if ((data[index + i] & 0x80) != 0)
                    continue;
                tag = new BerTag(data.Skip(index).Take(i).ToArray());
                index += 1;
                return tag;
            }
            throw new BerParseException("Decoder unable to determine tag");
        }

        public static BerLength PeekLength(byte[] data, int index)
        {
            var localIndex = index;
            return ReadLength(data, ref localIndex);
        }

        public static BerLength ReadLength(byte[] data, ref int index)
        {
            if (index >= data.Length)
                throw new BerParseException("Decoder index out of bounds while reading length");
            var len = new BerLength(new[] { data[index] });
            if (len.IsIndefinite || len.IsShortForm)
            {
                index++;
                return len;
            }
            var numBytes = data[0] & 0x7f;
            if (index + numBytes >= data.Length)
                throw new BerParseException("Decoder index out of bounds while reading long form length");
            len = new BerLength(data.Skip(index).Take(numBytes + 1).ToArray());
            index += (numBytes + 1);
            return len;
        }

        public static void PeekData(int numBytes, byte[] data, int index)
        {
            if (index >= data.Length)
                throw new BerParseException("Decoder index out of bounds while reading data");
            if (index + numBytes >= data.Length)
                throw new BerParseException($"Decoder index out of bounds while reading data, numBytes = {numBytes}");
        }

        public static byte[] ReadData(int numBytes, byte[] data, ref int index)
        {
            PeekData(numBytes, data, index);
            try
            {
                var bytesRead = data.Skip(index).Take(numBytes).ToArray();
                index += numBytes;
                return bytesRead;
            }
            catch (Exception ex)
            {
                throw new BerParseException("Decoder error reading from data array", ex);
            }
        }
    }
}
