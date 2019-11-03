
namespace TrueTalk.Speech.Grammar
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using edu.stanford.nlp.ling;
    using edu.stanford.nlp.parser.lexparser;
    using edu.stanford.nlp.process;
    using edu.stanford.nlp.semgraph;
    using edu.stanford.nlp.trees;
    using java.io;

    public class ClauseGraphFactory
    {
        private readonly String modelPath;

        public ClauseGraphFactory( String modelPath )
        {
            this.modelPath = modelPath;
        }

        public Clause FromString( String rawClause )
        {
            // Loading english PCFG parser from file
            var lp = LexicalizedParser.loadModel(modelPath);

            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");

            java.util.List rawWords = null;

            using( var reader = new StringReader( rawClause ) )
            {
                rawWords = tokenizerFactory.getTokenizer(reader).tokenize();

                reader.close( );
            }

            var tree = lp.apply(rawWords);

            // Extract dependencies from lexical tree
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(tree);
            var sg = new edu.stanford.nlp.semgraph.SemanticGraph(gs.typedDependencies());

            TokenGraph grammaticalStructure = new TokenGraph(phraseStructure: false);
            {
                var visited = new HashSet<IndexedWord>();
                var visitQueue = new Stack<IndexedWord>();

                visitQueue.Push( sg.getFirstRoot( ) );

                while( visitQueue.Count > 0 )
                {
                    var current = visitQueue.Pop();

                    visited.Add( current );

                    grammaticalStructure.AddOrUpdateVertex( current.index( ), current.value( ), current.tag( ), null );

                    foreach( SemanticGraphEdge edge in sg.getOutEdgesSorted( current ).toArray( ) )
                    {
                        var src = edge.getSource();
                        var tgt = edge.getTarget();

                        if( visited.Contains( src ) == false )
                        {
                            visitQueue.Push( src );
                        }
                        if( visited.Contains( tgt ) == false )
                        {
                            visitQueue.Push( tgt );
                        }

                        grammaticalStructure.AddEdge(
                            src.index( ),
                            src.value( ),
                            src.tag( ),
                            null,
                            tgt.index( ),
                            tgt.value( ),
                            tgt.tag( ),
                            null,
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
                var visitQueue = new Stack<Tree>();

                visitQueue.Push( root );

                while( visitQueue.Count > 0 )
                {
                    var current = visitQueue.Pop();

                    visited.Add( current );

                    phraseStructure.AddOrUpdateVertex( current.nodeNumber( current ), current.label( ).ToString( ), current.value( ), null );

                    foreach( Tree child in current.children( ) )
                    {
                        var src = current;
                        var tgt = child;

                        if( visited.Contains( src ) == false )
                        {
                            visitQueue.Push( src );
                        }
                        if( visited.Contains( tgt ) == false )
                        {
                            visitQueue.Push( tgt );
                        }

                        phraseStructure.AddEdge(
                            src.nodeNumber( root ),
                            String.IsNullOrEmpty( src.value( ) ) ? $"nn" : src.value( ),
                            src.label( ).ToString( ),
                            null,
                            tgt.nodeNumber( root ),
                            String.IsNullOrEmpty( tgt.value( ) ) ? $"nn" : tgt.value( ),
                            tgt.label( ).ToString( ),
                            null,
                            "dep"
                        );
                    }
                }

                phraseStructure.Display( );
            }

            //
            // Verify consistency of lookup for locating items across both graphs
            //
            //////foreach( var key in grammaticalStructure.VertexesLookup.Keys )
            //////{
            //////    Debug.Assert( phraseStructure.VertexesLookup.ContainsKey( key ) );
            //////}

            String grammar = sg.toFormattedString();
            string resGrammar = Regex.Replace(grammar, @"\n", "\r\n");

            String phrase = tree.pennString();
            string resPhrase = Regex.Replace(phrase, @"\n", "\r\n");

            var clause = new Clause(rawClause);

            var clauseGraph = new ClauseGraph
            {
                Owner                     = clause,
                GrammaticalRepresentation = resGrammar,
                PhraseRepresentation      = resPhrase,
                GrammaticalStructure      = grammaticalStructure,
                PhrasalStructure          = phraseStructure,
            };

            clause.Graph = clauseGraph;

            return clause;

            //////var tp = new TreePrint("penn,typedDependencies");
            //////tp.printTree( tree2 );
        }
    }
}
