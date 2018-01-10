using System.Collections;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1BitString : Asn1Object
    {
        public Asn1BitString(BitArray value) : base((int)Asn1UniversalTagNumber.BitString)
        {
            Value = value;
        }

        public BitArray Value { get; }
    }
}
