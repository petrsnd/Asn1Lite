﻿namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1VisibleString : Asn1StringBase
    {
        public Asn1VisibleString(byte[] value) : base((int)Asn1UniversalTagNumber.VisibleString, value)
        {
        }
    }
}
