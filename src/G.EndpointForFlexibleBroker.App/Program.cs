using Autofac;
using Autofac.Extensions.DependencyInjection;
using G.EndpointForFlexibleBroker.App.Infrastructure;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients.AzureEventHub;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<BrokerClientFactory>().As<IBrokerClientFactory>();
    builder.RegisterType<AzureEventHubProducerClientFactory>().As<IAzureEventHubProducerClientFactory>().SingleInstance();
    builder.RegisterType<AzureEventHubClient>().Keyed<IBrokerClient>(BrokerClientType.AzureEventHub);
    builder.RegisterType<BrokerJsonMessageSerializer>().As<IBrokerMessageSerializer>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }