

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class DividedBy : MathematicalSymbol
    {
        private DividedBy( String value ) : base( value )
        { }

        //--//

        public static DividedBy New( String value )
        {
            return new DividedBy( value );
        }

        //--//

        public override bool ApplyTransformation(IAnalysis analysis)
        {
            throw new NotImplementedException();
        }

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"DividedBy('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
