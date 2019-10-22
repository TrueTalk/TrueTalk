

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class RealNumber : Number
    {
        private RealNumber( String value ) : base( value, NumberKind.Real )
        { }

        //--//

        public static RealNumber New( String value )
        {
            return new RealNumber( value );
        }

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }

        //--//
        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Real('{0}')", this.Value );
        }
    }
}
