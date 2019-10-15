
namespace TrueTalk.Speech
{
    using System;
    using System.Text;

    public class Punctuation : Token
    {
        public enum PuctuationKind
        {
            Apostrophe,
            Colon,
            Comma,
            Dash,
            Ellipsis,
            Exclamation,
            Hyphen,
            Period,
            QuestionMark,
            Semicolon,
            Quotation,
            Slash,
        }

        //--//

        protected Punctuation( String value, PuctuationKind kind, TokenKind type ) : base( value, type )
        {
            KindOfPunctuation = kind;
        }

        //--//

        public static PuctuationKind Parse( string rawValue ) =>
            rawValue switch
            {
                "'" => PuctuationKind.Apostrophe,
                ":" => PuctuationKind.Colon,
                "," => PuctuationKind.Comma,
                "_" => PuctuationKind.Dash,
                "..." => PuctuationKind.Ellipsis,
                "!" => PuctuationKind.Exclamation,
                "-" => PuctuationKind.Hyphen,
                "." => PuctuationKind.Period,
                "?" => PuctuationKind.QuestionMark,
                "`" => PuctuationKind.Quotation,
                ";" => PuctuationKind.Semicolon,
                "/" => PuctuationKind.Slash,
                _   => throw new ArgumentException( "Value {rawvalue} cannot be parsed as a punctuation token." )
            };

        public static Punctuation New( String rawValue )
        {
            var kind = Parse( rawValue );

            return new Punctuation( rawValue, kind, Token.TokenKind.Mark );
        }

        //--//

        public PuctuationKind KindOfPunctuation { get; private set; }

        public String Value => base.RawValue;

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Puctuation('{0}')", this.Value );
        }
    }
}
