using Autofac;
using G.EndpointForFelxibleBroker.Shared.DTOs;
using G.EndpointForFlexibleBroker.App.Infrastructure;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients.AzureEventHub;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers;
using System.Reflection;

namespace G.EndpointForFlexibleBroker.App
{
    public class StartupSwagger
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                //shared
                xmlFilename = $"{Assembly.GetAssembly(typeof(VehicleInspectionDto)).GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });
        }
    }
}
