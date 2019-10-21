namespace TrueTalk.Speech
{
    using TrueTalk.SpeechRepresentation;

    using Synonyms = System.Collections.Generic.Dictionary<SpeechRepresentation.Token, System.Collections.Generic.List<SpeechRepresentation.Token>>;

    public class Logos
    {
        protected Logos( Synonyms synonyms )
        {
            m_synonyms = synonyms;
        }

        //--//

        private readonly Synonyms m_synonyms;

        //--//

        /// <summary>
        /// Extracting meaning from a sentence or a collection of sentences.
        /// </summary>
        public Understanding Understanding { get; private set; }

        /// <summary>
        /// Assigning relationships between different parts speech in a phrase or clause.
        /// </summary>
        public Judging Judiging { get; private set; }

        /// <summary>
        /// Generating logically sound representations from a clause or a set of clauses.
        /// </summary>
        public Reasoning Reasoning { get; private set; }
    }
}
