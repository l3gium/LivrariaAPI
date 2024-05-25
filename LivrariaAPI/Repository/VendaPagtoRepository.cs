using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Repository
{
    public class VendaPagtoRepository : IVendaPagto
    {
        private readonly AppDbContext _context;

        public VendaPagtoRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(VendaPagtoModel vendaPagto)
        {
            _context.Add(vendaPagto);
            return Save();
        }

        public bool Delete(VendaPagtoModel vendaPagto)
        {
            var vendaPagtoDeletado =  _context.VendaPagto.Where(v => v.Id == vendaPagto.Id).FirstOrDefaultAsync();
            _context.Remove(vendaPagtoDeletado);
            return Save();
        }

        public async Task<List<VendaPagtoModel>> GetAllVendasPagtoAsync()
        {
            return await _context.VendaPagto.ToListAsync();
        }

        public async Task<VendaPagtoModel> GetVendaPagtoByIdAsync(int id)
        {
            return await _context.VendaPagto.Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); 
            return saved > 0 ? true : false;
        }
    }
}
