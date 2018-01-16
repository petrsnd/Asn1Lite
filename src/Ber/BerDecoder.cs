using Petrsnd.Asn1Lite.Ber.Decoders;
using Petrsnd.Asn1Lite.TaggedTypes;

namespace Petrsnd.Asn1Lite.Ber
{
    public static class BerDecoder
    {
        public static Asn1Object DecodeUniversalType(Asn1UniversalTagNumber expected, byte[] data, ref int index)
        {
            var tag = BerReader.ReadTag(data, ref index);
            if (tag.TypeClass != Asn1TypeClass.Universal)
                throw new BerParseException(
                    $"Decoder expected type class ({Asn1TypeClass.Universal}), received ({tag.TypeClass})");
            var universalTag = (Asn1UniversalTagNumber) (tag.TagNumber & 0x1f);
            if (universalTag != expected)
                throw new BerParseException(
                    $"Decoder expected universal tag number ({universalTag}), received ({expected})");
            var length = BerReader.ReadLength(data, ref index);
            return BerUniversalDecoder.Decode(tag, length, data, ref index);
        }

        public static Asn1Object DecodeUniversalType(Asn1Object expected, byte[] data, ref int index)
        {
            return DecodeUniversalType((Asn1UniversalTagNumber) (expected.Tag & 0x1f), data, ref index);
        }

        public static Asn1Object DecodeUniversalType(Asn1Object expected, byte[] data)
        {
            var localIndex = 0;
            return DecodeUniversalType(expected, data, ref localIndex);
        }

        public static Asn1ExplicitType DecodeExplicitType(Asn1ExplicitType expected, byte[] data, ref int index)
        {
            // TODO
            return null;
        }

        public static Asn1ExplicitType DecodeExplicitType(Asn1ExplicitType expected, byte[] data)
        {
            var localIndex = 0;
            return DecodeExplicitType(expected, data, ref localIndex);
        }

        public static Asn1ImplicitType DecodeImplicitType(Asn1ImplicitType expected, byte[] data, ref int index)
        {
            // TODO
            return null;
        }

        public static Asn1ImplicitType DecodeImplicitType(Asn1ImplicitType expected, byte[] data)
        {
            var localIndex = 0;
            return DecodeImplicitType(expected, data, ref localIndex);
        }

        public static Asn1Object Decode(byte[] data, ref int index)
        {
            // TODO
            return null;
        }

        public static Asn1Object Decode(byte[] data)
        {
            var localIndex = 0;
            return Decode(data, ref localIndex);
        }
    }
}
