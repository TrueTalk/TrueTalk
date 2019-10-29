

namespace TrueTalk.IrViewer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using TrueTalk.Speech.Grammar;
    using static TrueTalk.Speech.Grammar.TokenGraph;

    //--//

    public partial class MainForm : Form
    {
        //
        // State
        //

        ClauseGraph m_clauseGraph;

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
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                    doc.Load( openFileDialog1.FileName );

                    foreach( System.Xml.XmlNode clauses in doc.SelectNodes( "clauses" ) )
                    {
                        Parser parser = new Parser(clauses);

                        SelectClause( null );

                        UpdateListBox( );

                        //textBoxFilter.Select( );

                        loadSucceeded = true;

                        break;
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
        }

        private void SelectClause( ClauseGraph graph )
        {
            if( graph == null )
            {
                m_clauseGraph = graph;
            }
            else
            {
                listBoxClauses.Visible = false;

                m_clauseGraph = graph;

                var grammaticalStructureGraph = CreateGraph( m_clauseGraph.GrammaticalStructure, m_clauseGraph.Owner.Text );
                var phraseStructureGraph      = CreateGraph( m_clauseGraph.PhrasalStructure    , m_clauseGraph.Owner.Text );

                this.gViewer1.Graph = grammaticalStructureGraph;
                this.textBoxGrammaticalStructureGraphPennString.Text = m_clauseGraph.GrammaticalRepresentation;
                this.gViewer2.Graph = phraseStructureGraph;
                this.textBoxPhraseStructureGraphPennString.Text = m_clauseGraph.PhraseRepresentation;
            }
        }

        private void UpdateListBox( )
        {
            if( Clause.ClauseGraphs != null )
            {
                //string filter = textBoxFilter.Text.ToLower();

                List<string> lst = new List<string>();

                foreach( var clauseGraph in Clause.ClauseGraphs.Values )
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

                listBoxClauses.Visible = listBoxClauses.Items.Count > 0;
            }
        }

        private void listBoxClauses_Click( object sender, EventArgs e )
        {
            if( Clause.ClauseGraphs != null )
            {
                if( listBoxClauses.SelectedItem != null )
                {
                    SelectClause( Clause.ClauseGraphs[ ( string )listBoxClauses.SelectedItem ] );
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
            else if( v.Value == "Leaf" )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Box;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.Red;
            }
            else if( v.Value == "Blah" )
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Box;
                attr.Fillcolor = Microsoft.Glee.Drawing.Color.Purple;
            }
            else
            {
                attr.Shape = Microsoft.Glee.Drawing.Shape.Box;
            }

            return node;
        }

        private void openFileDialog1_FileOk( object sender, CancelEventArgs e )
        {

        }

        //--//

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

        private void MainForm_Resize( object sender, EventArgs e )
        {
            const int margin = 3;
            gViewer1.Height = splitContainer1.Panel1.Height - margin;
            gViewer2.Height = splitContainer2.Panel1.Height - margin;

            //listBoxClauses.Width = textBoxFilter.Width;
        }

    }
}