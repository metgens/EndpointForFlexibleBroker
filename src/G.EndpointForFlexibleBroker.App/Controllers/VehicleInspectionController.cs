using G.EndpointForFelxibleBroker.Shared.DTOs;
using G.EndpointForFlexibleBroker.App.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace G.EndpointForFlexibleBroker.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInspectionController : ControllerBase
    {
        private readonly IBrokerClientFactory _brokerClientFactory;
        private readonly ILogger<VehicleInspectionController> _logger;

        public VehicleInspectionController(IBrokerClientFactory brokerClientFactory, ILogger<VehicleInspectionController> logger)
        {
            _brokerClientFactory = brokerClientFactory;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveVehicleInspection(VehicleInspectionDto vehicleInspection)
        {
            var resultBrokerFactory = _brokerClientFactory.Get(vehicleInspection.BrokerName);
            if (resultBrokerFactory.Failure)
            {
                
            }

            var brokerClient = resultBrokerFactory.Data;
            var resultSend = await brokerClient.SendAsync();
            if (resultSend.Failure)
            {

            }

            return Accepted();
        }
    }
}
