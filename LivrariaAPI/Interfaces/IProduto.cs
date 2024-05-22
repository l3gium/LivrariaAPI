using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface IProduto
    {
        Task<List<ProdutoModel>> GetAllProdutosAsync();
        Task<ProdutoModel> GetProdutosByIdAsync(int id);
        Task<ProdutoModel> GetProdutoByIdAsNoTracking(int id);
        bool Add(ProdutoModel produto);
        bool Update(ProdutoModel produto);  
        bool Delete(ProdutoModel produto);
        bool Save();
    }
}
