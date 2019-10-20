
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;

    public class Word : Token
    {
        protected Word( String value, TokenKind type ) : base( value, type )
        { }

        public static Word New( String value )
        {
            return new Word( value, Token.TokenKind.Word );
        }

        //--//

        public String Value => base.RawValue;

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Word('{0}')", this.Value );
        }
    }
}
