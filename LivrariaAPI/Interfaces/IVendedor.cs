using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface IVendedor
    {
        Task<List<VendedorModel>> GetAllVendedoresAsync();
        Task<VendedorModel> GetVendedorByIdAsync(int id);
        Task<VendedorModel> GetVendedorByIdAsNoTracking(int id);
        bool Add(VendedorModel vendedor);
        bool Update(VendedorModel vendedor);
        bool Delete(VendedorModel vendedor);
        bool Save();
    }
}
