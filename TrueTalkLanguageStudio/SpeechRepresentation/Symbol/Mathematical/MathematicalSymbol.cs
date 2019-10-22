

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public abstract class MathematicalSymbol : Symbol
    {
        protected MathematicalSymbol( String value ) : base( value, SymbolKind.Mathematical )
        { }
    }
}
