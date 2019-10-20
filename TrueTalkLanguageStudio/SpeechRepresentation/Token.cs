
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public class Token : TransformableItem
    {
        public enum TokenKind
        {
            Digit,
            Word,
            Mark
        }

        //--//

        protected Token(String value, TokenKind kind ) : base( )
        {
            RawValue    = value;
            KindOfToken = kind;
        }

        //--//

        public TokenKind KindOfToken  { get; private set; }

        public String RawValue { get; private set; }

        //--//

        //////public override void InnerToString( StringBuilder sb )
        //////{
        //////    base.InnerToString( sb );

        //////    sb.AppendFormat( @"{0}:'{1}'", this.KindOfToken, this.RawValue );
        //////}
    }
}
