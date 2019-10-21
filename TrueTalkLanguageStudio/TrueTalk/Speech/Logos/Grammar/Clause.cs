
namespace TrueTalk.Speech.Grammar
{
    using System;
    using TrueTalk.Interfaces;

    public class Clause : TransformableItem
    {
        public readonly string RawString;

        //--//

        public Clause( string rawString )
        {
            RawString = rawString;
        }

        public ClauseGraph Graph { get; internal set; } = null;

        public override bool ApplyTransformation(IAnalysis analysis)
        {
            throw new NotImplementedException();
        }
    }
}
