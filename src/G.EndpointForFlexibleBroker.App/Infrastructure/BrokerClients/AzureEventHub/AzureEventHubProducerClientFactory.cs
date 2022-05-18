using EventHubMock;
using G.EndpointForFlexibleBroker.Shared;
using System.Collections.Concurrent;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients.AzureEventHub
{

    /// <summary>
    /// Creates the EventHubProducerClient (using Microsoft SDK). Client is reusable, after instantiation is being strored in private property.
    /// Next instantiation will return already created client. 
    /// Used this pattern because Microsoft EventHubProducerClient is more efficient when being instantiated for longer lifetime scope
    /// (ref: https://docs.microsoft.com/en-us/dotnet/api/azure.messaging.eventhubs.producer.eventhubproducerclient?view=azure-dotnet#remarks)
    /// </summary>
    public class AzureEventHubProducerClientFactory : IAzureEventHubProducerClientFactory
    {
        private ConcurrentDictionary<string, EventHubProducerClient> _clients;

        public AzureEventHubProducerClientFactory()
        {
            _clients = new ConcurrentDictionary<string, EventHubProducerClient>();
        }

        public Result<EventHubProducerClient> Get(string connectionString, string eventHubName)
        {
            var key = connectionString + eventHubName;
            var client = _clients.GetOrAdd(key, key => new EventHubProducerClient(connectionString, eventHubName));
            return new SuccessResult<EventHubProducerClient>(client);
        }
    }
}