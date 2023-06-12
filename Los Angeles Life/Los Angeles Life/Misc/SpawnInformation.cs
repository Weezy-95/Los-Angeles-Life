using AltV.Net.Data;

namespace Los_Angeles_Life.Misc
{
    public class SpawnInformation
    {
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }

        public SpawnInformation(Position position, Rotation rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
