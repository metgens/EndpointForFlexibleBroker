namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers
{
    public interface IBrokerMessageSerializer
    {
        (byte[] payload, Dictionary<string, object>? properties) Serialize(object obj);
    }
}