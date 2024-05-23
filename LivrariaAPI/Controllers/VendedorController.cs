using LivrariaAPI.Interfaces;
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

        //public async Task<ActionResult<List<VendedorModel>>> GetVendedoresAsync()
        //{
        //    try
        //    {
        //        var vendedores = await _vendedorRepository
        //    }
        //    catch (Exception err)
        //    {
        //        throw;
        //    }
        //}
    }
}
