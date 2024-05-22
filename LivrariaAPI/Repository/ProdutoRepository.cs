using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Repository
{
    public class ProdutoRepository : IProduto
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(ProdutoModel produto)
        {
            _context.Add(produto);
            return Save();
        }

        public bool Delete(ProdutoModel produto)
        {
            var produtoDeletado = _context.Produtos.Where(p => p.Id == produto.Id).FirstOrDefault();
            _context.Remove(produtoDeletado);
            return Save();
        }

        public async Task<List<ProdutoModel>> GetAllProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<ProdutoModel> GetProdutoByIdAsNoTracking(int id)
        {
            return await _context.Produtos.Where(c => c.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ProdutoModel> GetProdutosByIdAsync(int id)
        {
            return await _context.Produtos.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ProdutoModel produto)
        {
            _context.Update(produto);
            return Save();
        }
    }
}
