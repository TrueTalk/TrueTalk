//
// Copyright (c) TrueTalk LLC.    All rights reserved.
//


namespace TrueTalk.GraphsAlgorithms
{
    public interface ITreeNode<FC>
    {
        int SpanningTreeIndex { get; set; }

        ITreeEdge<FC>[ ] Predecessors { get; }

        ITreeEdge<FC>[ ] Successors { get; }

        FC FlowControl { get; }
    }
}