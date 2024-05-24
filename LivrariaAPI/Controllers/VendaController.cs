using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using LivrariaAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : Controller
    {
        private readonly IVenda _vendaRepository;
        private readonly ICliente _clienteRepository;
        private readonly IVendedor _vendedorRepository;

        public VendaController(IVenda vendaRepository, ICliente clienteRepository, IVendedor vendedorRepository)
        {
            _vendaRepository = vendaRepository;
            _clienteRepository = clienteRepository;
            _vendedorRepository = vendedorRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<VendaModel>>> GetVendasAsync()
        {
            try
            {
                var vendas = await _vendaRepository.GetAllVendasAsync();
                if (vendas == null || !vendas.Any())
                    return NotFound("Nenhuma venda encontrada");

                return(vendas);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar as vendas. Erro: " + err);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendasByIdAsync(int id)
        {
            try
            {
                var venda = await _vendaRepository.GetVendaByIdAsync(id);
                if (venda == null)
                    return NotFound("Venda não encontrada");

                return Ok(venda);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar a venda. Erro: " + err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenda(VendaModel venda, int idCliente, int idVendedor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro na ModelState. Erro: " + ModelState);

                var cliente = await _clienteRepository.GetClienteByIdAsync(idCliente);
                if (cliente == null)
                    return NotFound("Cliente não encontrado");

                var vendedor = await _vendedorRepository.GetVendedorByIdAsync(idVendedor);
                if (vendedor == null)
                    return NotFound("Vendedor não encontrado");

                venda.ClienteId = cliente.Id;
                venda.VendedorId = vendedor.Id;

                _vendaRepository.Add(venda);

                return Ok(venda);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao criar venda. Erro: " + err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            try
            {
                var venda = await _vendaRepository.GetVendaByIdAsync(id);
                if (venda == null)
                    return NotFound("Venda não encontrada");

                _vendaRepository.Delete(venda);
                _vendaRepository.Save();

                return Ok("Venda exluida com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao criar venda. Erro: " + err);
            }
        }
    }
}
