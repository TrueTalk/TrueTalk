//
// Copyright (c) Microsoft Corporation.    All rights reserved.
//

namespace TrueTalk.IrViewer
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using TrueTalk.Speech.Grammar;
    using TrueTalk.SpeechRepresentation;

    public class Clause
    {
        //
        // State
        //

        public string Id;
        public string Text;

        public static Dictionary<string, ClauseGraph> ClauseGraphs = new Dictionary<string, ClauseGraph>(); 
    }

    public class Parser
    {
        private readonly ClauseGraphFactory Factory = new ClauseGraphFactory( @"C:\src\TrueTalk\TrueTalkLanguageStudio\Resources\Models\englishPCFG.ser.gz" );

        //
        // Constructor Methods
        //

        public Parser( XmlNode node )
        {
            foreach(XmlNode subnode in node.SelectNodes( "clause" ))
            {
                Clause clause = ParseClause( subnode );

                var clauseGraph = Factory.FromString( clause.Text );

                Clause.ClauseGraphs[ clause.Text ] = clauseGraph;
            }

            //////clauseGraph.GrammaticalStructure.Display( );
            //////clauseGraph.PhrasalStructure.Display( );
        }

        //--//

        private Clause ParseClause( XmlNode node )
        {
            Clause res = new Clause(); 

            res.Id = GetAttribute( node, "id" );

            foreach(XmlNode subnode in node.SelectNodes( "content" ))
            {
                ParseClause( subnode, res );
            }

            return res;
        }

        private void ParseClause( XmlNode node, Clause res )
        {
            res.Text = node.InnerText;
        }

        //////private void ParseToken( XmlNode node, Clause method, Token tk, Dictionary< string, Token > lookupBasicBlock )
        //////{
        //////    foreach(XmlNode subnode in node.SelectNodes( "Edge" ))
        //////    {
        //////        method.TokenEdges.Add( ParseBasicBlockEdge( subnode, lookupBasicBlock ) );
        //////    }
        //////}

        //////private TokenEdge ParseBasicBlockEdge( XmlNode node, Dictionary< string, Token > lookupToken )
        //////{
        //////    TokenEdge res = new TokenEdge();

        //////    res.From = lookupToken[ GetAttribute( node, "From" ) ];
        //////    res.To   = lookupToken[ GetAttribute( node, "To"   ) ];
        //////    res.Kind = GetAttribute( node, "Kind" )  ;

        //////    return res;
        //////}

        //--//

        private static string GetAttribute( XmlNode node, string name )
        {
            return node.Attributes.GetNamedItem( name ) is XmlAttribute attrib ? attrib.Value : null;
        }
    }
}
