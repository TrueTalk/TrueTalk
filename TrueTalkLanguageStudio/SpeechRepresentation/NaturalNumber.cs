

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class NaturalNumber : Number
    {
        private NaturalNumber( String value ) : base( value, DigitKind.Integer )
        { }

        //--//

        public static NaturalNumber New( String value )
        {
            return new NaturalNumber( value );
        }

        public override bool ApplyTransformation(IAnalysis analysis)
        {
            throw new NotImplementedException();
        }

        //--//

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Natural('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
