using System.ComponentModel.DataAnnotations;

namespace G.EndpointForFlexibleBroker.Shared.Interfaces
{
    public interface IEntityWithBrokerDeclaration
    {
        public string BrokerName { get; }
    }
}