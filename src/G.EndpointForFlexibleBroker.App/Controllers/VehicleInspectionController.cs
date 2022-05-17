using G.EndpointForFelxibleBroker.Shared.DTOs;
using G.EndpointForFlexibleBroker.App.Infrastructure;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerClients;
using G.EndpointForFlexibleBroker.App.Infrastructure.BrokerPayloadSerializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace G.EndpointForFlexibleBroker.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] 
    public class VehicleInspectionController : ControllerBase
    {
        private readonly IBrokerClientFactory _brokerClientFactory;
        private readonly IBrokerMessageSerializer _brokerMessageSerializer;
        private readonly ILogger<VehicleInspectionController> _logger;

        public VehicleInspectionController(IBrokerClientFactory brokerClientFactory, IBrokerMessageSerializer brokerMessageSerializer, ILogger<VehicleInspectionController> logger)
        {
            _brokerClientFactory = brokerClientFactory;
            _brokerMessageSerializer = brokerMessageSerializer;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReceiveVehicleInspection(VehicleInspectionDto vehicleInspection)
        {
            var resultBrokerFactory = _brokerClientFactory.Get(vehicleInspection.BrokerName);
            if (resultBrokerFactory.Failure)
            {
                return resultBrokerFactory switch
                {
                    NotFoundResult<IBrokerClient> errResult => StatusCode((int)StatusCodes.Status405MethodNotAllowed, errResult.Message),
                    ErrorResult<IBrokerClient> errResult => StatusCode((int)StatusCodes.Status500InternalServerError, errResult.Message),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError)
                };
            }

            var brokerClient = resultBrokerFactory.Data;

            var message = _brokerMessageSerializer.Serialize(vehicleInspection);
            
            var resultSend = await brokerClient.SendAsync(message.payload, message.properties);
            if (resultSend.Failure)
            {
                return resultBrokerFactory switch
                {
                    ErrorResult<IBrokerClient> errResult => StatusCode((int)StatusCodes.Status500InternalServerError, errResult.Message),
                    _ => StatusCode((int)HttpStatusCode.InternalServerError)
                };
            }

            return Accepted();
        }
    }
}
