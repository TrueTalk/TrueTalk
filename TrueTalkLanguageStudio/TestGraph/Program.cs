
namespace TrueTalk.TestGraph
{
    using TrueTalk.Analysis;
    using TrueTalk.Common;
    using TrueTalk.Speech.Grammar;
    using TrueTalk.SpeechRepresentation;

    class Program
    {
        static void Main( )
        {
            var modelPath = @"C:\src\TrueTalk\TrueTalkLanguageStudio\Resources\Models\englishPCFG.ser.gz";

            var factory = new ClauseGraphFactory( modelPath );

            var clause = factory.FromString( "If temperature is higher than 57 and 7 + 2 = 9, then open the windows." );

            clause.Graph.GrammaticalStructure.Display( );
            clause.Graph.PhrasalStructure    .Display( );

            var speechAnalysis = new SpeechAnalysis( );

            //clause.Graph.GrammaticalStructure.ApplyTransformation( speechAnalysis );
            clause.Graph.PhrasalStructure    .ApplyTransformation( speechAnalysis );

            //--//

            var natural1 = Number.New("72");
            CHECKS.ASSERT( natural1.Value == 72, "Failed to parse natural number." );
            CHECKS.ASSERT( natural1.KindOfNumber == Number.NumberKind.Natural, "Failed to parse natural number." );

            try
            {
                var natural2 = Number.New("72.32");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-natural number as a natural number." );
            }
            catch { }

            try
            {
                var natural3 = Number.New("-72");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-natural number as a natural number." );
            }
            catch { }

            try
            {
                var natural4 = Number.New("72Bar");

                CHECKS.ASSERT( false, "Failed throw exception on parsing badly formatted natural number." );
            }
            catch { }

            try
            {
                var natural2 = Number.New("0.72");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-natural number as a natural number." );
            }
            catch { }

            //--//

            var integer1 = Number.New("72");
            CHECKS.ASSERT( integer1.Value == 72, "Failed to parse integer number." );
            CHECKS.ASSERT( integer1.KindOfNumber == Number.NumberKind.Natural, "Failed to parse integer number." );

            try
            {
                var integer2 = Number.New("72.32");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-integer number as an integer number." );
            }
            catch { }

            var integer3 = Number.New("-72");
            CHECKS.ASSERT( integer3.Value == -72, "Failed to parse integer number." );
            CHECKS.ASSERT( integer3.KindOfNumber == Number.NumberKind.Integer, "Failed to parse integer number." );

            try
            {
                var integer4 = Number.New("72Bar");

                CHECKS.ASSERT( false, "Failed throw exception on parsing badly formatted integer number." );
            }
            catch { }

            try
            {
                var integer5 = Number.New("0.72");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-integer number as an integer number." );
            }
            catch { }

            //--//

            var double1 = Number.New("72");
            CHECKS.ASSERT( double1.Value == 72, "Failed to parse natural number." );
            CHECKS.ASSERT( double1.KindOfNumber == Number.NumberKind.Natural, "Failed to parse natural number." );
            var double2 = Number.New("72.32");
            CHECKS.ASSERT( double2.Value == 72.32, "Failed to parse double number." );
            CHECKS.ASSERT( double2.KindOfNumber == Number.NumberKind.Real, "Failed to parse real number." );
            var double3 = Number.New("-72");
            CHECKS.ASSERT( double3.Value == -72, "Failed to parse double number." );
            CHECKS.ASSERT( double3.KindOfNumber == Number.NumberKind.Integer, "Failed to parse real number." );

            try
            {
                var double4 = Number.New("72Bar");

                CHECKS.ASSERT( false, "Failed throw exception on parsing badly formatted double number." );
            }
            catch { }

            var double5 = Number.New("0.72");
            CHECKS.ASSERT( double5.Value == 0.72, "Failed to parse double number." );
            CHECKS.ASSERT( double5.KindOfNumber == Number.NumberKind.Real, "Failed to parse real number." );

            //--//

            var openParenthesis = MathematicalSymbol.New("(");
            CHECKS.ASSERT( openParenthesis.Value == "(", "Failed to parse open bracket '('." );
            CHECKS.ASSERT( openParenthesis.KindOfMathematicalSymbol == MathematicalSymbol.MathematicalSymbolKind.Parenthesis_Open, "Failed to parse open bracket '('." );

            var closedParenthesis = MathematicalSymbol.New(")");
            CHECKS.ASSERT( closedParenthesis.Value == ")", "Failed to parse open bracket ')'." );
            CHECKS.ASSERT( closedParenthesis.KindOfMathematicalSymbol == MathematicalSymbol.MathematicalSymbolKind.Parenthesis_Close, "Failed to parse close bracket ')'." );

            //--//

            var plus = MathematicalSymbol.New("+");
            CHECKS.ASSERT( plus.Value == "+", "Failed to parse open bracket '+'." );
            CHECKS.ASSERT( plus.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse plus symbol '+'." );

            var minus = MathematicalSymbol.New("-");
            CHECKS.ASSERT( minus.Value == "-", "Failed to parse open bracket '-'." );
            CHECKS.ASSERT( minus.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse minus symbol '-'." );

            var multipliedBy = MathematicalSymbol.New("*");
            CHECKS.ASSERT( multipliedBy.Value == "*", "Failed to parse open bracket '*'." );
            CHECKS.ASSERT( multipliedBy.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse MltipliedBy symbol '*'." );

            var dividedBy = MathematicalSymbol.New("/");
            CHECKS.ASSERT( dividedBy.Value == "/", "Failed to parse open bracket '/'." );
            CHECKS.ASSERT( dividedBy.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse dividedBy symbol '/'." );

            //--//

            var word = Word.New("Foo");
            CHECKS.ASSERT( word.Value == "Foo", "Failed to parse word 'Foo'." );
            CHECKS.ASSERT( word.KindOfToken == Token.TokenKind.Alpha, "Failed to parse a word." );

            //--//


        }
    }
}
