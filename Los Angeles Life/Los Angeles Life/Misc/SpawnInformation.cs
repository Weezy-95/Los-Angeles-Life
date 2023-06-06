using AltV.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
