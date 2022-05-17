﻿namespace G.EndpointForFlexibleBroker.App.Infrastructure
{
    public interface IBrokerClientFactory
    {
        Result<IBrokerClient> Get(string brokerName);
    }
}
