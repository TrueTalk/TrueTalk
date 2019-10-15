
namespace TrueTalk.Speech
{
    using System;
    using System.Text;
    using TrueTalk.Analysis;

    public class TransformableItem
    {
        //--//

        protected TransformableItem( )
        {
        }

        //--//

        public int Version { get; private set; }

        //--//

        public virtual TransformableItem CloneAndBumpVersion()
        {
            TransformableItem cpy = (TransformableItem)this.MemberwiseClone();

            cpy.Version += 1;

            return cpy;
        }

        public virtual void ApplyTransformation( SpeechAnalysis context )
        {
            context.Apply( this );
        }

        public virtual void InnerToString( StringBuilder sb )
        {
            sb.AppendFormat( @"Version:{0},", this.Version );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( ); 
        }
    }
}
