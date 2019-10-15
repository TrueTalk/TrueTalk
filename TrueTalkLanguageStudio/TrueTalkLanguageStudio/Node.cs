
namespace TrueTalkLanguageStudio
{
    using System;
    using System.Collections.Generic;

    public class Node
    {
        List<Node> childNodes;

        public Node()
        {
        }

        public Node( string text )
        {
            this.Text = text;
        }

        public List<Node> ChildNodes
        {
            get
            {
                if( this.childNodes == null )
                {
                    this.childNodes = new List<Node>();
                }

                return this.childNodes;
            }
        }

        public string Text { get; set; }
    }
}