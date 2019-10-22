

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class Bracket : MathematicalSymbol
    {
        public enum BracketKind
        {
            Parenthesis_Open,
            Parenthesis_Close,
        }

        private Bracket( String value, BracketKind kind ) : base( value )
        {
            KindOfBracket = kind;
        }

        //--//

        public static Bracket New( String value )
        {
            BracketKind kind;

            switch( value )
            {
                case "(": kind = BracketKind.Parenthesis_Open; break;
                case ")": kind = BracketKind.Parenthesis_Close; break;
                default: throw new ArgumentException( $"Symbol '{value}' is not supported." );
            }

            return new Bracket( value, kind );
        }

        public static Bracket New( BracketKind kind )
        {
            Bracket br = default;

            switch( kind )
            {
                case BracketKind.Parenthesis_Open : 
                    br = new Bracket( "(", BracketKind.Parenthesis_Open );
                    break;

                case BracketKind.Parenthesis_Close:
                    br = new Bracket( ")", BracketKind.Parenthesis_Close );
                    break;

                default: 
                    throw new ArgumentException( $"Bracket kind '{kind}' is not supported." );
            }

            return br;
        }

        //--//

        public BracketKind KindOfBracket { get; internal set; }

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }

        //--//

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Bracket('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
