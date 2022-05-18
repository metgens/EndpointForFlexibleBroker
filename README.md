# What is Endpoint for Flexible Broker?

Application is an REST API service that act as a gateway to ingest a lot of data fast into specified brokers. The service handles the call and forwards it to a broker. The destination broker is specified as a field in the message.

# How to start the solution?

Type the following command:
```
cd src\G.EndpointForFlexibleBroker.App
dotnet run
```

# API Documentation

Application provides API documentation as a Swagger page. The page is available at: `[page url]/swagger/index.html`.

# Architecture decisions records - ADR

All architecture decisions are written down in [ADRs](ADR). 

# How to configure clients for brokers?

The application reads brokers configuration using built-in in ASP.NET `IConfiguration` mechanism. Config may be provided using `appsettings.json files` or as `environment variables`.

The config structure looks like below:

```
"Brokers": {
    "AzureEventHubVehicleInspections": {
      "Type": "AzureEventHub",
      "ConnectionString": "conection-string-here",
      "EventHubName" : "VehicleInspections"
    }
  }
```

where:
- **AzureEventHubVehicleInspections** - is the name of the broker. This name is specified as a field in the message. 
- **Type** - is type of the broker. Type has to be defined to `BrokerClientType` enum.
- other field such as **ConnectionString, EventHubName** - are specific to given type of the broker. Two enlisted are specific for **Azure EventHub** broker.

When we want to add a new broker config, we need to provide new config structure inside the `Brokers` section. And ensure that name of the broker will be provided in the message field and type of the broker is already defined in `BrokerClientType` enum.

After configuration changes, the application should be restarted. 

# How to develop and register client for new broker type?

If `BrokerClientType` enum does not contain desired type of the broker probably application has not implemented client for this type.
The new client should:
- implement the `IBrokerClient` interface,
- be added to `BrokerClientType` enum
- and registered in IoC container using keyed registration where enum value is the key. Registration takes place in `StartupIoC` class.