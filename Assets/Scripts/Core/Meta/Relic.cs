namespace Pantheon.Core.Meta
{
    public class Relic
    {
        public string Name { get; }
        public string FlavorText { get; }

        public Relic(string name, string flavorText)
        {
            Name = name;
            FlavorText = flavorText;
        }
    }
}
