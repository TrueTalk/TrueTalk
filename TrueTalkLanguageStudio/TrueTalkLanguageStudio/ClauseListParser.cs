//
// Copyright (c) TrueTalk LLC.    All rights reserved.
//

namespace TrueTalk.IrViewer
{
    using System.Collections.Generic;
    using System.Xml;
    using TrueTalk.Speech.Grammar;

    //--//

    public class ClauseWrapper
    {
        public static Dictionary<string, ClauseGraph> ClauseGraphs = new Dictionary<string, ClauseGraph>();

        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class ClauseListParser
    {
        private readonly ClauseGraphFactory Factory = new ClauseGraphFactory( @"C:\src\TrueTalk\TrueTalkLanguageStudio\Resources\Models\englishPCFG.ser.gz" );

        //
        // Constructor Methods
        //

        public ClauseListParser( XmlNode node )
        {
            foreach( XmlNode subnode in node.SelectNodes( "Text" ) )
            {
                ClauseWrapper clause = ParseClause( subnode );

                var clauseGraph = Factory.FromString( clause.Text );

                ClauseWrapper.ClauseGraphs[ clause.Text ] = clauseGraph.Graph;
            }
        }

        //--//

        private ClauseWrapper ParseClause( XmlNode node )
        {
            ClauseWrapper res = new ClauseWrapper();

            res.Id = GetAttribute( node, "id" );

            foreach( XmlNode subnode in node.SelectNodes( "content" ) )
            {
                ParseClause( subnode, res );
            }

            return res;
        }

        private void ParseClause( XmlNode node, ClauseWrapper res )
        {
            res.Text = node.InnerText;
        }

        //--//

        private static string GetAttribute( XmlNode node, string name )
        {
            return node.Attributes.GetNamedItem( name ) is XmlAttribute attrib ? attrib.Value : null;
        }
    }
}
