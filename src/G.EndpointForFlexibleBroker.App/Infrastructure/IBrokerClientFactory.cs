using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;

namespace G.EndpointForFlexibleBroker.App.Infrastructure
{
    public interface IBrokerClientFactory
    {
        Result<IBrokerClient> Get(string brokerName);
    }
}
