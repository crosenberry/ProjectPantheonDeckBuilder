using System.Collections.Generic;

namespace Pantheon.Core.Run
{
    public class MapNode
    {
        public NodeType Type { get; }
        public IReadOnlyList<int> NextNodeIndices { get; }

        public MapNode(NodeType type, IReadOnlyList<int> nextNodeIndices)
        {
            Type = type;
            NextNodeIndices = nextNodeIndices;
        }
    }
}
