using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class vendaProdutoController : Controller
    {
        private readonly IVendaProduto _vendaProdutoRepository;
        private readonly IVenda _vendaRepository;
        private readonly IProduto _produtoRepository;

        public vendaProdutoController(IVendaProduto vendaProdutoRepository, 
                                      IVenda vendaRepository, 
                                      IProduto produtoRepository)
        {
            _vendaProdutoRepository = vendaProdutoRepository;
            _vendaRepository = vendaRepository;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<VendaProdutoModel>>> GetVendasProduto()
        {
            try
            {
                var vendasProdutos = await _vendaProdutoRepository.GetAllVendasProdutoAsync();
                if (vendasProdutos == null || !vendasProdutos.Any())
                    return NotFound("Não foram encontradas vendas de nenhum produto");

                return Ok(vendasProdutos);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar as vendas do produto. Erro: " + err);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendaProdutoByIdAsync(int id)
        {
            try
            {
                var vendaProduto = await _vendaProdutoRepository.GetVendaProdutoByIdAsync(id);

                if (vendaProduto == null)
                    return NotFound("Nenhuma venda do produto encontrada");

                return Ok(vendaProduto);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar os produtos. Erro: " + err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendaProduto(VendaProdutoModel vendaProduto, int vendaId, int produtoId, short quantidade)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Erro na ModelState. Erro " + ModelState);
                var venda = await _vendaRepository.GetVendaByIdAsync(vendaId);
                if (venda == null)
                    return NotFound("Código da venda inválido");

                var produto = await _produtoRepository.GetProdutosByIdAsync(produtoId);
                if (produto == null)
                    return NotFound("Código do produto inválido");

                vendaProduto.VendaId = venda.Id;
                vendaProduto.ProdutoId = produto.Id;
                vendaProduto.Qtde = quantidade;
                vendaProduto.Valor = produto.Valor;
                vendaProduto.Custo = produto.Custo;

                _vendaProdutoRepository.Add(vendaProduto);
                return Ok(vendaProduto);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar os produtos. Erro: " + err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendaProduto(int id)
        {
            try
            {
                var vendaProduto = await _vendaProdutoRepository.GetVendaProdutoByIdAsync(id);
                if (vendaProduto == null) 
                    return NotFound("Venda do produto não encontrada");

                _vendaProdutoRepository.Delete(vendaProduto);
                _vendaProdutoRepository.Save();

                return Ok("Venda do produto deletada");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar os produtos. Erro: " + err);
            }
        }
    }
}
