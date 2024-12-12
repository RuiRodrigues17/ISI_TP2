using Microsoft.AspNetCore.Mvc;
using ServiceReference1;

namespace ISI_TP2.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class TransportesController : ControllerBase
    {
        private readonly ServiceClient soapClient;
        public TransportesController()
        {
            soapClient = new ServiceClient(ServiceClient.EndpointConfiguration.BasicHttpBinding_IService);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllTransportes()
        {
            try
            {
                var transportes = await soapClient.GetAllTransportesAsync();
                return Ok(transportes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
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

        [HttpPost("{tipo},{matricula},{arcondicionado},{temperaturaAtual},{idUtilizador}")]
        public async Task<IActionResult> InsertTransport(string tipo,string matricula, bool arcondicionado, decimal temperaturaAtual, int idUtilizador)
        {
            try
            {
                Transporte transporte = new Transporte();
                transporte.Tipo = tipo;
                transporte.Matricula = matricula;
                transporte.ArCondicionado = arcondicionado;
                transporte.TemperaturaAtual = temperaturaAtual;
                transporte.IdUtilizador = idUtilizador;

                int rowsAffected = await soapClient.InsertTransporteAsync(transporte);

                if (rowsAffected > 0)
                {
                    return Ok("Transporte inserted with sucess.");
                }
                else
                {
                    return BadRequest("Error inserting transporte.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }

        [HttpPut("{id},{temperaturaAtual}")]
        public async Task<IActionResult> UpdateTransport(int id, decimal temperaturaAtual) {
            try
            {
                int rowsAffected = await soapClient.UpdateTransporteAsync(id, temperaturaAtual);
                if (rowsAffected > 0)
                {
                    return Ok($"Transporte updated successfully.Rows affected:{rowsAffected}");
                }
                else {
                    return NotFound($"Transporte with {id} not found");
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500,$"Internal server error:{ ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransport(int id)
        {
            try
            {
                int rowsAffected = await soapClient.DeleteTransporteAsync(id);
                if (rowsAffected > 0)
                {
                    return Ok($"Transporte deleted successfully. Rows affected: {rowsAffected}");
                }
                else
                {
                    return NotFound($"Device with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }
    }
}
