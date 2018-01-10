using System;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public abstract class Asn1TimeBase : Asn1Object
    {
        protected Asn1TimeBase(int tag, DateTime value) : base(tag)
        {
            Value = value;
        }

        public DateTime Value { get; }
    }
}
