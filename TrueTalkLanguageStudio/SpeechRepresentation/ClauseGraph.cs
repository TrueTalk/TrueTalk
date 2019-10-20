


namespace TrueTalk.SpeechRepresentation
{
    using System;

    public class ClauseGraph
    {
        public String RawClause { get; internal set; }

        public String GrammaticalRepresentation { get; internal set; }

        public String PhraseRepresentation { get; internal set; }

        public TokenGraph PhrasalStructure { get; internal set; }

        public TokenGraph GrammaticalStructure { get; internal set; }
    }
}
