using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;
using G.EndpointForFlexibleBroker.Shared;

namespace G.EndpointForFlexibleBroker.App.Infrastructure
{
    public interface IBrokerClientFactory
    {
        Result<IBrokerClient> Get(string brokerName);
    }
}
