
namespace TrueTalk.Speech.Grammar
{
    using System;
    using TrueTalk.Interfaces;

    public class Clause : TransformableItem
    {
        private Clause( )
        {
            Text = String.Empty;
        }

        public Clause( string rawText )
        {
            Text = rawText;
        }

        public Clause( string rawText, int version ) : base( version )
        {
            Text = rawText;
        }

        //--//

        public override bool Equals( object obj )
        {
            return this.Equals( obj as Clause );
        }

        public bool Equals( Clause other )
        {
            if( other is null )
            {
                return false;
            }

            if( Object.ReferenceEquals( this, other ) )
            {
                return true;
            }

            if( this.GetType( ) != other.GetType( ) )
            {
                return false;
            }

            return 
                this.Text                       == other.Text                       &&                                           
                this.Graph.GrammaticalStructure == other.Graph.GrammaticalStructure &&
                this.Graph.PhrasalStructure     == other.Graph.PhrasalStructure     ;
        }

        public static bool operator ==( Clause left, Clause right )
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

        public static bool operator !=( Clause right, Clause left )
        {
            return false == ( right == left );
        }

        public override int GetHashCode( )
        {
            return this.Version.GetHashCode( ) ^ this.Text.GetHashCode( ) ^ this.Graph.GetHashCode( );
        }

        //--//

        public string Text { get; }
        
        public ClauseGraph Graph { get; set; } = new ClauseGraph( );

        public override bool ApplyTransformation( IAnalysis analysis )
        {
            bool fChanged = this.Graph.ApplyTransformation( analysis );

            if(fChanged)
            {
                this.BumpVersion( );
            }

            return fChanged;
        }
    }
}
