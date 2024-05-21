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
        
        public async Task<ActionResult<List<ClienteModel>>> BuscarClientesAsync() 
        {
            var clientes = await _clienteRepository.GetAllClientesAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(clientes);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> BuscarClientePorIdAsync(int Id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(Id);
            
            if(cliente == null)
                return BadRequest("Cliente não encontrado");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cliente); 
        }

        [HttpPost]
        public IActionResult CriarCliente(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                var clienteNovo = new ClienteModel()
                { 
                    Nome = cliente.Nome,
                    Endereco = cliente.Endereco,
                    Numero = cliente.Numero,
                    Bairro = cliente.Bairro,
                    Municipio = cliente.Municipio,
                    Cep = cliente.Cep,
                    UF = cliente.UF,
                    DataNasc = cliente.DataNasc,
                };
                _clienteRepository.Add(clienteNovo);
                return Ok("Cliente criado com sucesso!");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCliente(ClienteModel cliente, int id)
        {
            var clienteEditado = await _clienteRepository.GetClienteByIdAsNoTracking(id);
            if(clienteEditado == null)
                return BadRequest("Cliente não encontrado");


            clienteEditado = new ClienteModel()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Endereco = cliente.Endereco,
                Numero = cliente.Numero,
                Bairro = cliente.Bairro,
                Municipio = cliente.Municipio,
                Cep = cliente.Cep,
                UF = cliente.UF,
                DataNasc = cliente.DataNasc
            };

            _clienteRepository.Update(clienteEditado);
            return Ok(clienteEditado);
        }
    }
}
