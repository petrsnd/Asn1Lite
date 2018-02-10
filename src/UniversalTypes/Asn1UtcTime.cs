using System;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1UtcTime : Asn1TimeBase
    {
        public Asn1UtcTime(DateTime value) : base((int)Asn1UniversalTagNumber.UtcTime, value)
        {
        }

        public override string ValueAsString => Value.ToUniversalTime().ToString("yyMMddHHmmssZ");
    }
}
