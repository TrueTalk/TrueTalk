﻿//
// Copyright (c) TrueTalk LLC.    All rights reserved.
//

namespace TrueTalk.CompilerDiagnostics
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using TrueTalk.Common;
    using TrueTalk.Interfaces;
    using TrueTalk.Speech.Grammar;
    using TrueTalk.SpeechRepresentation;

    //--//

    public class ClausePersistence
    {
        [Serializable]
        public class VertexEntry
        {
            [XmlElement("Key")]
            public object Key;

            [XmlElement("Value")]
            public object Value;

            public VertexEntry( )
            {
            }

            public VertexEntry( object key, object value )
            {
                Key   = key;
                Value = value;
            }
        }

        [Serializable]
        public class EdgeEntry
        {
            [XmlElement("Key")]
            public object Key;

            [XmlElement("Value")]
            public object Value;

            [XmlElement("Relation")]
            public object Relation;

            public EdgeEntry( )
            {
            }

            public EdgeEntry( object key, object value, object relation )
            {
                Key      = key;
                Value    = value;
                Relation = relation;
            }
        }

        [Serializable]
        //[XmlInclude( typeof( Phrase ) )]
        //[XmlInclude( typeof( Clause ) )]
        //[XmlInclude( typeof( ClauseGraph ) )]
        //[XmlInclude( typeof( TokenGraph ) )]
        [XmlInclude( typeof( Vertex ) )]
        [XmlInclude( typeof( Edge ) )]
        [XmlInclude( typeof( TransformableItem ) )]
        [XmlInclude( typeof( Token ) )]
        [XmlInclude( typeof( Text ) )]
        [XmlInclude( typeof( Word ) )]
        [XmlInclude( typeof( Achronym ) )]
        [XmlInclude( typeof( Number ) )]
        [XmlInclude( typeof( Symbol ) )]
        [XmlInclude( typeof( MathematicalSymbol ) )]
        [XmlInclude( typeof( Punctuation ) )]
        public class PersistedClause
        {
            private PersistedClause( ) { }

            public PersistedClause( Clause clause )
            {
                var persistedGrammaticalGraphVertexes = new List<VertexEntry>( );

                foreach(var entry in clause.Graph.GrammaticalStructure.Vertexes)
                {
                    persistedGrammaticalGraphVertexes.Add( new VertexEntry( entry.Key, entry.Value ) );
                }

                var persistedGrammaticalGraphEdges = new List<EdgeEntry>( );

                foreach( var entry in clause.Graph.GrammaticalStructure.Edges )
                {
                    persistedGrammaticalGraphEdges.Add( new EdgeEntry( entry.Source, entry.Target, entry.Relation ) );
                }

                var persistedPhrasalGraphVertexes = new List<VertexEntry>( );

                foreach( var entry in clause.Graph.PhrasalStructure.Vertexes )
                {
                    persistedPhrasalGraphVertexes.Add( new VertexEntry( entry.Key, entry.Value ) );
                }

                var persistedPhrasalGraphEdges = new List<EdgeEntry>( );

                foreach( var entry in clause.Graph.PhrasalStructure.Edges )
                {
                    persistedPhrasalGraphEdges.Add( new EdgeEntry( entry.Source, entry.Target, entry.Relation ) );
                }

                this.Text                          = clause.Text;
                this.GrammaticalRepresentation     = clause.Graph.GrammaticalRepresentation;
                this.PhraseRepresentation          = clause.Graph.PhraseRepresentation;
                this.PersistedGrammarGraphVertexes = persistedGrammaticalGraphVertexes;
                this.PersistedGrammarGraphEdges    = persistedGrammaticalGraphEdges;
                this.PersistedPhraseGraphVertexes  = persistedPhrasalGraphVertexes;
                this.PersistedPhraseGraphEdges     = persistedPhrasalGraphEdges;
            }

            [XmlElement("Text")]
            public string Text;

            [XmlElement("GrammaticalRepresentation")]
            public String GrammaticalRepresentation;

            [XmlElement("PhraseRepresentation")]
            public String PhraseRepresentation;

            [XmlElement( "PersistedPhraseGraphVertexes" )]
            public List<VertexEntry> PersistedPhraseGraphVertexes;

            [XmlElement( "PersistedPhraseGraphEdges" )]
            public List<EdgeEntry> PersistedPhraseGraphEdges;

            [XmlElement( "PersistedGrammarGraphVertexes" )]
            public List<VertexEntry> PersistedGrammarGraphVertexes;

            [XmlElement( "PersistedGrammarGraphEdges" )]
            public List<EdgeEntry> PersistedGrammarGraphEdges;
        }

        //--//

        private readonly string workspaceDirectory;

        //--//

        public ClausePersistence( string workspaceDirectory )
        {
            var compilerDiagnosticsPath = Path.Combine( workspaceDirectory + $"\\Compiler\\Transformations\\" );

            Directory.CreateDirectory( compilerDiagnosticsPath );

            this.workspaceDirectory = compilerDiagnosticsPath;
        }

        //--//

        public string PersistClause( Clause clause )
        {
            var fileName = Path.Combine( this.workspaceDirectory, clause.Text + "__v" + clause.Version.ToString( ) + ".ttd" );

            var record = new PersistedClause( clause );

            XmlSerializer serializer = new XmlSerializer(typeof(PersistedClause));

            StreamWriter writer = new StreamWriter( fileName );

            serializer.Serialize( writer, record );

            writer.Close( );

            return fileName;
        }

        public Clause LoadClause( string fileName )
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PersistedClause));

            StreamReader reader = new StreamReader( fileName );

            var record = (PersistedClause)serializer.Deserialize( reader );

            var phrasalStructure = new TokenGraph( phraseStructure: true );

            foreach( VertexEntry entry in record.PersistedPhraseGraphVertexes )
            {
                phrasalStructure.AddOrUpdateVertex( (int)entry.Key, ( (Vertex)entry.Value ).Value, ( (Vertex)entry.Value ).Tag, ( (Vertex)entry.Value ).Token );
            }
            foreach( EdgeEntry entry in record.PersistedPhraseGraphEdges )
            {
                var src = (int)entry.Key;
                var tgt = (int)entry.Value;
                var rel = (string)entry.Relation;

                phrasalStructure.AddEdge( 
                    src, 
                    phrasalStructure.Vertexes[ src ].Value,
                    phrasalStructure.Vertexes[ src ].Tag,
                    phrasalStructure.Vertexes[ src ].Token,
                    tgt, 
                    phrasalStructure.Vertexes[ tgt ].Value,
                    phrasalStructure.Vertexes[ tgt ].Tag,
                    phrasalStructure.Vertexes[ tgt ].Token,
                    rel 
                    );
            }

            var grammarStructure = new TokenGraph( phraseStructure: false );

            foreach( VertexEntry entry in record.PersistedGrammarGraphVertexes )
            {
                grammarStructure.AddOrUpdateVertex( (int)entry.Key, ( (Vertex)entry.Value ).Value, ( (Vertex)entry.Value ).Tag, ( (Vertex)entry.Value ).Token );
            }
            foreach( EdgeEntry entry in record.PersistedGrammarGraphEdges )
            {
                var src = (int)entry.Key;
                var tgt = (int)entry.Value;
                var rel = (string)entry.Relation;

                grammarStructure.AddEdge(
                    src,
                    grammarStructure.Vertexes[ src ].Value,
                    grammarStructure.Vertexes[ src ].Tag,
                    grammarStructure.Vertexes[ src ].Token,
                    tgt,
                    grammarStructure.Vertexes[ tgt ].Value,
                    grammarStructure.Vertexes[ tgt ].Tag,
                    grammarStructure.Vertexes[ tgt ].Token,
                    rel
                    );
            }

            var clause = new Clause( record.Text );

            clause.Graph.Owner                     = clause;
            clause.Graph.PhrasalStructure          = phrasalStructure;
            clause.Graph.PhraseRepresentation      = record.PhraseRepresentation;
            clause.Graph.GrammaticalStructure      = grammarStructure;
            clause.Graph.GrammaticalRepresentation = record.GrammaticalRepresentation;

            return clause;
        }
    }
}
