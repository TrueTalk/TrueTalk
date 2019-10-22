

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class NaturalNumber : Number
    {
        private NaturalNumber( String value ) : base( value, NumberKind.Natural )
        { }

        //--//

        public static NaturalNumber New( String value )
        {
            if (Number.TryParse(value, out Double result, out NumberKind kind))
            {
                if (kind == NumberKind.Natural)
                {
                    return new NaturalNumber(value);
                }
            }

            throw new ArgumentException($"Token '{value}' could not be parsed as a natural number");
        }

        //--//

        public new int Value => (int)(base.Value - (base.Value % 1));

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
