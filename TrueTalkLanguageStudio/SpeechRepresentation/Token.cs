
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using TrueTalk.Interfaces;

    public abstract class Token : TransformableItem
    {
        public enum TokenKind
        {
            Numeric,
            Alpha,
            Symbol,
            Puctuation,
        }

        //--//

        public static readonly char[] MathematicalSymbols = { '+', '-', '*', '/', '(', ')', '=' };
        public static readonly char[] PuctuationSymbols   = { '\'', ':', ',', '_', '!', '-', '.', '?', '`', ';', '/' };

        //--//

        protected Token( ) : base( )
        {
        }

        protected Token( String value, TokenKind kind ) : base( )
        {
            RawValue    = value;
            KindOfToken = kind;
        }

        //--//

        public TokenKind KindOfToken { get; set; }

        public String RawValue { get; set; }

        public static bool IsAlpha( string value )
        {
            if( IsNumeric( value ) ||
                IsMathematicalSymbol( value ) ||
                IsPunctuation( value ) )
            {
                return false;
            }

            //
            // If none of the above, then it is a word.
            //
            return true;
        }

        public static bool IsNumeric( string value )
        {
            EnsureNormalizeString( value );

            //
            // Any number can be parsed into a double.
            //
            if( Double.TryParse( value, out _ ) )
            {
                return true;
            }

            return false;
        }

        public static bool IsPunctuation( string value )
        {
            EnsureNormalizeString( value );

            //
            // Must be one character long.
            // 
            if( value.Length > 1 )
            {
                return false;
            }

            //
            // Is it a punctuation mark?
            //
            if( value.IndexOfAny( PuctuationSymbols ) != -1 )
            {
                return true;
            }

            return false;
        }

        public static bool IsMathematicalSymbol( string value )
        {
            EnsureNormalizeString( value );

            //
            // Must be one character long.
            // 
            if( value.Length > 1 )
            {
                return false;
            }

            //
            // Is it a mathematical symbol?
            //
            if( value.IndexOfAny( MathematicalSymbols ) != -1 )
            {
                return true;
            }

            return false;
        }

        //--//

        public static string EnsureNormalizeString( string value )
        {
            value.Trim( );

            if( value.IndexOf( ' ' ) != -1 )
            {
                throw new ArgumentException( $"The string {value} contains spaces." );
            }

            return value;
        }
    }
}
