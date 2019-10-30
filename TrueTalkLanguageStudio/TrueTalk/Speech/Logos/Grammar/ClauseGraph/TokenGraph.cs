

namespace TrueTalk.Speech.Grammar
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TrueTalk.Interfaces;
    using TrueTalk.SpeechRepresentation;

    using Console = System.Console;
    using RelationKind = System.String;
    using VertexIndex = System.Int32;
    using VertexKey = System.String;
    using VertexTag = System.String;
    using VertexValue = System.String;

    public class TokenGraph : TransformableItem
    {
        [Serializable]
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

            //--//

            public override bool Equals( object obj )
            {
                return this.Equals( obj as TokenGraph );
            }

            public bool Equals( Vertex v )
            {
                if( v is null )
                {
                    return false;
                }

                if( Object.ReferenceEquals( this, v ) )
                {
                    return true;
                }

                if( this.GetType( ) != v.GetType( ) )
                {
                    return false;
                }

                return VertexesAreEquivalent( this, v );
            }

            public static bool operator ==( Vertex left, Vertex right )
            {
                if( left is null )
                {
                    if( right is null )
                    {
                        return true;
                    }

                    return false;
                }

                return left.Equals( right );
            }

            public static bool operator !=( Vertex right, Vertex left )
            {
                return false == ( right == left );
            }

            //--//

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

            //--//

            private bool VertexesAreEquivalent( Vertex left, Vertex right )
            {
                if( left.IncomingEdges.Count != right.IncomingEdges.Count )
                {
                    return false;
                }

                if( left.OutgoingEdges.Count != right.OutgoingEdges.Count )
                {
                    return false;
                }

                foreach( var e1 in left.IncomingEdges )
                {
                    bool same = false;

                    // TODO TODO TODO: make this more efficient.
                    foreach( var e2 in right.IncomingEdges )
                    {
                        if( e1.Source == e2.Source && 
                            e1.Target == e2.Target && 
                            e1.Relation == e2.Relation )
                        {
                            same = true; break;
                        }
                    }

                    if( same == false )
                    {
                        return false;
                    }
                }

                foreach( var e1 in left.OutgoingEdges )
                {
                    bool same = false;

                    // TODO TODO TODO: make this more efficient.
                    foreach( var e2 in right.OutgoingEdges )
                    {
                        if( e1.Source == e2.Source &&
                            e1.Target == e2.Target &&
                            e1.Relation == e2.Relation )
                        {
                            same = true; break;
                        }
                    }

                    if( same == false )
                    {
                        return false;
                    }
                }

                return (
                    left.Key   == right.Key   &&
                    left.Index == right.Index &&
                    left.Value == right.Value &&
                    left.Tag   == right.Tag   &&
                    left.Token == right.Token 
                    );
            }
        }

        [Serializable]
        public class Edge
        {
            public int          Source;
            public int          Target;
            public RelationKind Relation;

            public String ShortLabel => $"{Relation}";

            public String LongLabel => $"[{Source}] -> {Relation} -> [{Target}]";

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

        public override bool Equals( object obj )
        {
            return this.Equals( obj as TokenGraph );
        }

        public bool Equals( TokenGraph graph )
        {
            if( graph is null )
            {
                return false;
            }

            if( Object.ReferenceEquals( this, graph ) )
            {
                return true;
            }

            if( this.GetType( ) != graph.GetType( ) )
            {
                return false;
            }

            return GraphsAreEquivalent( this, graph );
        }

        public static bool operator ==( TokenGraph left, TokenGraph right )
        {
            if( left is null )
            {
                if( right is null )
                {
                    return true;
                }

                return false;
            }

            return left.Equals( right );
        }

        public static bool operator !=( TokenGraph right, TokenGraph left )
        {
            return false == ( right == left );
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
                Console.WriteLine( $"Edge {e.ShortLabel}->{e}: {e.Relation}" );
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

            var e = new Edge { Source = src.Index, Target = tgt.Index, Relation = r };

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
            return this.phraseStructure &&  v.Value.Equals( v.Tag );
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

        private static bool GraphsAreEquivalent( TokenGraph left, TokenGraph right )
        {
            if( left.phraseStructure != right.phraseStructure )
            {
                return false;
            }

            if( left.Edges.Count != right.Edges.Count )
            {
                return false;
            }

            if( left.Vertexes.Count != right.Vertexes.Count )
            {
                return false;
            }

            foreach( var v in left.Vertexes.Values )
            {
                if( v.Equals( right.Vertexes[ v.Index ] ) == false )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
