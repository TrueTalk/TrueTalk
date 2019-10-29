
namespace TrueTalk.Speech.Grammar
{
    using System;
    using TrueTalk.Interfaces;

    public class Clause : TransformableItem
    {
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

        public string Text { get; }


        public ClauseGraph Graph { get; internal set; } = new ClauseGraph( );

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }
    }
}
