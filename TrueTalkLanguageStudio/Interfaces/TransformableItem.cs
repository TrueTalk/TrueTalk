
namespace TrueTalk.Interfaces
{
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public abstract class TransformableItem
    {
        //--//

        protected TransformableItem( )
        {
        }

        protected TransformableItem( int version )
        {
            this.Version = version;
        }

        //--//

        [XmlElement( "Version" )]
        public int Version { get; private set; }

        public abstract bool ApplyTransformation( IAnalysis analysis );

        //--//

        public virtual TransformableItem CloneAndBumpVersion( )
        {
            TransformableItem cpy = (TransformableItem)this.MemberwiseClone();

            cpy.Version += 1;

            return cpy;
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
