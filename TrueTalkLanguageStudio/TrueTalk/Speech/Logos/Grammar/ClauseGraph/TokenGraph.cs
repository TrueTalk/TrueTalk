

namespace TrueTalk.Speech.Grammar
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TrueTalk.Interfaces;
    using TrueTalk.SpeechRepresentation;

    using Console      = System.Console;
    using RelationKind = System.String;
    using VertexIndex  = System.Int32;
    using VertexKey    = System.String;
    using VertexTag    = System.String;
    using VertexValue  = System.String;

    public class TokenGraph : TransformableItem
    {
        public class Vertex
        {
            public VertexKey    Key;
            public VertexIndex  Index;
            public VertexValue  Value;
            public VertexTag    Tag;
            public Token        Token         = default;
            public List<Edge>   IncomingEdges = new List<Edge>();
            public List<Edge>   OutgoingEdges = new List<Edge>();

            public String ShortLabel => $"{Value}";

            public String LongLabel => $"{Value}/{Tag}/{Index}";

            public override string ToString( )
            {
                return LongLabel;

                //////String incomingRep = "";
                //////int incoming = IncomingEdges.Count;

                //////String outgoingRep = "";
                //////int outgoing = OutgoingEdges.Count;

                //////for( int i = 0; i < incoming; ++i )
                //////{
                //////    incomingRep += IncomingEdges[ i ];

                //////    if( i < incoming - 1 )
                //////    {
                //////        incomingRep += ",";
                //////    }
                //////}

                //////for( int i = 0; i < outgoing; ++i )
                //////{
                //////    outgoingRep += OutgoingEdges[ i ];

                //////    if( i < outgoing - 1 )
                //////    {
                //////        outgoingRep += ",";
                //////    }
                //////}

                //////return $"[{incomingRep}] -> ({LongLabel}) -> [{outgoingRep}]";
            }
        }

        public class Edge
        {
            public Vertex       Source;
            public Vertex       Target;
            public RelationKind Relation;

            public String ShortLabel => $"{Relation}";

            public String LongLabel => $"[{Source.LongLabel}] -> {Relation} -> [{Target.LongLabel}]";

            public override string ToString( )
            {
                return LongLabel;
            }

            //--//
        }

        //--//

        private TokenGraph( ) { }

        public TokenGraph( bool phraseStructure )
        {
            this.phraseStructure = phraseStructure;
        }

        //--//

        private readonly bool phraseStructure;

        //--//

        public Dictionary<VertexIndex, Vertex> Vertexes       = new Dictionary<VertexIndex, Vertex>();
        public List<Edge>                      Edges          = new List<Edge>();
        public Dictionary<VertexTag, Vertex>   VertexesLookup = new Dictionary<VertexTag, Vertex>();

        //--//

        private readonly Edge[] EmptyEdgeSet = new Edge[0] { };

        //--//

        public void Display( )
        {
            Console.WriteLine( "Vertexes:" );
            foreach( var v in Vertexes.Values )
            {
                Console.WriteLine( $"Vertex {v.Index}: => {v.LongLabel}" );
            }
            Console.WriteLine( "Edges:" );
            foreach( var e in Edges )
            {
                Console.WriteLine( $"Edge {e.Source.Index}->{e.Target.Index}: {e.ShortLabel}" );
            }
        }

        public void AddOrUpdateVertex( VertexIndex id, VertexValue value, VertexTag tag, Edge[ ] incoming, Edge[ ] outgoing )
        {
            Vertex v = null;

            if( Vertexes.ContainsKey( id ) )
            {
                UpdateVertexEdges( v, incoming, outgoing );
            }
            else
            {
                AddVertex( id, value, tag, incoming, outgoing );
            }
        }

        public void AddOrUpdateVertex( VertexIndex id, VertexValue value, VertexTag tag )
        {
            AddOrUpdateVertex( id, value, tag, EmptyEdgeSet, EmptyEdgeSet );
        }

        public void AddEdge( VertexIndex srcId, VertexValue srcValue, VertexTag srcTag, VertexIndex tgtId, VertexValue tgtValue, VertexTag tgtTag, RelationKind r )
        {
            Vertex src = null;
            Vertex tgt = null;

            if( Vertexes.ContainsKey( srcId ) )
            {
                src = Vertexes[ srcId ];
            }
            else
            {
                src = AddVertex( srcId, srcValue, srcTag );

                ReconcileLookup( src );
            }

            if( Vertexes.ContainsKey( tgtId ) )
            {
                tgt = Vertexes[ tgtId ];
            }
            else
            {
                tgt = AddVertex( tgtId, tgtValue, tgtTag );

                ReconcileLookup( tgt );
            }

            var e = new Edge { Source = src, Target = tgt, Relation = r };

            src.OutgoingEdges.Add( e );
            tgt.IncomingEdges.Add( e );

            //
            // This allows to add multiple edges between the same source/target pair. 
            //
            Edges.Add( e );

            //Console.WriteLine( e.ToString( ) );
            //Console.WriteLine( "==> " + src.ToString( ) );
            //Console.WriteLine( "==> " + tgt.ToString( ) );
        }

        public bool IsTag( Vertex v ) 
        {
            //
            // All tags in a phrasal structure graph have the same value has the value entry.
            // 
            return this.phraseStructure &&  v.Value.Equals( v.Tag ) ;
        }

        //--//

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            return analysis.Apply( this );
        }

        //--//

        private Vertex AddVertex( VertexIndex index, VertexValue value, VertexTag tag, Edge[ ] incoming, Edge[ ] outgoing )
        {
            Debug.Assert( Vertexes.ContainsKey( index ) == false );

            var v = new Vertex
            {
                Key   = phraseStructure ? (tag.Contains("-") ? tag : $"{tag}-{index}") : $"{value}-{index}",
                Index = index,
                Value = value,
                Tag   = tag
            };

            UpdateVertexEdges( v, incoming, outgoing );

            Vertexes.Add( v.Index, v );

            ReconcileLookup( v );

            return v;
        }

        private Vertex AddVertex( VertexIndex id, VertexValue value, VertexTag tag )
        {
            Debug.Assert( Vertexes.ContainsKey( id ) == false );

            return AddVertex( id, value, tag, EmptyEdgeSet, EmptyEdgeSet );
        }

        private void UpdateVertexEdges( Vertex v, Edge[ ] incoming, Edge[ ] outgoing )
        {
            foreach( var e in incoming )
            {
                v.IncomingEdges.Add( e );

                Edges.Add( e );
            }

            foreach( var e in outgoing )
            {
                v.OutgoingEdges.Add( e );

                Edges.Add( e );
            }
        }

        private void ReconcileLookup( Vertex v )
        {
            if( VertexesLookup.ContainsKey( v.Key ) == false )
            {
                VertexesLookup.Add( v.Key, v );
            }
        }
    }
}
