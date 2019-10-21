
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public abstract class Text : Token
    {
        protected Text( String value ) : base( value, TokenKind.Alpha )
        { }

        //--//

        public String Value => base.RawValue;
    }
}
