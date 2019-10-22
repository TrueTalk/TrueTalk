﻿

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class IntegerNumber : Number
    {
        private IntegerNumber( String value ) : base( value, NumberKind.Integer )
        { }

        //--//

        public static IntegerNumber New( String value )
        {
            return new IntegerNumber( value );
        }

        //--//

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            throw new NotImplementedException( );
        }

        public new int Value => ( int )( base.Value - ( base.Value % 1 ) );

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"Integer('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
