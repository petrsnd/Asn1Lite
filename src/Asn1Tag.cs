﻿namespace Petrsnd.Asn1Lite
{
    public enum Asn1TypeClass : byte
    {
        Universal = 0,
        Application = 1,
        ContextSpecific = 2,
        Private = 3
    }

    public enum Asn1UniversalTagNumber
    {
        EndOfContent = 0,
        Boolean = 1,
        Integer = 2,
        BitString = 3,
        OctetString = 4,
        Null = 5,
        ObjectIdentifier = 6,
        ObjectDescriptor = 7,
        External = 8,
        Real = 9,
        Enumerated = 10,
        EmbeddedPdv = 11,
        Utf8String = 12,
        RelativeOid = 13,
        Sequence = 16,
        Set = 17,
        NumericString = 18,
        PrintableString = 19,
        T61String = 20,
        VideotexString = 21,
        Ia5String = 22,
        UtcTime = 23,
        GeneralizedTime = 24,
        GraphicString = 25,
        VisibleString = 26,
        GeneralString = 27,
        UniversalString = 28,
        CharacterString = 29,
        BmpString = 30
    }
}
