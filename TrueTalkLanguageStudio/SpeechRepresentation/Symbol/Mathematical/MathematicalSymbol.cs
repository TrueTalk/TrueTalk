

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class MathematicalSymbol : Symbol
    {
        public static readonly string PlusRepresentation = "+";
        public static readonly string MinusRepresentation = "-";
        public static readonly string MultipliedByRepresentation = "*";
        public static readonly string DividedByRepresentation = "/";
        public static readonly string OpenParenthesisRepresentation  = "(";
        public static readonly string CloseParenthesisRepresentation = ")";

        //--//

        public enum MathematicalSymbolKind
        {
            Plus,
            Minus,
            DividedBy,
            MultipliedBy,
            Parenthesis_Open,
            Parenthesis_Close,
        }

        //--//

        private MathematicalSymbol( String value, MathematicalSymbolKind kind ) : base( value, SymbolKind.Mathematical )
        {
            KindOfMathematicalSymbol = kind;
        }

        //--//

        public static MathematicalSymbol New( string value )
        {
            MathematicalSymbol ms = default;

            switch( value )
            {
                case "+":
                    ms = new MathematicalSymbol( value, MathematicalSymbolKind.Plus );
                    break;

                case "-":
                    ms = new MathematicalSymbol( value, MathematicalSymbolKind.Minus );
                    break;

                case "*":
                    ms = new MathematicalSymbol( value, MathematicalSymbolKind.MultipliedBy );
                    break;

                case "/":
                    ms = new MathematicalSymbol( value, MathematicalSymbolKind.DividedBy );
                    break;

                case "(":
                    ms = new MathematicalSymbol( value, MathematicalSymbolKind.Parenthesis_Open );
                    break;

                case ")":
                    ms = new MathematicalSymbol( value, MathematicalSymbolKind.Parenthesis_Close );
                    break;

                default:
                    throw new ArgumentException( "Mathematical symbol {value} is not supported." );
            }

            return ms;
        }

        public MathematicalSymbolKind KindOfMathematicalSymbol { get; internal set; }

        //--//

        public override bool ApplyTransformation( IAnalysis analysis ) => throw new NotImplementedException( );

        //--//

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"MathematicalSymbol('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
