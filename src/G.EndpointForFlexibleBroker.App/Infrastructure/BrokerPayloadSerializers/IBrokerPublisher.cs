using G.EndpointForFlexibleBroker.Shared;
using G.EndpointForFlexibleBroker.Shared.Interfaces;

namespace G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers
{
    public interface IBrokerPublisher
    {
        Task<Result> SendAsync(IEntityWithBrokerDeclaration message);
    }
}