namespace EventHubMock
{
    public class EventData
    {
        public IDictionary<string, object> Properties { get; }
        public byte[] Array { get; }

        public EventData(byte[] array)
        {
            Properties = new Dictionary<string, object>();
            Array = array;
        }
    }
}