
namespace TrueTalk.TestGraph
{
    using System;
    using TrueTalk.Speech.Grammar;
    using TrueTalk.SpeechRepresentation;

    class Program
    {
        static void Main( )
        {
            var modelPath = @"C:\src\TrueTalk\TrueTalkLanguageStudio\Resources\Models\englishPCFG.ser.gz";

            var factory = new ClauseGraphFactory( modelPath );

            var clause = factory.FromString( "The big brown dog jumped over the lazy fox." );

            clause.GrammaticalStructure.Display( );
            clause.PhrasalStructure    .Display( );
        }
    }
}