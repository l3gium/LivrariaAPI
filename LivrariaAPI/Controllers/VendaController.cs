using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : Controller
    {
        private readonly IVenda _vendaRepository;
        private readonly ICliente _clienteRepository;

        public VendaController(IVenda vendaRepository, ICliente clienteRepository)
        {
            _vendaRepository = vendaRepository;
            _clienteRepository = clienteRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<VendaModel>>> GetVendasAsync()
        {
            try
            {
                var vendas = await _vendaRepository.GetAllVendasAsync();
                if (vendas == null)
                    return NotFound("Nenhuma venda encontrada");

                return(vendas);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar as vendas. Erro: " + err);
            }
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetVendasByIdAsync(int id)
        //{
            //continuar depois
        //}
    }
}
