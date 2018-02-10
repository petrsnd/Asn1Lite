using System;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerUtcTimeDecoder : BerTimeDecoderBase, IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.UtcTime;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var timeString = DecodeTimeContents(tag, length, data, ref index);
            if (timeString.Length < 10)
                throw new BerParseException("Decoder validation: UTC time should have at least 10 characters for YYMMDDhhmm");
            var formattedTimeString = $"{timeString.Substring(0, 2)}-{timeString.Substring(2, 2)}-{timeString.Substring(4, 2)}T{timeString.Substring(6, 2)}:{timeString.Substring(8, 2)}";
            if (timeString.Length >= 12)
                formattedTimeString += $":{timeString.Substring(10, 2)}";
            if (timeString.Length > 12)
                formattedTimeString += timeString.Substring(12);
            try
            {
                return new Asn1UtcTime(DateTime.Parse(formattedTimeString));
            }
            catch (Exception e)
            {
                throw new BerParseException($"Decoder validation: Encoded '{timeString}' formatted as '{formattedTimeString}' could not be parsed", e);
            }
        }
    }
}
