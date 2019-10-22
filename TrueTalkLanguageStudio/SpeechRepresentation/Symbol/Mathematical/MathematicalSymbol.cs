

namespace TrueTalk.SpeechRepresentation
{
    using System;

    public abstract class MathematicalSymbol : Symbol
    {
        protected MathematicalSymbol( String value ) : base( value, SymbolKind.Mathematical )
        { }
    }
}
