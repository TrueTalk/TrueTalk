﻿


namespace TrueTalk.Speech.Grammar
{
    using System;

    public class ClauseGraph
    {
        public ClauseGraph( )
        {
        }

        // TODO make property setters invisble but to XmlSerialization

        public Clause Owner { get; set; } = null;

        public String GrammaticalRepresentation { get; set; }

        public String PhraseRepresentation { get; set; }

        public TokenGraph PhrasalStructure { get; set; }

        public TokenGraph GrammaticalStructure { get; set; }
    }
}
