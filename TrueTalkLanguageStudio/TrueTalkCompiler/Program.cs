using System;
using System.IO;

namespace TrueTalk.Compiler
{
    class Compiler
    {
        public static readonly string SourceFileExtension = ".tts"; // True Talk Source file

        static void Main( string[ ] args )
        {
            UserConfiguration userConfig = ParseArgs(args);

            new CompilerEngine( userConfig ).Compile( );
        }

        //--//

        private static UserConfiguration ParseArgs( string[ ] args )
        {
            var config = new UserConfiguration();

            for( int idx = 0; idx < args.Length; ++idx )
            {
                var current = args[idx];

                if( current.StartsWith( "--" ) )
                {
                    if( idx >= args.Length )
                    {
                        throw new ArgumentException( $"Parameter {current} is not followed by any value." );
                    }

                    var paramPair = (current, args[++idx]);

                    switch( paramPair.current )
                    {
                        case "--model":
                            {
                                config.LanguageModel = paramPair.Item2;
                            }
                            break;

                        case "--workspaceDir":
                            {
                                if( Directory.Exists( paramPair.Item2 ) == false )
                                {
                                    throw new ArgumentException( $"Work space directory {current} could not be found." );
                                }

                                config.WorkspaceDirectory = paramPair.Item2;
                            }
                            break;

                        default:
                            throw new ArgumentException( $"Parameter {current} could not be recognized." );
                    }
                }
                else
                {
                    if( File.Exists( current ) && Path.GetExtension( args[ 0 ] ).Equals( SourceFileExtension ) )
                    {
                        config.SourceFile = current;
                    }
                }
            }

            return config;
        }
    }
}
