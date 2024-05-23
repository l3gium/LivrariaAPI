using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Repository
{
    public class VendaRepository : IVenda
    {
        private readonly AppDbContext _context;

        public VendaRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(VendaModel venda)
        {
            _context.Add(venda);
            return Save();
        }

        public bool Delete(VendaModel venda)
        {
            var vendaDeletada = _context.Vendas.Where(v => v.Id == venda.Id).FirstOrDefault();
            _context.Remove(venda);

            return Save();
        }

        public async Task<List<VendaModel>> GetAllVendasAsync()
        {
            return await _context.Vendas.ToListAsync();
        }

        public async Task<VendaModel> GetVendaByIdAsNoTracking(int id)
        {
            return await _context.Vendas.Where(v => v.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<VendaModel> GetVendaByIdAsync(int id)
        {
            return await _context.Vendas.Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(VendaModel venda)
        {
            throw new NotImplementedException();
        }
    }
}
