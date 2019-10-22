
namespace TrueTalk.TestGraph
{
    using TrueTalk.Common;
    using TrueTalk.SpeechRepresentation;

    class Program
    {
        static void Main( )
        {
            //////var modelPath = @"C:\src\TrueTalk\TrueTalkLanguageStudio\Resources\Models\englishPCFG.ser.gz";

            //////var factory = new ClauseGraphFactory( modelPath );

            //////var clause = factory.FromString( "The big brown dog jumped over the lazy fox." );

            //////clause.Graph.GrammaticalStructure.Display( );
            //////clause.Graph.PhrasalStructure    .Display( );

            //--//

            var natural1 = NaturalNumber.New("72");
            CHECKS.ASSERT( natural1.Value == 72, "Failed to parse natural number." );
            CHECKS.ASSERT( natural1.KindOfNumber == Number.NumberKind.Natural, "Failed to parse natural number." );

            try
            {
                var natural2 = NaturalNumber.New("72.32");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-natural number as a natural number." );
            }
            catch { }

            try
            {
                var natural3 = NaturalNumber.New("-72");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-natural number as a natural number." );
            }
            catch { }

            try
            {
                var natural4 = NaturalNumber.New("72Bar");

                CHECKS.ASSERT( false, "Failed throw exception on parsing badly formatted natural number." );
            }
            catch { }

            try
            {
                var natural2 = NaturalNumber.New("0.72");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-natural number as a natural number." );
            }
            catch { }

            //--//

            var integer1 = IntegerNumber.New("72");
            CHECKS.ASSERT( integer1.Value == 72, "Failed to parse integer number." );
            CHECKS.ASSERT( integer1.KindOfNumber == Number.NumberKind.Integer, "Failed to parse integer number." );

            try
            {
                var integer2 = IntegerNumber.New("72.32");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-integer number as an integer number." );
            }
            catch { }

            var integer3 = IntegerNumber.New("-72");
            CHECKS.ASSERT( integer3.Value == -72, "Failed to parse integer number." );
            CHECKS.ASSERT( integer3.KindOfNumber == Number.NumberKind.Integer, "Failed to parse integer number." );

            try
            {
                var integer4 = IntegerNumber.New("72Bar");

                CHECKS.ASSERT( false, "Failed throw exception on parsing badly formatted integer number." );
            }
            catch { }

            try
            {
                var integer5 = IntegerNumber.New("0.72");

                CHECKS.ASSERT( false, "Failed throw exception on parsing a non-integer number as an integer number." );
            }
            catch { }

            //--//

            var double1 = RealNumber.New("72");
            CHECKS.ASSERT( double1.Value == 72, "Failed to parse double number." );
            CHECKS.ASSERT( double1.KindOfNumber == Number.NumberKind.Real, "Failed to parse real number." );
            var double2 = RealNumber.New("72.32");
            CHECKS.ASSERT( double2.Value == 72.32, "Failed to parse double number." );
            CHECKS.ASSERT( double2.KindOfNumber == Number.NumberKind.Real, "Failed to parse real number." );
            var double3 = RealNumber.New("-72");
            CHECKS.ASSERT( double3.Value == -72, "Failed to parse double number." );
            CHECKS.ASSERT( double3.KindOfNumber == Number.NumberKind.Real, "Failed to parse real number." );

            try
            {
                var double4 = RealNumber.New("72Bar");

                CHECKS.ASSERT( false, "Failed throw exception on parsing badly formatted double number." );
            }
            catch { }

            var double5 = RealNumber.New("0.72");
            CHECKS.ASSERT( double5.Value == 0.72, "Failed to parse double number." );
            CHECKS.ASSERT( double5.KindOfNumber == Number.NumberKind.Real, "Failed to parse real number." );

            //--//

            var openParenthesis = Bracket.New("(");
            CHECKS.ASSERT( openParenthesis.Value == "(", "Failed to parse open bracket '('." );
            CHECKS.ASSERT( openParenthesis.KindOfBracket == Bracket.BracketKind.Parenthesis_Open, "Failed to parse open bracket '('." );

            var openParenthesis1 = MathematicalSymbolFactory.NewMathematicalSymbol("(");
            CHECKS.ASSERT( openParenthesis1.Value == "(", "Failed to parse open bracket '('." );
            CHECKS.ASSERT( openParenthesis1.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse open bracket '('." );

            var closedParenthesis = Bracket.New(")");
            CHECKS.ASSERT( closedParenthesis.Value == ")", "Failed to parse open bracket ')'." );
            CHECKS.ASSERT( closedParenthesis.KindOfBracket == Bracket.BracketKind.Parenthesis_Close, "Failed to parse close bracket ')'." );

            var closedParenthesis1 = MathematicalSymbolFactory.NewMathematicalSymbol(")");
            CHECKS.ASSERT( closedParenthesis1.Value == ")", "Failed to parse open bracket ')'." );
            CHECKS.ASSERT( openParenthesis1.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse open bracket ')'." );

            //--//

            var plus = Plus.New("+");
            CHECKS.ASSERT( plus.Value == "+", "Failed to parse open bracket '+'." );
            CHECKS.ASSERT( plus.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse plus symbol '+'." );
            CHECKS.ASSERT( typeof( Plus ) == plus.GetType( ), "Failed to generate correct type of plus symbol '+'." );

            var plus1 = MathematicalSymbolFactory.NewMathematicalSymbol("+");
            CHECKS.ASSERT( plus1.Value == "+", "Failed to parse open bracket '+'." );
            CHECKS.ASSERT( plus1.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse plus symbol '+'." );
            CHECKS.ASSERT( typeof( Plus ) == plus1.GetType( ), "Failed to generate correct type of plus symbol '+'." );

            var minus = Minus.New("-");
            CHECKS.ASSERT( minus.Value == "-", "Failed to parse open bracket '-'." );
            CHECKS.ASSERT( minus.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse minus symbol '-'." );
            CHECKS.ASSERT( typeof( Minus ) == minus.GetType( ), "Failed to generate correct type of minus symbol '-'." );

            var minus1 = MathematicalSymbolFactory.NewMathematicalSymbol("-");
            CHECKS.ASSERT( minus1.Value == "-", "Failed to parse open bracket '-'." );
            CHECKS.ASSERT( minus1.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse minus symbol '+'." );
            CHECKS.ASSERT( typeof( Minus ) == minus1.GetType( ), "Failed to generate correct type of minus symbol '+'." );

            var multipliedBy = MultipliedBy.New("*");
            CHECKS.ASSERT( multipliedBy.Value == "*", "Failed to parse open bracket '*'." );
            CHECKS.ASSERT( multipliedBy.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse MltipliedBy symbol '*'." );
            CHECKS.ASSERT( typeof( MultipliedBy ) == multipliedBy.GetType( ), "Failed to generate correct type of MultipliedBy symbol '*'." );

            var multipliedBy1 = MathematicalSymbolFactory.NewMathematicalSymbol("*");
            CHECKS.ASSERT( multipliedBy1.Value == "*", "Failed to parse open bracket '*'." );
            CHECKS.ASSERT( multipliedBy1.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse MltipliedBy symbol '*'." );
            CHECKS.ASSERT( typeof( MultipliedBy ) == multipliedBy1.GetType( ), "Failed to generate correct type of MultipliedBy symbol '*'." );

            var dividedBy = DividedBy.New("/");
            CHECKS.ASSERT( dividedBy.Value == "/", "Failed to parse open bracket '/'." );
            CHECKS.ASSERT( dividedBy.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse dividedBy symbol '/'." );
            CHECKS.ASSERT( typeof( DividedBy ) == dividedBy.GetType( ), "Failed to generate correct type of dividedBy symbol '/'." );

            var dividedBy1 = MathematicalSymbolFactory.NewMathematicalSymbol("/");
            CHECKS.ASSERT( dividedBy1.Value == "/", "Failed to parse open bracket '/'." );
            CHECKS.ASSERT( dividedBy1.KindOfSymbol == Symbol.SymbolKind.Mathematical, "Failed to parse dividedBy symbol '/'." );
            CHECKS.ASSERT( typeof( DividedBy ) == dividedBy1.GetType( ), "Failed to generate correct type of dividedBy symbol '/'." );

            //--//

            var word = Word.New("Foo");
            CHECKS.ASSERT( word.Value == "Foo", "Failed to parse word 'Foo'." );
            CHECKS.ASSERT( word.KindOfToken == Token.TokenKind.Alpha, "Failed to parse a word." );

            //--//


        }
    }
}