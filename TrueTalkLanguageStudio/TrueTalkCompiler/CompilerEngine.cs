﻿//
// Copyright (c) TrueTalk LLC.    All rights reserved.
//

namespace TrueTalk.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using TrueTalk.Analysis;
    using TrueTalk.Common;
    using TrueTalk.CompilerDiagnostics;
    using TrueTalk.Speech.Grammar;

    //--//

    internal class CompilerEngine
    {
        private readonly UserConfiguration userConfig;

        public CompilerEngine( UserConfiguration userConfig )
        {
            this.userConfig = userConfig;
        }

        public Version Version => new Version( 1, 0, 0, 0 );

        public void Compile( )
        {
            var diag = new ClausePersistence( this.userConfig.WorkspaceDirectory );

            var allLines = ParseClauses( this.userConfig.SourceFile ) ;

            var factory = new ClauseGraphFactory( this.userConfig.LanguageModel );

            foreach( string clauseText in allLines )
            {
                var clause = factory.FromString( clauseText );

                var fileName = diag.PersistClause( clause );

                var clause1 = diag.LoadClause( fileName );

                CHECKS.ASSERT( clause == clause1, "Persisted graph is not equivalent to original graph." );

                clause1.Graph.GrammaticalStructure.Display( );
                clause1.Graph.PhrasalStructure    .Display( );

                var speechAnalysis = new SpeechAnalysis();

                clause.ApplyTransformation( speechAnalysis );

                fileName = diag.PersistClause( clause );

                clause1 = diag.LoadClause( fileName );

                CHECKS.ASSERT( clause == clause1, "Persisted graph is not equivalent to original graph." );
            }
        }

        private string[ ] ParseClauses( string sourceFile )
        {
            var clauses = new List<string>();

            var reader = new StreamReader(new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read));

            while( reader.EndOfStream == false )
            {
                clauses.Add( reader.ReadLine( ) );
            }

            return clauses.ToArray( );
        }
    }
}
