using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
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
            var clientes = await _clienteRepository.GetAllClientesAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clientes);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetClienteByIdAsync(int Id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(Id);
            
            if(cliente == null)
                return BadRequest("Cliente não encontrado");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cliente); 
        }

        [HttpPost]
        public IActionResult CreateCliente(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.Add(cliente);
                return Ok("Cliente criado com sucesso!");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteModel cliente, int id)
        {
            var clienteExistente = await _clienteRepository.GetClienteByIdAsNoTracking(id);
            if (clienteExistente == null)
                return BadRequest("Cliente não encontrado");

            try
            {
                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Endereco = cliente.Endereco;
                clienteExistente.Numero = cliente.Numero;
                clienteExistente.Bairro = cliente.Bairro;
                clienteExistente.Municipio = cliente.Municipio;
                clienteExistente.Cep = cliente.Cep;
                clienteExistente.UF = cliente.UF;
                clienteExistente.DataNasc = cliente.DataNasc;

                _clienteRepository.Update(clienteExistente);
                return Ok(clienteExistente);
            }
            catch (Exception err) 
            {
                return StatusCode(500, "Houve um erro ao atualizar o cliente: " + err);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            _clienteRepository.Delete(cliente);
            _clienteRepository.Save();
            return Ok(cliente);
        }
    }
}
