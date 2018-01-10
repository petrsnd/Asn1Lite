using System.Collections.Generic;

namespace Petrsnd.Asn1Lite.UniversalTypes
{
    public abstract class Asn1StructuredTypeBase : Asn1Object
    {
        private readonly List<Asn1Object> _value;

        protected Asn1StructuredTypeBase(int tag, Asn1Object[] value) : base(tag)
        {
            _value = new List<Asn1Object>(value);
        }

        public Asn1Object[] Value => _value.ToArray();

        public void Add(Asn1Object obj)
        {
            if (obj != null)
                _value.Add(obj);
        }
    }
}
