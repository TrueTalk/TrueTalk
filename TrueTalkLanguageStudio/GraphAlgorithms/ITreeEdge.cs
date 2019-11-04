//
// Copyright (c) TrueTalk LLC.    All rights reserved.
//


namespace TrueTalk.GraphsAlgorithms
{
    public interface ITreeEdge<FC>
    {
        ITreeNode<FC> Predecessor { get; }

        ITreeNode<FC> Successor { get; }

        EdgeClass EdgeClass { get; set; }
    }
}