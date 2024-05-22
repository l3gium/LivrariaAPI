using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using LivrariaAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPagamentoController : Controller
    {
        private readonly ITipoPagamento _tipoPagamentoRepository;

        public TipoPagamentoController(ITipoPagamento tipoPagamentoRepository)
        {
            _tipoPagamentoRepository = tipoPagamentoRepository;
        }     
        
        [HttpPost]
        public async Task<IActionResult> CreateTipoPagamento(TipoPagamentoModel tipoPagamento)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest("Erro na ModelState. Erro: " + ModelState);

                _tipoPagamentoRepository.Add(tipoPagamento);
                return Ok("Tipo de pagamento criado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao procurar pelo tipo de pagamento. Erro: " + err);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoPagamentoModel>>> GetTiposPagamentoAsync()
        {
            try
            {
                var tiposPagamento = await _tipoPagamentoRepository.GetAllTiposPagamentoAsync();

                return Ok(tiposPagamento);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar os produtos. Erro: " + err);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTipoPagamento(int id, [FromBody] TipoPagamentoModel tipoPagamento)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest("Erro na ModelState. Erro: " + ModelState);

                var tipoPagamentoExistente = await _tipoPagamentoRepository.GetTipoPagamentoByIdAsNoTrackingAsync(id);

                if (tipoPagamentoExistente == null)
                    return NotFound("Tipo de pagamento não encontrado");

                tipoPagamentoExistente.Descricao = tipoPagamento.Descricao;
                tipoPagamentoExistente.TipoPagto = tipoPagamento.TipoPagto;

                _tipoPagamentoRepository.Update(tipoPagamentoExistente);

                return Ok("Tipo de pagamento atualizado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao atualizar os tipos de pagamento. Erro: " + err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPagamento(int id)
        {
            try
            {
                var tipoPagamento = await _tipoPagamentoRepository.GetTipoPagamentoByIdAsNoTrackingAsync(id);

                if (tipoPagamento == null)
                    return NotFound("Tipo de pagamento não encontrado");

                _tipoPagamentoRepository.Delete(tipoPagamento);
                _tipoPagamentoRepository.Save();

                return Ok("Tipo de pagamento deletado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao atualizar os tipos de pagamento. Erro: " + err);
            }
        }
    }
}
