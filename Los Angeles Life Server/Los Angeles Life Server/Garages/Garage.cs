using AltV.Net.Data;
using Los_Angeles_Life_Server.Misc;
using AltV.Net.Elements.Entities;

namespace Los_Angeles_Life_Server.Garages
{
    public class Garage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position Location { get; set; }
        public int BlipId { get; set; }
        public int BlipColorId { get; set; }
        public List<SpawnInformation> SpawnPositionInformationList { get; set; }
        public List<SpawnInformation> StoragePositionInformationList { get; set; }
        public List<IVehicle> VehiclesToStore { get; set; }

        public Garage(int id, string name, Position location, int blipId, int blipColorId)
        {
            SpawnPositionInformationList = new List<SpawnInformation>();
            StoragePositionInformationList = new List<SpawnInformation>();
            VehiclesToStore = new List<IVehicle>();

            Id = id;
            Name = name;
            Location = location;
            BlipId = blipId;
            BlipColorId = blipColorId;
        }
    }
}
