using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaPagtoController : Controller
    {
        private readonly IVendaPagto _vendaPagtoRepository;
        private readonly IVenda _vendaRepository;
        private readonly ITipoPagamento _tipoPagRepository;
        private readonly IVendaProduto _vendaProdutoRepository;

        public VendaPagtoController(IVendaPagto vendaPagtoRepository,
                                    IVenda vendaRepository, 
                                    ITipoPagamento tipoPagRepository,
                                    IVendaProduto vendaProdutoRepository)
        {
            _vendaPagtoRepository = vendaPagtoRepository;
            _vendaRepository = vendaRepository;
            _tipoPagRepository = tipoPagRepository;
            _vendaProdutoRepository = vendaProdutoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<VendaPagtoModel>>> GetVendasPagtoAsync()
        {
            try
            {
                var vendasPagto = await _vendaPagtoRepository.GetAllVendasPagtoAsync()  ;
                if (vendasPagto == null || !vendasPagto.Any())
                    return NotFound("Não foi possível encontrar os pagamentos das vendas");

                return Ok(vendasPagto);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar as vendas. Erro: " + err);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendasPagtoById(int id)
        {
            try
            {
                var vendaPagto = await _vendaPagtoRepository.GetVendaPagtoByIdAsync(id);
                if (vendaPagto == null)
                    return NotFound("Pagamento de venda não encontrado");

                return Ok(vendaPagto);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar as vendas. Erro: " + err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendaPagto(VendaPagtoModel vendaPagto, int vendaId, int tipoPag)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro na ModelState. Erro" + ModelState);

                var venda = await _vendaRepository.GetVendaByIdAsync(vendaId);
                if (venda == null)
                    return NotFound("Venda não encontrada");

                var tipoPagto = await _tipoPagRepository.GetTipoPagamentoByIdAsNoTrackingAsync(tipoPag);
                if (tipoPagto == null)
                    return NotFound("Tipo de pagamento encontrada");

                var vendaProduto = await _vendaProdutoRepository.GetVendaProdutoByVendaId(vendaId);

                vendaPagto.VendaId = venda.Id;
                vendaPagto.TipoPag = tipoPagto.Id;
                vendaPagto.ValorPago = vendaProduto.Qtde * vendaProduto.Valor;

                _vendaPagtoRepository.Add(vendaPagto);

                return Ok(vendaPagto);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar as vendas. Erro: " + err);
            }
        }
    }
}
