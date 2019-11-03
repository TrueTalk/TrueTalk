
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class Achronym : Text
    {
        private Achronym( ) : base( )
        { }

        protected Achronym( String value ) : base( value )
        { }

        public static Achronym New( String value )
        {
            return new Achronym( value );
        }

        //--//

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Achronym('{0}')", this.Value );
        }
    }
}
