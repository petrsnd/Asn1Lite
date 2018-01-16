using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal class BerEnumeratedEncoder : IBerUniversalDecoder 
    {
        public Asn1UniversalTagNumber GetTagNumber()
        {
            return Asn1UniversalTagNumber.Enumerated;
        }

        public Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if ((new BerIntegerDecoder()).Decode(tag, length, data, ref index) is Asn1Integer obj)
                return new Asn1Enumerated(obj.Value);
            throw new BerParseException("Failed to decode BER enumerated using BER integer decoder");
        }
    }
}
