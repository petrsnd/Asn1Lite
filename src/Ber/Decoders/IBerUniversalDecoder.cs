namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal interface IBerUniversalDecoder
    {
        Asn1UniversalTagNumber GetTagNumber();

        Asn1Object Decode(BerTag tag, BerLength length, byte[] data, ref int index);
    }
}
