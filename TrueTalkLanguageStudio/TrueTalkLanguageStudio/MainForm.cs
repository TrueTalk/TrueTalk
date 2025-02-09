

namespace TrueTalk.IrViewer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;
    using TrueTalk.CompilerDiagnostics;
    using TrueTalk.Speech.Grammar;
    using TrueTalk.SpeechRepresentation;
    using static TrueTalk.Speech.Grammar.TokenGraph;

    //--//

    public partial class MainForm : Form
    {
        //
        // State
        //

        private ClauseGraph clauseGraph;
        private string      currentFile;

        //
        // Constructor Methods
        //

        public MainForm( )
        {
            InitializeComponent( );
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            bool loadSucceeded = false;
            if( openFileDialog1.ShowDialog( ) == DialogResult.OK )
            {
                try
                {
                    //
                    // Load clauses from an XML doc. e.g.:
                    //
                    // <?xml version="1.0" encoding="utf-8"?>
                    // <clauses>
                    //   <clause id="1">
                    //     <content>The big brown dog jumped over the lazy fox.</content>
                    //   </clause>
                    //   <clause id="2">
                    //     <content>Work hard and hope for the best.</content>
                    //   </clause>
                    // </clauses>
                    // 
                    if( Path.GetExtension( openFileDialog1.FileName ) == ".txt" )
                    {
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                        doc.Load( openFileDialog1.FileName );

                        foreach( System.Xml.XmlNode clauses in doc.SelectNodes( "clauses" ) )
                        {
                            ClauseListParser parser = new ClauseListParser( clauses );

                            SelectClause( null );

                            UpdateListBox( );

                            //textBoxFilter.Select( );

                            loadSucceeded = true;

                            break;
                        }
                    }
                    //
                    // Load clauses from a persisted graph
                    // 
                    else if( Path.GetExtension( openFileDialog1.FileName ) == ".ttd" )
                    {
                        loadSucceeded = LoadPersistedClause( openFileDialog1.FileName );
                    }
                }
                catch
                {
                }
            }

            if( !loadSucceeded )
            {
                Close( );
            }
            else
            {
                this.currentFile = openFileDialog1.FileName;
            }
        }

        private bool LoadPersistedClause( string fileName )
        {
            var loader = new ClausePersistence( Environment.GetEnvironmentVariable( "TEMP" ) );

            var clause = loader.LoadClause( fileName );

            ClauseWrapper.ClauseGraphs[ clause.Text ] = clause.Graph;

            SelectClause( null );

            UpdateListBox( );

            return true;
        }

        private void SelectClause( ClauseGraph graph )
        {
            if( graph == null )
            {
                clauseGraph = graph;
            }
            else
            {
                listBoxClauses.Visible = false;

                clauseGraph = graph;

                var grammaticalStructureGraph = CreateGraph( clauseGraph.GrammaticalStructure, clauseGraph.Owner.Text );
                var phraseStructureGraph      = CreateGraph( clauseGraph.PhrasalStructure    , clauseGraph.Owner.Text );

                this.gViewer1.Graph                                  = grammaticalStructureGraph;
                this.textBoxGrammaticalStructureGraphPennString.Text = clauseGraph.GrammaticalRepresentation;
                this.gViewer2.Graph                                  = phraseStructureGraph;
                this.textBoxPhraseStructureGraphPennString.Text      = clauseGraph.PhraseRepresentation;

                this.Text = $"IR Viewer - {graph.Owner.Text}";
                this.originalClause.Text = graph.Owner.Text;
            }
        }

        private void UpdateListBox( )
        {
            if( ClauseWrapper.ClauseGraphs != null )
            {
                //string filter = textBoxFilter.Text.ToLower();

                List<string> lst = new List<string>();

                foreach( var clauseGraph in ClauseWrapper.ClauseGraphs.Values )
                {
                    //if( string.IsNullOrEmpty( filter ) || id.ToLower( ).Contains( filter ) )
                    //{
                    lst.Add( clauseGraph.Owner.Text );
                    //}
                }

                lst.Sort( );

                ListBox.ObjectCollection col = listBoxClauses.Items;

                listBoxClauses.SuspendLayout( );

                col.Clear( );

                col.AddRange( lst.ToArray( ) );

                listBoxClauses.ResumeLayout( );

                if( listBoxClauses.Items.Count == 1 )
                {
                    SelectClause( ClauseWrapper.ClauseGraphs[ ( string )listBoxClauses.Items[ 0 ] ] );
                }
                else if( listBoxClauses.Items.Count > 1 )
                {
                    listBoxClauses.Visible = true;
                }
            }
        }

        private void openFileDialog1_FileOk( object sender, EventArgs e )
        {
        }
        
        private void listBoxClauses_Click( object sender, EventArgs e )
        {
            if( ClauseWrapper.ClauseGraphs != null )
            {
                if( listBoxClauses.SelectedItem != null )
                {
                    SelectClause( ClauseWrapper.ClauseGraphs[ ( string )listBoxClauses.SelectedItem ] );
                }
            }
        }

        //--//

        private Microsoft.Glee.Drawing.Graph CreateGraph( TokenGraph graph, string rawClause )
        {
            Microsoft.Glee.Drawing.Graph g = new Microsoft.Glee.Drawing.Graph(string.Format("Graphs for '{0}'", rawClause));

            g.GraphAttr.NodeAttr.Padding = 3;

            foreach( var e in graph.Edges )
            {
                Microsoft.Glee.Drawing.Edge edgeG = g.AddEdge( graph.Vertexes[ e.Source ].Key, graph.Vertexes[ e.Target ].Key) as Microsoft.Glee.Drawing.Edge;
                Microsoft.Glee.Drawing.EdgeAttr attr = edgeG.Attr;

                attr.Label = e.Relation;
                attr.Fontsize -= 4;
            }

            foreach( Vertex v in graph.Vertexes.Values )
            {
                Microsoft.Glee.Drawing.Node node = CreateNode( g, v );
            }

            return g;
        }

        private static Microsoft.Glee.Drawing.Node CreateNode( Microsoft.Glee.Drawing.Graph graph, Vertex v )
        {
            Microsoft.Glee.Drawing.Node node = graph.AddNode(v.Key) as Microsoft.Glee.Drawing.Node;
            Microsoft.Glee.Drawing.NodeAttr attr = node.Attr;

            if( v.Value == "ROOT" )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Box;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.Green;
            }
            else if( v.Token != null && v.Token.GetType( ) == typeof( Achronym ) )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Box;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.Tan;
            }
            else if( v.Token != null && v.Token.GetType( ) == typeof( Word ) )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Box;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.LightGray;
            }
            else if( v.Token != null && v.Token.GetType( ) == typeof( MathematicalSymbol ) )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.Cyan;
            }
            else if( v.Token != null && v.Token.GetType( ) == typeof( Number ) )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Circle;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.LightCyan;
            }
            else if( v.Token != null && v.Token.GetType( ) == typeof( Punctuation ) )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Diamond;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.Purple;
            }
            else
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Box;
            }

            return node;
        }

        //--//

        private void MainForm_Resize( object sender, EventArgs e )
        {
            const int margin = 3;
            gViewer1.Height = splitContainer1.Panel1.Height - margin;
            gViewer2.Height = splitContainer2.Panel1.Height - margin;

            //listBoxClauses.Width = textBoxFilter.Width;
        }

        private void forward_Click( object sender, EventArgs e )
        {
            var nextFile = GetNextFile( this.currentFile );

            if( File.Exists( nextFile ) == false )
            {
                return;
            }

            if(LoadPersistedClause( nextFile ))
            {
                this.currentFile = nextFile;
            }
        }

        private void backward_Click( object sender, EventArgs e )
        {
            var prevFile = GetPreviousFile( this.currentFile );

            if( File.Exists( prevFile ) == false )
            {
                return;
            }
            
            if( LoadPersistedClause( prevFile ) )
            {
                this.currentFile = prevFile;
            }
        }

        private string GetNextFile( string currentFile )
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension( currentFile );
            var directory = Path.GetDirectoryName( currentFile );

            int nextVersion = (int)((int)fileNameWithoutExtension[ fileNameWithoutExtension.Length - 1 ] - (int)'0') + 1;

            var nextFile = Path.Combine( directory, fileNameWithoutExtension.Substring( 0, fileNameWithoutExtension.Length - 1 ) + nextVersion + ".ttd" );

            return nextFile;
        }

        private string GetPreviousFile( string currentFile )
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension( currentFile );
            var directory = Path.GetDirectoryName( currentFile );

            int nextVersion = (int)((int)fileNameWithoutExtension[ fileNameWithoutExtension.Length - 1 ] - (int)'0') - 1;

            var nextFile = Path.Combine( directory, fileNameWithoutExtension.Substring( 0, fileNameWithoutExtension.Length - 1 ) + nextVersion + ".ttd" );

            return nextFile;
        }
    }
}






