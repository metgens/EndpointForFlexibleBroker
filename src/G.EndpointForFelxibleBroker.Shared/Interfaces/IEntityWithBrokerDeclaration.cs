using System.ComponentModel.DataAnnotations;

namespace G.EndpointForFelxibleBroker.Shared.Interfaces
{
    public interface IEntityWithBrokerDeclaration
    {
        public string BrokerName { get; }
    }
}