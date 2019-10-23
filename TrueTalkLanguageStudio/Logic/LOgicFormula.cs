namespace TrueTalk.Logic
{
    using TrueTalk.Common;

    public class LogicFormula
    {
        private readonly ExpressionTree<Atom> m_exprTree;

        LogicFormula( ExpressionTree<Atom> exprTree )
        {
            m_exprTree = exprTree;
        }
    }
}
