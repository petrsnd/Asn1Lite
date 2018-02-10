using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal static class BerUniversalDecoder
    {
        private static Dictionary<Asn1UniversalTagNumber, IBerUniversalDecoder> _registry;

        private static void CreateRegistry()
        {
            _registry = new Dictionary<Asn1UniversalTagNumber, IBerUniversalDecoder>();
            var assembly = Assembly.GetExecutingAssembly();
            var type = typeof(IBerUniversalDecoder);
            foreach (var decoderType in assembly.GetTypes().Where(c => type.IsAssignableFrom(c)))
            {
                if (Activator.CreateInstance(decoderType) is IBerUniversalDecoder decoder)
                    _registry.Add(decoder.GetTagNumber(), decoder);
            }
        }

        public static Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            if (_registry == null)
                CreateRegistry();
            var universalTag = (Asn1UniversalTagNumber)(tag.TagNumber & 0x1f);
            IBerUniversalDecoder decoder;
            try
            {
                if (_registry == null)
                    throw new BerParseException("Unable to set up BER decoder for universal class");
                decoder = _registry[universalTag];
            }
            catch (Exception ex)
            {
                throw new BerParseNotImplementedException(universalTag, ex);
            }
            return decoder.Decode(tag, length, data, ref index);
        }

        /*
         case Asn1UniversalTagNumber.ObjectDescriptor:
         case Asn1UniversalTagNumber.External:
         case Asn1UniversalTagNumber.EmbeddedPdv:
         case Asn1UniversalTagNumber.Sequence:
         case Asn1UniversalTagNumber.Set:
         case Asn1UniversalTagNumber.T61String:
         case Asn1UniversalTagNumber.VideotexString:
         case Asn1UniversalTagNumber.GeneralizedTime:
         case Asn1UniversalTagNumber.GraphicString:
         case Asn1UniversalTagNumber.GeneralString:
         case Asn1UniversalTagNumber.UniversalString:
         case Asn1UniversalTagNumber.CharacterString:
         case Asn1UniversalTagNumber.BmpString:
         */
    }
}
