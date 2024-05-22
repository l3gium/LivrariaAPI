using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IProduto _produtoRepository;

        public ProdutoController(IProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> GetProdutosAsync()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutosAsync();
                if (produtos == null || !produtos.Any())
                    return NotFound("Nenhum produto encontrado");
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(produtos);
            }
            catch (Exception err) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar os produtos. Erro: " + err);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProdutoByIdAsync(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutosByIdAsync(id);

                if (produto == null)
                    return NotFound("Produto não encontrado");

                return Ok(produto);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao procurar pelo produto. Erro: " + err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduto(ProdutoModel produto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Não foi possível criar o produto. " + ModelState);

                _produtoRepository.Add(produto);
                return Ok("Cliente criado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao procurar pelo produto. Erro: " + err);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, [FromBody] ProdutoModel produto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var produtoExistente = await _produtoRepository.GetProdutoByIdAsNoTracking(id);

                if (produtoExistente == null)
                    return NotFound("Produto não encontrado");

                produtoExistente.Titulo = produto.Titulo;
                produtoExistente.ISBN = produto.ISBN;
                produtoExistente.Autor = produto.Autor;
                produtoExistente.Assunto = produto.Assunto;
                produtoExistente.Valor = produto.Valor;
                produtoExistente.Custo = produto.Custo;

                _produtoRepository.Update(produtoExistente);

                return Ok("Produto atualizado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao atualizar o produto. Por favor, tente novamente mais tarde. Erro: " + err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutosByIdAsync(id);

                if (produto == null)
                    return NotFound("Produto não encontrado");

                _produtoRepository.Delete(produto);
                _produtoRepository.Save();

                return Ok("Produto deletado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao atualizar o produto. Por favor, tente novamente mais tarde. Erro: " + err);
            }
        }
    }
}
