using LivrariaAPI.Models;
using LivrariaAPI.Repository;

namespace LivrariaAPI.Interfaces
{
    public interface ICliente
    {
        Task<List<ClienteModel>> GetAllClientesAsync();
        Task<ClienteModel> GetClienteByIdAsync(int id);
        Task<ClienteModel> GetClienteByIdAsNoTracking(int id);
        bool Add(ClienteModel cliente);
        bool Update(ClienteModel cliente);
        bool Delete(ClienteModel cliente);
        bool Save();            
    }
}
