namespace TrueTalk.Logic
{
    using TrueTalk.Speech.Grammar;

    public class Atom
    {
        private readonly Clause      m_clause;
        private          Truth.State m_truth;
        private          bool        m_negated;

        //--//

        public Atom( Clause clause, Truth.State truth )
        {
            m_clause = clause;
            m_truth  = truth;
        }

        public Atom( Truth.State truth )
        {
            m_truth = truth;
        }

        public bool Negate( )
        {
            m_negated = !m_negated;

            return Truth.Negate( ref m_truth );
        }
    }
}
