namespace TrueTalk.Logic
{
    using TrueTalk.Common;
    using TrueTalk.Speech.Grammar;
    using TrueTalk.SpeechRepresentation;

    public class LogicFormula
    {
        private ExpressionTree<Atom> m_exprTree;

        LogicFormula( ExpressionTree<Atom> exprTree )
        {
            m_exprTree = exprTree;
        }
    }
}
