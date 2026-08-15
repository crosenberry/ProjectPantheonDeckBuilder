namespace Pantheon.Core.Run.Greek
{
    public static class GreekStages
    {
        // Minimal sample layout for the first playable run slice, same role the
        // minimal Greek enemy sample set plays for combat content: a linear
        // Combat -> Combat -> Boss path. Real node counts/branching are content
        // tuning, deferred per the M4 design session.
        public static Stage SampleStage()
        {
            var nodes = new[]
            {
                new MapNode(NodeType.Combat, new[] { 1 }),
                new MapNode(NodeType.Combat, new[] { 2 }),
                new MapNode(NodeType.Boss, new int[0])
            };

            return new Stage(nodes, new[] { 0 });
        }
    }
}
