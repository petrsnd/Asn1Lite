using System.Text;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1PrintableString : Asn1StringBase
    {
        public Asn1PrintableString() : base((int)Asn1UniversalTagNumber.PrintableString)
        {
        }

        public Asn1PrintableString(byte[] value) : base((int)Asn1UniversalTagNumber.PrintableString, value)
        {
        }

        public override string ValueAsString => Encoding.ASCII.GetString(Value);
    }
}
