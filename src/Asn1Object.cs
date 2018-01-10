namespace Petrsnd.Asn1Lite
{
    public class Asn1Object
    {
        public Asn1Object(int tag)
        {
            Tag = tag;
        }

        public int Tag { get; }
    }
}
