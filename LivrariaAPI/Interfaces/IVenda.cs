using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface IVenda
    {
        Task<List<VendaModel>> GetAllVendasAsync();
        Task<VendaModel> GetVendaByIdAsync(int id);
        Task<VendaModel> GetVendaByIdAsNoTracking(int id);
        bool Add(VendaModel venda);
        bool Update(VendaModel venda);
        bool Delete(VendaModel venda);
        bool Save();
    }
}
