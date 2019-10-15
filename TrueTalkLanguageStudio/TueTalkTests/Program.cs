namespace TrueTalk.Tests
{
    using System;
    using TrueTalk.Speech;

    class Program
    {
        static void Main( string[ ] args )
        {
            var d1 = IntegerNumber.New( "72" );
            var d2 = RealNumber.New( "72.14" );
            var w = Word.New( "cippalippa" );

            Console.WriteLine( d1 );
            Console.WriteLine( d2 );
            Console.WriteLine( w );
        }
    }
}
