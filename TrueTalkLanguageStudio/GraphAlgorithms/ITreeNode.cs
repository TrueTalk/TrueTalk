﻿//
// Copyright (c) Microsoft Corporation.    All rights reserved.
//


namespace TrueTalk.GraphsAlgorithms
{
    using System;

    public interface ITreeNode<FC>
    {
        int SpanningTreeIndex { get; set; }

        ITreeEdge<FC>[ ] Predecessors { get; }

        ITreeEdge<FC>[ ] Successors { get; }

        FC FlowControl { get; }
    }
}