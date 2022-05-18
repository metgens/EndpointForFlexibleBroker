using EventHubMock;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients.AzureEventHub
{
    public class AzureEventHubClient : IBrokerClient
    {
        private EventHubProducerClient? _producerClient;
        private readonly IAzureEventHubProducerClientFactory _azureEventHubProducerClientFactory;

        public AzureEventHubClient(IAzureEventHubProducerClientFactory azureEventHubProducerClientFactory)
        {
            _azureEventHubProducerClientFactory = azureEventHubProducerClientFactory;
        }

        public Result Configure(IConfigurationSection configSection)
        {
            var connectionString = configSection["ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
                return new ErrorResult($"Missing '{connectionString}' in '{configSection.Path}' in appsettings");

            var eventHubName = configSection["EventHubName"];
            if (string.IsNullOrEmpty(eventHubName))
                return new ErrorResult($"Missing '{eventHubName}' in '{configSection.Path}' in appsettings");

            var result = _azureEventHubProducerClientFactory.Get(connectionString, eventHubName);
            if (result.Failure)
                return result;

            _producerClient = result.Data;

            return new SuccessResult();
        }

        public async Task<Result> SendAsync(byte[] payload, Dictionary<string, object>? userProperties = null, CancellationToken cancellationToken = default)
        {
            if (_producerClient == null)
                return new ErrorResult($"'{nameof(AzureEventHubClient)}' has not beed configured.");

            var eventsBatch = PrepareEventBatch(payload, userProperties);

            try
            {
                await _producerClient.SendAsync(eventsBatch, cancellationToken);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex);
            }

            return new SuccessResult();
        }

        private static List<EventData> PrepareEventBatch(byte[] payload, Dictionary<string, object>? userProperties)
        {
            var events = new List<EventData>();
            var eventData = new EventData(payload);

            if (userProperties != null)
                foreach (var userProperty in userProperties)
                {
                    eventData.Properties.Add(userProperty.Key, userProperty.Value);
                }

            events.Add(eventData);
            return events;
        }
    }
}
