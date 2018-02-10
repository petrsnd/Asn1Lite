using System.Text;

namespace Petrsnd.Asn1Lite.Ber.Decoders
{
    internal abstract class BerTimeDecoderBase
    {
        protected string DecodeTimeContents(BerTag tag, BerLength length, byte[] data, ref int index)
        {
            var bytes = BerReader.ReadData(length.Length, data, ref index);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
