using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface IVendaProduto
    {
        Task<List<VendaProdutoModel>> GetAllVendasProdutoAsync();
        Task<VendaProdutoModel> GetVendaProdutoByIdAsync(int id);
        Task<VendaProdutoModel> GetVendaProdutoByIdAsNoTracking(int id);
        bool Add(VendaProdutoModel vendaProduto);
        bool Delete(VendaProdutoModel vendaProduto);
        bool Save();
    }
}
