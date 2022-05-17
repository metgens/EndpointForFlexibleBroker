using G.EndpointForFelxibleBroker.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace G.EndpointForFelxibleBroker.Shared.DTOs
{
    /// <summary>
    /// Results of visual inspection of cars in specified locations
    /// </summary>
    public class VehicleInspectionDto : IEntityWithBrokerDeclaration
    {
        [Required]
        public string BrokerName { get; }
        [Required]
        public string PlateNumber { get; }
        [Range(1, uint.MaxValue)]
        public uint LocationId { get; }
        [Range(1, ulong.MaxValue)]
        public ulong EpochTimeMs { get; }

        public VehicleInspectionDto(string brokerName, string plateNumber, uint locationId, ulong epochTimeMs)
        {
            BrokerName = brokerName;
            PlateNumber = plateNumber;
            LocationId = locationId;
            EpochTimeMs = epochTimeMs;
        }
    }
}