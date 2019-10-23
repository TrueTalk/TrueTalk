
namespace TrueTalk.Analysis
{
    using System;
    using TrueTalk.Interfaces;
    using TrueTalk.Speech.Grammar;
    using TrueTalk.SpeechRepresentation;

    public class SpeechAnalysis : IAnalysis
    {
        public bool Apply( TransformableItem item )
        {
            var tokenGraph = (TokenGraph)item;

            bool fChanged = false;

            foreach( TokenGraph.Vertex v in tokenGraph.Vertexes.Values )
            {
                if( ( Token.IsPunctuation( v.Value ) == false ) && tokenGraph.IsTag( v ) )
                {
                    continue;
                }

                if( v.Token == null )
                {
                    fChanged = true;

                    Token tk = null;

                    if( Token.IsAlpha( v.Value ) )
                    {
                        tk = Word.New( v.Value );
                    }
                    else if( Token.IsNumeric( v.Value ) )
                    {
                        tk = Number.New( v.Value );
                    }
                    else if( Token.IsMathematicalSymbol( v.Value ) )
                    {
                        tk = MathematicalSymbol.New( v.Value );
                    }
                    else if( Token.IsPunctuation( v.Value ) )
                    {
                        tk = Punctuation.New( v.Value );
                    }
                    else
                    {
                        throw new ArgumentException( $"Token {v.Value} could not be convered to a Token." );
                    }

                    v.Token = tk;
                }
            }

            return fChanged;
        }
    }
}
