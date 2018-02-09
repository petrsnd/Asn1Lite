using System.Text;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Utf8String : Asn1StringBase
    {
        public Asn1Utf8String() : base((int)Asn1UniversalTagNumber.Utf8String)
        {
        }

        public Asn1Utf8String(byte[] value) : base((int)Asn1UniversalTagNumber.Utf8String, value)
        {
        }

        public override string ValueAsString => Encoding.UTF8.GetString(Value);
    }
}
