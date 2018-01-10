using System.Linq;
using Petrsnd.Asn1Lite.TaggedTypes;
using Petrsnd.Asn1Lite.UniversalTypes;

namespace Petrsnd.Asn1Lite.Ber
{
    public static class BerDecoder
    {
        private static BerTag PeekTag(byte[] data, int index)
        {
            var localIndex = index;
            return ReadTag(data, ref localIndex);
        }

        private static BerTag ReadTag(byte[] data, ref int index)
        {
            if (index >= data.Length)
                throw new BerParseException("Parser index out of bounds while reading tag");
            var tag = new BerTag(new[] { data[index] });
            if (tag.IsLowForm)
            {
                index++;
                return tag;
            }
            for (var i = 1; i < data.Length; i++)
            {
                if (index + i >= data.Length)
                    throw new BerParseException("Parser index out of bounds while reading high form tag");
                if ((data[index + i] & 0x80) != 0)
                    continue;
                tag = new BerTag(data.Skip(index).Take(i).ToArray());
                index += 1;
                return tag;
            }
            throw new BerParseException("Parser unable to determine tag");
        }

        private static BerLength PeekLength(byte[] data, int index)
        {
            var localIndex = index;
            return ReadLength(data, ref localIndex);
        }

        private static BerLength ReadLength(byte[] data, ref int index)
        {
            if (index >= data.Length)
                throw new BerParseException("Parser index out of bounds while reading length");
            var len = new BerLength(new[] { data[index] });
            if (len.IsIndefinite || len.IsShortForm)
            {
                index++;
                return len;
            }
            var numBytes = data[0] & 0x7f;
            if (index + numBytes >= data.Length)
                throw new BerParseException("Parser index out of bounds while reading long form length");
            len = new BerLength(data.Skip(index).Take(numBytes + 1).ToArray());
            index += (numBytes + 1);
            return len;
        }

        public static Asn1Object DecodeUniversalType(Asn1UniversalTagNumber expected, byte[] data, ref int index)
        {
            var tag = ReadTag(data, ref index);
            if (tag.TypeClass != Asn1TypeClass.Universal)
                throw new BerParseException(
                    $"Expected type class ({Asn1TypeClass.Universal}), received ({tag.TypeClass})");
            var universalTag = (Asn1UniversalTagNumber) (tag.TagNumber & 0x1f);
            if (universalTag != expected)
                throw new BerParseException(
                    $"Expected universal tag number ({universalTag}), received ({expected})");
            BerLength length = ReadLength(data, ref index);
            switch (universalTag)
            {
                case Asn1UniversalTagNumber.EndOfContents:
                    return new Asn1EndOfContents();
                case Asn1UniversalTagNumber.Boolean:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Boolean);
                case Asn1UniversalTagNumber.Integer:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Integer);
                case Asn1UniversalTagNumber.BitString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.BitString);
                case Asn1UniversalTagNumber.OctetString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.OctetString);
                case Asn1UniversalTagNumber.Null:
                    return new Asn1Null();
                case Asn1UniversalTagNumber.ObjectIdentifier:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.ObjectIdentifier);
                case Asn1UniversalTagNumber.ObjectDescriptor:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.ObjectDescriptor);
                case Asn1UniversalTagNumber.External:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.External);
                case Asn1UniversalTagNumber.Real:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Real);
                case Asn1UniversalTagNumber.Enumerated:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Enumerated);
                case Asn1UniversalTagNumber.EmbeddedPdv:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.EmbeddedPdv);
                case Asn1UniversalTagNumber.Utf8String:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Utf8String);
                case Asn1UniversalTagNumber.RelativeOid:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.RelativeOid);
                case Asn1UniversalTagNumber.Sequence:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Sequence);
                case Asn1UniversalTagNumber.Set:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Set);
                case Asn1UniversalTagNumber.NumericString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.NumericString);
                case Asn1UniversalTagNumber.PrintableString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.PrintableString);
                case Asn1UniversalTagNumber.T61String:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.T61String);
                case Asn1UniversalTagNumber.VideotexString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.VideotexString);
                case Asn1UniversalTagNumber.Ia5String:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.Ia5String);
                case Asn1UniversalTagNumber.UtcTime:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.UtcTime);
                case Asn1UniversalTagNumber.GeneralizedTime:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.GeneralizedTime);
                case Asn1UniversalTagNumber.GraphicString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.GraphicString);
                case Asn1UniversalTagNumber.VisibleString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.VisibleString);
                case Asn1UniversalTagNumber.GeneralString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.GeneralString);
                case Asn1UniversalTagNumber.UniversalString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.UniversalString);
                case Asn1UniversalTagNumber.CharacterString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.CharacterString);
                case Asn1UniversalTagNumber.BmpString:
                    throw new BerParseNotImplementedException(Asn1UniversalTagNumber.BmpString);
                default:
                    throw new BerParseException($"Unknown universal tag number ({tag})");
            }
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
