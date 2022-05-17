namespace G.EndpointForFlexibleBroker.App.Infrastructure
{
    public interface IBrokerClient
    {
        Task<Result> SendAsync();
    }
}