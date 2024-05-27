using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface IVendaProduto
    {
        Task<List<VendaProdutoModel>> GetAllVendasProdutoAsync();
        Task<VendaProdutoModel> GetVendaProdutoByIdAsync(int id);
        Task<VendaProdutoModel> GetVendaProdutoByIdAsNoTracking(int id);
        Task<VendaProdutoModel> GetVendaProdutoByVendaId(int vendaId);
        Task<VendaProdutoModel> GetVendaProdutoByVendaId(int vendaId, int produtoId);
        Task<List<VendaProdutoModel>> GetVendaProdutosByVendaIdAsync(int vendaId);
        bool Add(VendaProdutoModel vendaProduto);
        bool Delete(VendaProdutoModel vendaProduto);
        bool Save();
    }
}
