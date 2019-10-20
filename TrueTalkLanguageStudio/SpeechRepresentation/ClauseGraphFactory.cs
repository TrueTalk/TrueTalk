
namespace TrueTalk.SpeechRepresentation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using edu.stanford.nlp.trees;
    using edu.stanford.nlp.parser.lexparser;
    using edu.stanford.nlp.semgraph;
    using java.io;
    using edu.stanford.nlp.process;
    using edu.stanford.nlp.ling;

    using VertexIndex = System.Int32;
    using VertexValue      = System.String;
    using VertexTag        = System.String;
    using RelationKind     = System.String;
    using System.Text.RegularExpressions;

    public class ClauseGraphFactory
    {
        private readonly String modelPath;

        public ClauseGraphFactory( String modelPath ) { this.modelPath = modelPath; }

        public ClauseGraph FromString( String clause )
        {
            // Loading english PCFG parser from file
            var lp = LexicalizedParser.loadModel(modelPath);

            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var reader = new StringReader(clause);
            var rawWords = tokenizerFactory.getTokenizer(reader).tokenize();
            reader.close( );
            var tree = lp.apply(rawWords);

            // Extract dependencies from lexical tree
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(tree);
            var sg = new edu.stanford.nlp.semgraph.SemanticGraph(gs.typedDependencies());

            TokenGraph grammaticalStructure = new TokenGraph(phraseStructure: false);
            {
                var visited = new HashSet<IndexedWord>();
                var visitQueue = new Queue<IndexedWord>();

                visitQueue.Enqueue( sg.getFirstRoot( ) );

                while( visitQueue.Count > 0 )
                {
                    var current = visitQueue.Dequeue();

                    visited.Add( current );

                    grammaticalStructure.AddOrUpdateVertex( current.index( ), current.value( ), current.tag( ) );

                    foreach( SemanticGraphEdge edge in sg.getOutEdgesSorted( current ).toArray( ) )
                    {
                        var src = edge.getSource();
                        var tgt = edge.getTarget();

                        if( visited.Contains( src ) == false )
                        {
                            visitQueue.Enqueue( src );
                        }
                        if( visited.Contains( tgt ) == false )
                        {
                            visitQueue.Enqueue( tgt );
                        }

                        grammaticalStructure.AddEdge(
                            src.index( ),
                            src.value( ),
                            src.tag( ),
                            tgt.index( ),
                            tgt.value( ),
                            tgt.tag( ),
                            edge.getRelation( ).getShortName( )
                            );
                    }
                }

                grammaticalStructure.Display( );
            }

            TokenGraph phraseStructure = new TokenGraph(phraseStructure: true);
            {
                Tree root = tree;

                var visited = new HashSet<Tree>();
                var visitQueue = new Queue<Tree>();

                visitQueue.Enqueue( root );

                while( visitQueue.Count > 0 )
                {
                    var current = visitQueue.Dequeue();

                    visited.Add( current );

                    phraseStructure.AddOrUpdateVertex( current.nodeNumber( current ), current.label( ).ToString( ), current.value( ) );

                    foreach( Tree child in current.children( ) )
                    {
                        var src = current;
                        var tgt = child;

                        if( visited.Contains( src ) == false )
                        {
                            visitQueue.Enqueue( src );
                        }
                        if( visited.Contains( tgt ) == false )
                        {
                            visitQueue.Enqueue( tgt );
                        }

                        phraseStructure.AddEdge(
                            src.nodeNumber( root ),
                            src.value( ) == "" ? $"nn" : src.value(),
                            src.label( ).ToString( ),
                            tgt.nodeNumber( root ),
                            tgt.value( ) == "" ? $"nn" : tgt.value( ),
                            tgt.label( ).ToString( ),
                            "dep"
                        );
                    }
                }

                phraseStructure.Display( );
            }

            //
            // Verify consistency of lookup for locating items across both graphs
            //
            foreach( var key in grammaticalStructure.VertexesLookup.Keys )
            {
                Debug.Assert( phraseStructure.VertexesLookup.ContainsKey( key ) );
            }

            String grammar = sg.toFormattedString();
            string resGrammar = Regex.Replace(grammar, @"\n", "\r\n");

            String phrase = tree.pennString();
            string resPhrase = Regex.Replace(phrase, @"\n", "\r\n");

            return new ClauseGraph { 
                RawClause = clause,
                GrammaticalRepresentation = resGrammar,
                PhraseRepresentation = resPhrase,
                GrammaticalStructure = grammaticalStructure,
                PhrasalStructure = phraseStructure,
            };

            //////var tp = new TreePrint("penn,typedDependencies");
            //////tp.printTree( tree2 );
        }
    }
}
