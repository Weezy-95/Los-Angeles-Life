using AltV.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Los_Angeles_Life.Vehicles
{
    public class ServerVehicle
    {
        public long Id { get; set; }
        public int SessionId { get; set; }
        public int VehicleTemplateId { get; set; }
        public int FactionId { get; set; }
        public int GarageStorageId { get; set; }
        public string Owner { get; set; }
        public float Fuel { get; set; }
        public float Mileage { get; set; }
        public bool IsEngineHealthy { get; set; }
        public bool IsLocked { get; set; }
        public bool IsInGarage { get; set; }
        public Position VehiclePosition { get; set; }
        public Rotation VehicleRotation { get; set; }
        public string Plate { get; set; }
    }
}
