

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using TrueTalk.Common;

    public static class NumberFactory
    {
        public static Number NewNumber( string rawValue )
        {
            if( Number.TryParse( rawValue, out double result, out Number.NumberKind kind ) == false )
            {
                throw new ArgumentException( $"Token '{rawValue}' could not be parsed as any kind of number." );
            }

            Number n = default;

            switch( kind )
            {
                case Number.NumberKind.Natural:
                    n = NaturalNumber.New( rawValue );
                    break;

                case Number.NumberKind.Integer:
                    n = IntegerNumber.New( rawValue );
                    break;

                case Number.NumberKind.Real:
                    n = RealNumber.New( rawValue );
                    break;

                default:
                    CHECKS.ASSERT( false, "Number kind {kind} is not supported." );
                    break;
            }

            return n;
        }
    }
}
