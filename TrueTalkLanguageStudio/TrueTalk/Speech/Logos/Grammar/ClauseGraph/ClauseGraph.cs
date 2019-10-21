﻿


namespace TrueTalk.Speech.Grammar
{
    using System;

    public class ClauseGraph
    {
        public Clause Clause { get; internal set; } = null;

        public String GrammaticalRepresentation { get; internal set; }

        public String PhraseRepresentation { get; internal set; }

        public TokenGraph PhrasalStructure { get; internal set; }

        public TokenGraph GrammaticalStructure { get; internal set; }
    }
}
