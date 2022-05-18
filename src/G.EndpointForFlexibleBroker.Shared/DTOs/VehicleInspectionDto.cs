using G.EndpointForFlexibleBroker.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace G.EndpointForFlexibleBroker.Shared.DTOs
{
    /// <summary>
    /// Results of visual inspection of cars in specified locations
    /// </summary>
    public class VehicleInspectionDto : IEntityWithBrokerDeclaration
    {
        /// <summary>
        /// Destination broker name.
        /// </summary>
        [Required]
        public string BrokerName { get; }
        /// <summary>
        /// Car plate number readed after visual inspection
        /// </summary>
        [Required]
        public string PlateNumber { get; }
        /// <summary>
        /// Location Id, where the inspection takes place
        /// </summary>
        [Range(1, uint.MaxValue)]
        public uint LocationId { get; }
        /// <summary>
        /// Epoch time of inspection in miliseconds
        /// </summary>
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