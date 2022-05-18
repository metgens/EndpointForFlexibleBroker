using G.EndpointForFlexibleBroker.Shared;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients
{
    public interface IBrokerClient
    {
        Result Configure(IConfigurationSection configSection);
        Task<Result> SendAsync(byte[] payload, Dictionary<string, object>? userProperties = null, CancellationToken cancellation = default);
    }
}