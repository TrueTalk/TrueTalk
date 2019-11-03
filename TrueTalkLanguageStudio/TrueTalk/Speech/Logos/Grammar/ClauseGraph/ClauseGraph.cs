


namespace TrueTalk.Speech.Grammar
{
    using System;
    using TrueTalk.Interfaces;

    public class ClauseGraph : TransformableItem
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

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            bool fChanged = false;

            fChanged |= this.GrammaticalStructure.ApplyTransformation( analysis );
            fChanged |= this.PhrasalStructure    .ApplyTransformation( analysis );

            return fChanged;
        }
    }
}
