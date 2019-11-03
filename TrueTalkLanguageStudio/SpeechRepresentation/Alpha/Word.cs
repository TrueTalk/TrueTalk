
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    [Serializable]
    public sealed class Word : Text
    {
        private Word( ) : base( )
        { }

        private Word( String value ) : base( value )
        { }

        public static Word New( String value )
        {
            return new Word( value );
        }

        //--//

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Word('{0}')", this.Value );
        }
    }
}
