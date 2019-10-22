

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using TrueTalk.Common;

    public abstract class Number : Token
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

        protected Number( String rawValue, NumberKind kind ) : base( rawValue, TokenKind.Numeric )
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

    }
}
