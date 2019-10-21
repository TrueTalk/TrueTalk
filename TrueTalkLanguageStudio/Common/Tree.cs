//
// Copyright (c) Microsoft Corporation.    All rights reserved.
//

namespace TrueTalk.Common
{
    using System;

    public class ExpressionTree<T>
    {
        public enum EdgeKind
        {
            Not,
            And,
            Or,
            Nor,
            Nand,
            Xor,
            Imply,
        }

        //--//

        public class Edge<T>
        {
            public ExpressionTree<T> Source;
            public ExpressionTree<T> Target;
            public EdgeKind          Kind;

            public Edge( ExpressionTree<T> s, ExpressionTree<T> t, EdgeKind k )
            {
                CHECKS.ASSERT_NOT_NULL( s, "A node in an expression tree cannot be null." );
                CHECKS.ASSERT_NOT_NULL( t, "A node in an expression tree cannot be null." );

                Source = s;
                Target = t;
                Kind   = k;
            }
        }

        //--//

        public Edge<T> Left;
        public Edge<T> Right;
        public T       Value;

        //--//

        public ExpressionTree( T v )
        {
            CHECKS.ASSERT( v != null, "Cannot insert a null node is an expression tree!" );

            Left  = Right = null;
            Value = v;
        }

        public void InsertLeft( ExpressionTree<T> tree, EdgeKind k )
        {
            this.Left = new Edge<T>( this, tree, k );
        }

        public void InsertRight( ExpressionTree<T> tree, EdgeKind k )
        {
            this.Right = new Edge<T>( this, tree, k );
        }

        public Edge<T> RemoveLeft( )
        {
            var l = this.Left;

            this.Left = null;

            return l;
        }

        public Edge<T> RemoveRight( )
        {
            var r = this.Right;

            this.Right = null;

            return r;
        }

        public Edge<T> ExchangeLeft( ExpressionTree<T> tree, EdgeKind k )
        {
            var l = this.Left;

            this.Left = new Edge<T>( this, tree, k );

            return l;
        }

        public Edge<T> ExchangeRight( ExpressionTree<T> tree, EdgeKind k )
        {
            var r = this.Right;

            this.Right = new Edge<T>( this, tree, k );

            return r;
        }
    }
}
