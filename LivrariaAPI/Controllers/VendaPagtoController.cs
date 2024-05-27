using LivrariaAPI.DTO;
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
        public async Task<IActionResult> CreateVendaPagto([FromBody] VendaPagtoRequestDto vendaPagtoDto, int vendaId, int tipoPag)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro na ModelState. Erro" + ModelState);

                var venda = await _vendaRepository.GetVendaByIdAsync(vendaId);
                if (venda == null)
                    return NotFound("Venda não encontrada");
                if (venda.Id != vendaId)
                    return BadRequest("ID's de venda não coincidem");

                var tipoPagto = await _tipoPagRepository.GetTipoPagamentoByIdAsNoTrackingAsync(tipoPag);
                if (tipoPagto == null)
                    return NotFound("Tipo de pagamento não encontrada");
                if (tipoPagto.Id != tipoPag)
                    return BadRequest("ID's de tipo de pagamento não coincidem");

                vendaPagtoDto.VendaId = vendaId;
                vendaPagtoDto.TipoPag = tipoPag;


                decimal valorTotal = 0;

                var vendaProdutos = await _vendaProdutoRepository.GetVendaProdutosByVendaIdAsync(vendaPagtoDto.VendaId);
                if (vendaProdutos == null || !vendaProdutos.Any())
                    return NotFound("Nenhum produto encontrado para venda");

                vendaPagtoDto.VendaProdutos = vendaProdutos;

                foreach (var produto in vendaPagtoDto.VendaProdutos)
                {
                    var vendaProduto = vendaProdutos.FirstOrDefault(vp => vp.ProdutoId == produto.ProdutoId);
                    if (vendaProduto == null)
                        return NotFound($"Produto com ID {produto.Id} não encontrado na venda {venda.Id}");

                    valorTotal += produto.Qtde * produto.Valor;
                }

                var vendaPagto = new VendaPagtoModel()
                {
                    VendaId = venda.Id,
                    TipoPag = tipoPagto.Id,
                    ValorPago = valorTotal
                };

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
