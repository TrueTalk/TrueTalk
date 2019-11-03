
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    [Serializable]
    public sealed class Punctuation : Token
    {
        public enum PuctuationKind
        {
            Apostrophe,
            Colon,
            Comma,
            Dash,
            Exclamation,
            Hyphen,
            Period,
            QuestionMark,
            Semicolon,
            Quotation,
            Slash,
        }

        //--//

        private Punctuation( ) : base( )
        {
        }

        private Punctuation( String value, PuctuationKind kind, TokenKind type ) : base( value, type )
        {
            KindOfPunctuation = kind;
        }

        //--//

        public static PuctuationKind Parse( string rawValue )
        {
            switch( rawValue )
            {
                case "'": return PuctuationKind.Apostrophe;
                case ":": return PuctuationKind.Colon;
                case ",": return PuctuationKind.Comma;
                case "_": return PuctuationKind.Dash;
                case "!": return PuctuationKind.Exclamation;
                case "-": return PuctuationKind.Hyphen;
                case ".": return PuctuationKind.Period;
                case "?": return PuctuationKind.QuestionMark;
                case "`": return PuctuationKind.Quotation;
                case ";": return PuctuationKind.Semicolon;
                case "/": return PuctuationKind.Slash;
                default: throw new ArgumentException( "Value {rawvalue} cannot be parsed as a punctuation token." );
            }
        }

        public static Punctuation New( String rawValue )
        {
            var kind = Parse( rawValue );

            return new Punctuation( rawValue, kind, Token.TokenKind.Puctuation );
        }

        //--//

        public PuctuationKind KindOfPunctuation { get; set; }

        public String Value => base.RawValue;

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Puctuation('{0}')", this.Value );
        }
    }
}
