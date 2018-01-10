﻿namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1Ia5String : Asn1StringBase
    {
        public Asn1Ia5String(byte[] value) : base((int)Asn1UniversalTagNumber.Ia5String, value)
        {
        }
    }
}
