using EventHubMock;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients.AzureEventHub
{
    public interface IAzureEventHubProducerClientFactory
    {
        Result<EventHubProducerClient> Get(string connectionString, string eventHubName);
    }
}