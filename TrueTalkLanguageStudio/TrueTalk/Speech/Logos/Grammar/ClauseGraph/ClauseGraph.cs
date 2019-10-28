


namespace TrueTalk.Speech.Grammar
{
    using System;
    using System.IO;

    public class ClauseGraph
    {
        public ClauseGraph( )
        {
        }

        public Clause Owner { get; internal set; } = null;

        public String GrammaticalRepresentation { get; internal set; }

        public String PhraseRepresentation { get; internal set; }

        public TokenGraph PhrasalStructure { get; internal set; }

        public TokenGraph GrammaticalStructure { get; internal set; }
    }
}
