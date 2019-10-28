

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Common;
    using TrueTalk.Interfaces;

    public sealed class Number : Token
    {
        public enum NumberKind
        {
            Natural,
            Integer,
            Rational,
            Real,
            Irrational,
            Algebraic,
            Trascendental,
            Imaginary,
            Complex,
        }

        //--//

        private Number( String rawValue, NumberKind kind ) : base( rawValue, TokenKind.Numeric )
        {
            if( Number.TryParse( rawValue, out Double result, out NumberKind kind1 ) == false )
            {
                throw new ArgumentException( $"Token '{rawValue}' could not be parsed as a natural number." );
            }

            if( kind == NumberKind.Natural )
            {
                CHECKS.ASSERT( kind == kind1, "Number kind does not match." );
            }
            else if( kind == NumberKind.Integer )
            {
                CHECKS.ASSERT( ( kind == kind1 ) || ( kind1 == NumberKind.Natural ), "Number kind does not match." );
            }

            Value        = result;
            KindOfNumber = kind;
        }

        //--//

        public static Number New( string rawValue )
        {
            if( Number.TryParse( rawValue, out double result, out Number.NumberKind kind ) == false )
            {
                throw new ArgumentException( $"Token '{rawValue}' could not be parsed as any kind of number." );
            }

            switch( kind )
            {
                case Number.NumberKind.Natural:
                case Number.NumberKind.Integer:
                case Number.NumberKind.Real   :
                    return new Number( rawValue, kind );

                default:
                    throw new ArgumentException( $"Number {result} of kind '{kind}' is not supported." );
            }
        }

        //--//

        public static bool TryParse( string rawValue, out Double result )
        {
            return Double.TryParse( rawValue, out result );
        }

        public static bool TryParse( string rawValue, out Double result, out NumberKind kind )
        {
            var res = TryParse( rawValue, out result );

            kind = res ? ( Math.Abs( result % 1 ) > 0 ? NumberKind.Real : ( result > 0 ) ? NumberKind.Natural : NumberKind.Integer ) : NumberKind.Irrational;

            return res;
        }

        //--//

        public NumberKind KindOfNumber { get; private set; }

        public Double Value { get; private set; }

        //--//

        public override bool ApplyTransformation( IAnalysis analysis ) => throw new NotImplementedException( );

        //--//

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( $"Number({this.KindOfNumber}:'{this.Value}')" );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