//private void gViewer1_SelectionChanged(object sender,
//                                        EventArgs e)
//{
//    if (m_highlightedObject != null)
//    {
//        if (m_highlightedObject is Microsoft.Glee.Drawing.Edge)
//        {
//            Microsoft.Glee.Drawing.Edge edge = (Microsoft.Glee.Drawing.Edge)m_highlightedObject;

//            edge.Attr = m_highlightedObjectAttr as Microsoft.Glee.Drawing.EdgeAttr;
//        }
//        else if (m_highlightedObject is Microsoft.Glee.Drawing.Node)
//        {
//            Microsoft.Glee.Drawing.Node node = (Microsoft.Glee.Drawing.Node)m_highlightedObject;

//            node.Attr = m_highlightedObjectAttr as Microsoft.Glee.Drawing.NodeAttr;
//        }

//        m_highlightedObject = null;
//        m_highlightedObjectAttr = null;
//    }

//    if (gViewer1.SelectedObject != null)
//    {
//        m_highlightedObject = gViewer1.SelectedObject;

//        if (m_highlightedObject is Microsoft.Glee.Drawing.Edge)
//        {
//            Microsoft.Glee.Drawing.Edge edge = (Microsoft.Glee.Drawing.Edge)m_highlightedObject;

//            m_highlightedObjectAttr = edge.Attr.Clone();

