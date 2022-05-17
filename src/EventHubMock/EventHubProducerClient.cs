namespace EventHubMock
{
    public class EventHubProducerClient
    {
        private string connectionString;
        private string eventHubName;

        public EventHubProducerClient(string connectionString, string eventHubName)
        {
            this.connectionString = connectionString;
            this.eventHubName = eventHubName;
        }

        public Task SendAsync(IEnumerable<EventData> eventBatch, CancellationToken cancellationToken = default)
        {
            foreach (var eventData in eventBatch)
                Console.WriteLine($">> {eventHubName}: Sent event. Payload length: {eventData.Array.Length}. Properties count: {eventData.Properties.Count}");
           
            return Task.CompletedTask;
        }
    }
}