using System.Text;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Ia5String : Asn1StringBase
    {
        public Asn1Ia5String() : base((int)Asn1UniversalTagNumber.Ia5String)
        {
        }

        public Asn1Ia5String(byte[] value) : base((int)Asn1UniversalTagNumber.Ia5String, value)
        {
        }

        public override string ValueAsString => Encoding.ASCII.GetString(Value);
    }
}
