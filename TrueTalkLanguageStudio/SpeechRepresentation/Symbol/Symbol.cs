

namespace TrueTalk.SpeechRepresentation
{
    using System;

    public abstract class Symbol : Token
    {
        //
        // Mathematical symbols: https://en.wikipedia.org/wiki/List_of_mathematical_symbols#Basic_symbols
        //
        public enum SymbolKind
        {
            Mathematical,
        }

        //--//

        protected Symbol( ) : base( )
        {
        }

        protected Symbol( String rawValue, SymbolKind kind ) : base( rawValue, TokenKind.Symbol )
        {
            KindOfSymbol = kind;
        }

        //--//

        public SymbolKind KindOfSymbol { get; set; }

        public String Value => RawValue;
    }
}
