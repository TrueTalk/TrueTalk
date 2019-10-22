

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class Minus : MathematicalSymbol
    {
        public static readonly string MinusRepresentation = "-";

        //--//

        private Minus( String value ) : base( value )
        { }

        //--//

        public static Minus New( String value )
        {
            if( value.Equals( MinusRepresentation ) == false )
            {
                throw new ArgumentException( $"Minus value {value} does not match required represenation {MinusRepresentation}." );
            }

            return new Minus( value );
        }

        //--//

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Minus('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
