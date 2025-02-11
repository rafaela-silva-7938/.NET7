using ApiPedidos.Application.Commands;
using ApiPedidos.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Services.Controllers
{

    /// <summary>
    /// O ControllerBase é a classe base que fornece métodos para responder a requisições 
    /// HTTP no ASP.NET Core. Ela inclui vários métodos auxiliares para retornar códigos de 
    /// status HTTP, como Ok(), Created(), NotFound(), entre outros, além de 
    /// StatusCode(int statusCode, object value).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {

        private readonly IPedidoAppService? _pedidoAppService;
        public PedidosController(IPedidoAppService? pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }


        [HttpPost]
        public async Task<IActionResult> Post(PedidoCreateCommand command)
        {
            try
            {
                await _pedidoAppService.Add(command);

                // Toda API no ASP.NET Core pode retornar um status code usando o método StatusCode()
                return StatusCode(201, new
                {
                    status = "success",
                    message = "Pedido realizado com sucesso."
                });
            }
            catch (Exception ex)
            {
                var erro = ex;
                throw ex;
            }
  
        }
    }
}
