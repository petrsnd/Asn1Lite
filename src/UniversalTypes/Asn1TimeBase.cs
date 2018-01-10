using System;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public class Asn1TimeBase : Asn1Object
    {
        public Asn1TimeBase(int tag, DateTime value) : base(tag)
        {
            Value = value;
        }

        public DateTime Value { get; }
    }
}
