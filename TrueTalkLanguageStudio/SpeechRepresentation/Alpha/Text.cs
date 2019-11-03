
namespace TrueTalk.SpeechRepresentation
{
    using System;

    public abstract class Text : Token
    {
        protected Text( ) : base( )
        { }

        protected Text( String value ) : base( value, TokenKind.Alpha )
        { }

        //--//

        public String Value => base.RawValue;
    }
}
