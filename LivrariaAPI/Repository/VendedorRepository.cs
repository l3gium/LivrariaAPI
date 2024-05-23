using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Repository
{
    public class VendedorRepository : IVendedor
    {
        private readonly AppDbContext _context;

        public VendedorRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(VendedorModel vendedor)
        {
            _context.Add(vendedor);
            return Save();
        }

        public bool Delete(VendedorModel vendedor)
        {
            var vendedorDeletado = _context.Vendedores.Where(v => v.Id == vendedor.Id).FirstOrDefault();
            _context.Remove(vendedorDeletado);

            return Save();
        }

        public async Task<List<VendedorModel>> GetAllVendedoresAsync()
        {
            return await _context.Vendedores.ToListAsync();
        }

        public async Task<VendedorModel> GetVendedorByIdAsNoTracking(int id)
        {
            return await _context.Vendedores.Where(v => v.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<VendedorModel> GetVendedorByIdAsync(int id)
        {
            return await _context.Vendedores.Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(VendedorModel vendedor)
        {
            _context.Update(vendedor);
            return Save();
        }
    }
}
