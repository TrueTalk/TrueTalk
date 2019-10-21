

namespace TrueTalk.SpeechRepresentation
{
    using System;

    public abstract class Number : Token
    {
        public enum DigitKind
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

        protected Number( String rawValue, DigitKind kind ) : base( rawValue, TokenKind.Numberic )
        {
            Value       = Parse( rawValue );
            KindOfDigit = kind;
        }

        //--//

        public static Double Parse( string rawValue )
        {
            if( Double.TryParse( rawValue, out double result ) )
            {
                return result;
            }

            throw new ArgumentException( "Value {rawvalue} cannot be parsed as a double-precision digit." );
        }

        public static Double Parse( string rawValue, out DigitKind kind )
        {
            var result = Parse( rawValue );

            kind = Math.Abs( result % 1 ) > 0 ? DigitKind.Real : ( result > 0 ) ? DigitKind.Integer : DigitKind.Natural;

            return result;
        }

        //--//

        public DigitKind KindOfDigit { get; private set; }

        public Double Value { get; private set; }

    }
}
