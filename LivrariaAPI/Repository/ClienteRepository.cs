using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Repository
{
    public class ClienteRepository : ICliente
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(ClienteModel cliente)
        {
            _context.Add(cliente);
            return Save();
        }

        public bool Delete(ClienteModel cliente)
        {
            var clienteDeletado = _context.Clientes.Where(c => c.Id == cliente.Id).FirstOrDefault();
            _context.Clientes.Remove(cliente);

            return Save();
        }

        public async Task<List<ClienteModel>> GetAllClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<ClienteModel> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ClienteModel cliente)
        {
            _context.Update(cliente);
            return Save();
        }
        public async Task<ClienteModel> GetClienteByIdAsNoTracking(int id)
        {
            return await _context.Clientes.Where(c => c.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
