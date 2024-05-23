﻿using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedor _vendedorRepository;

        public VendedorController(IVendedor vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<VendedorModel>>> GetVendedoresAsync()
        {
            try
            {
                var vendedores = await _vendedorRepository.GetAllVendedoresAsync();
                if (vendedores == null)
                    return NotFound("Nâo foram encontrados vendedores");

                return Ok(vendedores);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar vendedores. Erro: " + err);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendedorById(int id)
        {
            try
            {
                var vendedor = await _vendedorRepository.GetVendedorByIdAsync(id);
                if (vendedor == null)
                    return NotFound("Vendedor não encontrado");

                return Ok(vendedor);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao buscar vendedor. Erro: " + err);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendedor(VendedorModel vendedor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _vendedorRepository.Add(vendedor);
                return Ok("Vendedor criado com sucesso!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar vendedor. Erro: " + err);
            }
        }
    }
}
