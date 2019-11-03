
namespace TrueTalk.Interfaces
{
    using System;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
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
        public int Version { get; set; }

        public abstract bool ApplyTransformation( IAnalysis analysis );

        //--//

        public virtual TransformableItem CloneAndBumpVersion( )
        {
            TransformableItem cpy = (TransformableItem)this.MemberwiseClone();

            cpy.BumpVersion( );

            return cpy;
        }

        public virtual void BumpVersion( )
        {
            this.Version += 1;
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
