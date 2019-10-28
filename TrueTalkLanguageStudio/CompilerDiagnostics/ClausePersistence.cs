

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
    using TrueTalk.Speech.Grammar;

    //--//

    public class ClausePersistence
    {
        private readonly string workspaceDirectory;

        //--//

        public ClausePersistence( string workspaceDirectory )
        {
            var compilerDiagnosticsPath = Path.Combine( workspaceDirectory + @"Compiler\Trasformations\" );

            Directory.CreateDirectory( compilerDiagnosticsPath );

            this.workspaceDirectory = compilerDiagnosticsPath;
        }

        //--//

        public string PersistClause( Clause clause, string compilerVersion, string workingDirectory )
        {
            //https://stackoverflow.com/questions/3671259/how-to-xml-serialize-a-dictionary

            var fileName = this.workspaceDirectory + clause.Text + "__" + clause.Version.ToString( ) + ".ttd";

            XmlSerializer mySerializer = new XmlSerializer(typeof(Clause));

            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter( fileName );
            mySerializer.Serialize( myWriter, clause );
            myWriter.Close( );

            return fileName;
        }

        public Clause LoadClause( string fileName )
        {
            return new Clause( "blah" );
        }

        //////public string PersistClause( Clause clause, string compilerVersion, string workingDirectory )
        //////{
        //////    var settings = new XmlWriterSettings( );
        //////    settings.Async = false;

        //////    var fileName = this.workspaceDirectory + clause.Text + "__" + clause.Version.ToString( ) + ".ttd";

        //////    using( var fs = new FileStream( fileName, FileMode.Create, FileAccess.Write ) )
        //////    {
        //////        using( var writer = XmlWriter.Create( fs, settings ) )
        //////        {
        //////            writer.WriteStartDocument( );

        //////            writer.WriteComment( "This is the persisted version of a TrueTalk clause and its underlying phrasal and grammatical structure graphs." );
        //////            writer.WriteComment( $"Compiler version is '{compilerVersion}'." );
        //////            writer.WriteComment( $"Original clause is '{clause.Text}'." );

        //////            //writer.WriteStartElement( "CompilerVersion" );
        //////            //writer.WriteAttributeString( "Version", compilerVersion );
        //////            //writer.WriteEndElement( );

        //////            {
        //////                writer.WriteStartElement( "Clause" );
        //////                {
        //////                    writer.WriteAttributeString( "Text", clause.Text );
        //////                    writer.WriteAttributeString( "Version", clause.Version.ToString( ) );
        //////                    //--//
        //////                    writer.WriteStartElement( "ClauseGraph" );
        //////                    {
        //////                        //
        //////                        // Grammatical structure.
        //////                        //
        //////                        writer.WriteStartElement( "TokenGraph" );
        //////                        {
        //////                            writer.WriteAttributeString( "Type", "Grammatical Structure" );
        //////                            writer.WriteAttributeString( "Representation", clause.Graph.GrammaticalRepresentation );
        //////                            writer.WriteAttributeString( "Version", clause.Graph.GrammaticalStructure.ToString( ) );

        //////                            PersistTokenGraph( writer, clause.Graph.GrammaticalStructure );
        //////                        }

        //////                        //
        //////                        // Phrasal structure.
        //////                        //
        //////                        writer.WriteStartElement( "TokenGraph" );
        //////                        {
        //////                            writer.WriteAttributeString( "Type", "Phrasal Structure" );
        //////                            writer.WriteAttributeString( "Representation", clause.Graph.GrammaticalRepresentation );
        //////                            writer.WriteAttributeString( "Version", clause.Graph.GrammaticalStructure.ToString( ) );

        //////                            PersistTokenGraph( writer, clause.Graph.PhrasalStructure );
        //////                        }
        //////                    }
        //////                    writer.WriteEndElement( );
        //////                }
        //////                writer.WriteEndElement( );
        //////            }
        //////            writer.WriteEndDocument( );
        //////        }
        //////    }

        //////    return fileName;
        //////}

        //////public Clause LoadClause( string fileName )
        //////{
        //////    Clause      clause      = default;
        //////    ClauseGraph clauseGraph = default;

        //////    var settings = new XmlReaderSettings( );
        //////    settings.Async = false;

        //////    using( var fs = new FileStream( fileName, FileMode.Open, FileAccess.Read ) )
        //////    {
        //////        using( var reader = XmlReader.Create( fs, settings ) )
        //////        {
        //////            while( reader.Read( ) )
        //////            {
        //////                switch(reader.NodeType)
        //////                {
        //////                    case XmlNodeType.Element:
        //////                        {
        //////                            switch(reader.Name)
        //////                            {
        //////                                case "Clause":
        //////                                    {
        //////                                        string version = String.Empty;
        //////                                        string text    = String.Empty;

        //////                                        reader.MoveToFirstAttribute( );

        //////                                        do
        //////                                        {
        //////                                            if( reader.Name == "Version" )
        //////                                            {
        //////                                                version = reader.Value;
        //////                                            }
        //////                                            else if( reader.Name == "Text" )
        //////                                            {
        //////                                                text = reader.Value;
        //////                                            }
        //////                                            else
        //////                                            {
        //////                                                CHECKS.ASSERT( false, $"Malformed document {fileName}." );
        //////                                            }

        //////                                        } while( reader.MoveToNextAttribute( ) );

        //////                                        clause = new Clause( text, Int32.Parse( version ) );
        //////                                    }
        //////                                    break;

        //////                                case "ClauseGraph":
        //////                                    {
        //////                                        reader.Read( );

        //////                                        var tokenGraph = LoadTokenGraph( reader );

        //////                                    }
        //////                                    break;
        //////                            }
        //////                        }
        //////                        break;
        //////                    case XmlNodeType.EndElement:
        //////                        {
        //////                        }
        //////                        break;

        //////                }
        //////                //CHECKS.ASSERT( reader.Value == "CompilerVersion", $"Compiler version is not present in document {fileName}" );

        //////                //...
        //////            }
        //////        }
        //////    }

        //////    return new Clause( "" );
        //////}

        //////private TokenGraph LoadTokenGraph( XmlReader reader )
        //////{
        //////    string type           = String.Empty;
        //////    string representation = String.Empty;
        //////    string version        = String.Empty;

        //////    reader.MoveToFirstAttribute( );

        //////    do
        //////    {
        //////        if( reader.Name == "Type" )
        //////        {
        //////            type = reader.Value;
        //////        }
        //////        else if( reader.Name == "Representation" )
        //////        {
        //////            representation = reader.Value;
        //////        }
        //////        else if( reader.Name == "Version" )
        //////        {
        //////            version = reader.Value;
        //////        }
        //////        else
        //////        {
        //////            CHECKS.ASSERT( false, $"Malformed document." );
        //////        }

        //////    } while( reader.MoveToNextAttribute( ) );


        //////    var tokenGraph = new TokenGraph( type == "Phrasal Structure" );

        //////    if( reader.Read( ) )
        //////    {
        //////        if(reader.Name == "Vertexes")
        //////        {
        //////            reader.Read( );

        //////            switch(reader.Name)
        //////            {
        //////                case "Vertex":
        //////                    {
        //////                        // <Vertex Key="Lorenzo-3" Index="3" Tag="Lorenzo" Value="NNP" Token="">
        //////                        string key = String.Empty;
        //////                        string index = String.Empty;
        //////                        string tag = String.Empty;
        //////                        string value = String.Empty;
        //////                        string token = String.Empty;

        //////                        reader.MoveToFirstAttribute( );

        //////                        do
        //////                        {
        //////                            if( reader.Name == "Key" )
        //////                            {
        //////                                key = reader.Value;
        //////                            }
        //////                            else if( reader.Name == "Index" )
        //////                            {
        //////                                index = reader.Value;
        //////                            }
        //////                            else if( reader.Name == "Tag" )
        //////                            {
        //////                                tag = reader.Value;
        //////                            }
        //////                            else if( reader.Name == "Value" )
        //////                            {
        //////                                value = reader.Value;
        //////                            }
        //////                            else if( reader.Name == "Token" )
        //////                            {
        //////                                token = reader.Value;
        //////                            }
        //////                            else
        //////                            {
        //////                                CHECKS.ASSERT( false, $"Malformed document." );
        //////                            }

        //////                        } while( reader.MoveToNextAttribute( ) );
        //////                    }
        //////                    break;
        //////            }
        //////        }
        //////    }

        //////    return tokenGraph;
        //////}

        //--//

        //////private void PersistTokenGraph( XmlWriter writer, TokenGraph tokenGraph )
        //////{
        //////    writer.WriteStartElement( "Vertexes" );
        //////    {
        //////        writer.WriteAttributeString( "Count", tokenGraph.Vertexes.Count.ToString( ) );

        //////        foreach( var vertex in tokenGraph.Vertexes.Values )
        //////        {
        //////            writer.WriteStartElement( "Vertex" );
        //////            {
        //////                writer.WriteAttributeString( "Key", vertex.Key.ToString( ) );
        //////                writer.WriteAttributeString( "Index", vertex.Index.ToString( ) );
        //////                writer.WriteAttributeString( "Tag", vertex.Value.ToString( ) );
        //////                writer.WriteAttributeString( "Value", vertex.Tag.ToString( ) );
        //////                writer.WriteAttributeString( "Token", vertex.Token?.ToString( ) );
        //////                //--//
        //////                writer.WriteStartElement( "Edges" );
        //////                {
        //////                    foreach( var edge in tokenGraph.Edges )
        //////                    {
        //////                        writer.WriteStartElement( "Edge" );
        //////                        {
        //////                            writer.WriteAttributeString( "Source", edge.Source.Index.ToString( ) );
        //////                            writer.WriteAttributeString( "Target", edge.Target.Index.ToString( ) );
        //////                            writer.WriteAttributeString( "Relation", edge.Relation.ToString( ) );
        //////                        }
        //////                        writer.WriteEndElement( );
        //////                    }
        //////                    writer.WriteEndElement( );
        //////                }
        //////                writer.WriteEndElement( );
        //////            }
        //////            writer.WriteEndElement( );
        //////        }
        //////    }
        //////    writer.WriteEndElement( );
        //////}


    }
}
