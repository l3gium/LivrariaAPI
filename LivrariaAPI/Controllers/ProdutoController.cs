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
        public async Task<IActionResult> CreateProduto(ProdutoModel produto, string titulo, string isbn,
                                                       string autor, string assunto, decimal valor, decimal custo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                produto.Titulo = titulo;
                produto.ISBN = isbn;
                produto.Autor = autor;
                produto.Assunto = assunto;
                produto.Valor = valor;
                produto.Custo = custo;

                _produtoRepository.Add(produto);
                return Ok("Produto criado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao procurar pelo produto. Erro: " + err);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(int id, string titulo, string isbn,
                                                       string autor, string assunto, decimal valor, decimal custo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var produtoExistente = await _produtoRepository.GetProdutoByIdAsNoTracking(id);

                if (produtoExistente == null)
                    return NotFound("Produto não encontrado");

                produtoExistente.Titulo = titulo;
                produtoExistente.ISBN = isbn;
                produtoExistente.Autor = autor;
                produtoExistente.Assunto = assunto;
                produtoExistente.Valor = valor;
                produtoExistente.Custo = custo;

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
