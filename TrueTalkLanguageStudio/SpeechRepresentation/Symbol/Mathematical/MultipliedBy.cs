﻿

namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Text;
    using TrueTalk.Interfaces;

    public sealed class MultipliedBy : MathematicalSymbol
    {
        private MultipliedBy( String value ) : base( value )
        { }

        //--//

        public static MultipliedBy New( String value )
        {
            return new MultipliedBy( value );
        }

        //--//

        public override bool ApplyTransformation(IAnalysis analysis)
        {
            throw new NotImplementedException();
        }

        public override void InnerToString( StringBuilder sb )
        {
            base.InnerToString( sb );

            sb.AppendFormat( @"MultipliedBy('{0}')", this.Value );
        }

        public override string ToString( )
        {
            StringBuilder sb = new StringBuilder();

            InnerToString( sb );

            return sb.ToString( );
        }
    }
}