//            edge.Attr.Color = Microsoft.Glee.Drawing.Color.Magenta;
//            edge.Attr.Fontcolor = Microsoft.Glee.Drawing.Color.Magenta;
//        }
//        else if (m_highlightedObject is Microsoft.Glee.Drawing.Node)
//        {
//            Microsoft.Glee.Drawing.Node node = (Microsoft.Glee.Drawing.Node)m_highlightedObject;

//            m_highlightedObjectAttr = node.Attr.Clone();

//            node.Attr.Color = Microsoft.Glee.Drawing.Color.Magenta;
//            node.Attr.Fontcolor = Microsoft.Glee.Drawing.Color.Magenta;
//        }
//    }

//    gViewer1.Invalidate();
//}

//private void gViewer1_MouseClick(object sender,
//                                  MouseEventArgs e)
//{
//    // Do nothing if we didn't click on a node or click on the same node 
//    if (m_highlightedObject is Microsoft.Glee.Drawing.Node && m_highlightedObject != m_selectedNode)
//    {
//        if (m_selectedNode != null)
//        {
//            m_selectedNode.Attr = m_selectedNodeAttr;
//            m_selectedNode = null;
//            m_selectedNodeAttr = null;
//        }

//        Microsoft.Glee.Drawing.Node node = (Microsoft.Glee.Drawing.Node)m_highlightedObject;

//        foreach (BasicBlock bb in m_method.BasicBlocks)
//        {
//            if (bb.Id == node.Id)
//            {
//                SelectBasicBlock(bb);
//                m_selectedNode = node;

//                // Save the original / untempered style so when we can go back to it when the node is unselected
//                m_selectedNodeAttr = ((Microsoft.Glee.Drawing.NodeAttr)m_highlightedObjectAttr).Clone();

//                // Apply the selected style to both the node and the saved styled from highlight so when
//                // mouse moves away, the node remain selected
//                ((Microsoft.Glee.Drawing.NodeAttr)m_highlightedObjectAttr).Fillcolor = Microsoft.Glee.Drawing.Color.Goldenrod;
//                node.Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Goldenrod;

//                break;
//            }
//        }

//        gViewer1.Invalidate();
//    }
//}