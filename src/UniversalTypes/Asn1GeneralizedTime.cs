using System;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1GeneralizedTime : Asn1TimeBase
    {
        public Asn1GeneralizedTime(DateTime value) : base((int)Asn1UniversalTagNumber.GeneralizedTime, value)
        {
        }
    }
}
