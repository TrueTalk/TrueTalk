
namespace TrueTalk.Speech.Grammar
{
    using System;
    using TrueTalk.Interfaces;

    public class Clause : TransformableItem
    {
        public readonly string Text;

        //--//

        private Clause( )
        {
            Text = String.Empty;
        }

        public Clause( string rawText )
        {
            Text = rawText;
        }

        public Clause( string rawText, int version ) : base( version )
        {
            Text = rawText;
        }

        public ClauseGraph Graph { get; internal set; } = null;

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }
    }
}
