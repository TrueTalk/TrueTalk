

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using TrueTalk.Common;

    public static class MathematicalSymbolFactory
    {
        public static MathematicalSymbol NewMathematicalSymbol( string rawValue )
        {
            MathematicalSymbol ms = default;

            switch( rawValue )
            {
                case "+":
                    ms = Plus.New( rawValue );
                    break;

                case "-":
                    ms = Minus.New( rawValue );
                    break;

                case "*":
                    ms = MultipliedBy.New( rawValue );
                    break;

                case "/":
                    ms = DividedBy.New( rawValue );
                    break;

                case "(":
                    ms = Bracket.New( Bracket.BracketKind.Parenthesis_Open );
                    break;

                case ")":
                    ms = Bracket.New( Bracket.BracketKind.Parenthesis_Close );
                    break;

                default:
                    CHECKS.ASSERT( false, "Mathematical symbol {rawValue} is not supported." );
                    break;
            }

            return ms;
        }
    }
}
