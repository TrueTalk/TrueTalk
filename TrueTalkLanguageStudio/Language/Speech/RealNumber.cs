

namespace TrueTalk.Speech
{
    using System;
    using System.Text;

    public sealed class RealNumber : Number
    {
        private RealNumber( String value ) : base( value, DigitKind.Integer )
        { }

        //--//

        public static RealNumber New( String value )
        {
            return new RealNumber( value );
        }

        //--//
        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Real('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
