using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using LivrariaAPI.Models;

namespace LivrariaAPI.Repository
{
    public class vendaProdutoRepository : IVendaProduto
    {
        private readonly AppDbContext _context;
        private readonly IVenda _vendaRepository;

        public vendaProdutoRepository(AppDbContext context,
                                      IVenda vendaRepository)
        {
            _context = context;
            _vendaRepository = vendaRepository;
        }
        public bool Add(VendaProdutoModel vendaProduto)
        {
            _context.Add(vendaProduto);
            return Save();
        }

        public bool Delete(VendaProdutoModel vendaProduto)
        {
            var vendaProdutoDeletada = _context.VendaProdutos.Where(v => v.Id == vendaProduto.Id).FirstOrDefault();
            _context.Remove(vendaProdutoDeletada);
            return Save();
        }

        public async Task<List<VendaProdutoModel>> GetAllVendasProdutoAsync()
        {
            return await _context.VendaProdutos.ToListAsync();
        }

        public async Task<VendaProdutoModel> GetVendaProdutoByIdAsNoTracking(int id)
        {
            return await _context.VendaProdutos.Where(v => v.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<VendaProdutoModel> GetVendaProdutoByIdAsync(int id)
        {
            return await _context.VendaProdutos.Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<VendaProdutoModel> GetVendaProdutoByVendaId(int vendaId)
        {
            var venda = await _vendaRepository.GetVendaByIdAsync(vendaId);
            return await _context.VendaProdutos.Where(v => v.VendaId == venda.Id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
