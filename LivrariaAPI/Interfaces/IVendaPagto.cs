using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface IVendaPagto
    {
        Task<List<VendaPagtoModel>> GetAllVendasPagtoAsync();
        Task<VendaPagtoModel> GetVendaPagtoByIdAsync(int id);
        bool Add(VendaPagtoModel vendaPagto);
        bool Delete(VendaPagtoModel vendaPagto);
        bool Save();
    }
}
