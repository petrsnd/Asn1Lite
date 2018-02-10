using System;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerGeneralizedTimeDecoder : BerTimeDecoderBase, IBerUniversalDecoder
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.GeneralizedTime;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var timeString = DecodeTimeContents(tag, length, data, ref index);
            if (timeString.Length < 10)
                throw new BerParseException("Decoder validation: Generalized time should have at least 10 characters for YYMMDDhhmm");
            var formattedTimeString =
                $"{timeString.Substring(0, 4)}-{timeString.Substring(4, 2)}-{timeString.Substring(6, 2)}T{timeString.Substring(8, 2)}";
            if (timeString.Length >= 12)
                formattedTimeString += $":{timeString.Substring(10, 2)}";
            if (timeString.Length >= 14)
                formattedTimeString += $":{timeString.Substring(12, 2)}";
            if (timeString.Length > 14)
                formattedTimeString += timeString.Substring(14);
            try
            {
                return new Asn1GeneralizedTime(DateTime.Parse(formattedTimeString));
            }
            catch (Exception e)
            {
                throw new BerParseException($"Decoder validation: Encoded '{timeString}' formatted as '{formattedTimeString}' could not be parsed", e);
            }
        }
    }
}
