using System.Collections.Generic;
using System.Linq;

namespace Pantheon.Core.Run
{
    public class Stage
    {
        private readonly IReadOnlyList<int> _entryNodeIndices;

        public IReadOnlyList<MapNode> Nodes { get; }
        public int? CurrentNodeIndex { get; private set; }

        public Stage(IReadOnlyList<MapNode> nodes, IReadOnlyList<int> entryNodeIndices)
        {
            Nodes = nodes;
            _entryNodeIndices = entryNodeIndices;
        }

        public IReadOnlyList<int> AvailableNodeIndices =>
            CurrentNodeIndex.HasValue ? Nodes[CurrentNodeIndex.Value].NextNodeIndices : _entryNodeIndices;

        public bool IsComplete => CurrentNodeIndex.HasValue && Nodes[CurrentNodeIndex.Value].Type == NodeType.Boss;

        public void MoveTo(int nodeIndex)
        {
            if (!AvailableNodeIndices.Contains(nodeIndex))
            {
                throw new System.InvalidOperationException($"Node {nodeIndex} is not reachable from the current position.");
            }

            CurrentNodeIndex = nodeIndex;
        }
    }
}
