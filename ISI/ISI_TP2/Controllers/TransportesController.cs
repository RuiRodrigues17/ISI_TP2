using Microsoft.AspNetCore.Mvc;
using ServiceReference1;

namespace ISI_TP2.Controllers
{
    public class TransportesController:ControllerBase
    {
        private readonly ServiceClient soapClient;
        public TransportesController()
        {
            soapClient = new ServiceClient(ServiceClient.EndpointConfiguration.BasicHttpBinding_IService);
        }

        public async Task<IActionResult> GetTransportedByID(int id)
        {
            try
            {
                Transporte transporte = await soapClient.GetTransporteByIdAsync(id);

                return Ok(transporte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
