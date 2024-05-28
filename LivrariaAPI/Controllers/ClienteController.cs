using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using LivrariaAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ICliente _clienteRepository;

        public ClienteController(ICliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        [HttpGet]
        
        public async Task<ActionResult<List<ClienteModel>>> GetClientesAsync() 
        {
            try
            {
                var clientes = await _clienteRepository.GetAllClientesAsync();

                if (clientes == null)
                    return NotFound("Nenhum cliente encontrado.");

                return Ok(clientes);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar os clientes. Erro: " + err);
            }

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetClienteByIdAsync(int Id)
        {
            try
            {
                var cliente = await _clienteRepository.GetClienteByIdAsync(Id);

                if (cliente == null)
                    return NotFound("Cliente não encontrado");

                return Ok(cliente);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao buscar o cliente. Erro: " + err);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente(ClienteModel cliente, 
                                                       string nome, string endereco, 
                                                       short numero, string bairro,
                                                       string municipio, int cep, string uf)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                cliente.Nome = nome;
                cliente.Endereco = endereco;
                cliente.Numero = numero;
                cliente.Bairro = bairro;
                cliente.Municipio = municipio;
                cliente.Cep = cep;
                cliente.UF = uf;

                _clienteRepository.Add(cliente);
                return Ok("Cliente criado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao criar cliente. Erro: " + err);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, 
                                                       string nome, string endereco,
                                                       short numero, string bairro,
                                                       string municipio, int cep, string uf)
        {
            

            try
            {
                var clienteExistente = await _clienteRepository.GetClienteByIdAsNoTracking(id);
                if (clienteExistente == null)
                    return NotFound("Cliente não encontrado");

                clienteExistente.Nome = nome;
                clienteExistente.Endereco = endereco;
                clienteExistente.Numero = numero;
                clienteExistente.Bairro = bairro;
                clienteExistente.Municipio = municipio;
                clienteExistente.Cep = cep;
                clienteExistente.UF = uf;

                _clienteRepository.Update(clienteExistente);
                return Ok("Cliente atualizado com sucesso");
            }
            catch (Exception err) 
            {
                return StatusCode(500, "Houve um erro ao atualizar o cliente: " + err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetClienteByIdAsync(id);

                if (cliente == null)
                    return NotFound("Cliente não encontrado");

                _clienteRepository.Delete(cliente);
                _clienteRepository.Save();
                return Ok("Cliente deletado com sucesso");
            }
            catch (Exception err)
            {
                return StatusCode(500, "Houve um erro ao excluir o cliente: " + err);
            }
        }
    }
}
