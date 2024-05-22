using LivrariaAPI.Data;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Repository
{
    public class TipoPagamentoRepository : ITipoPagamento
    {
        private readonly AppDbContext _context;

        public TipoPagamentoRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(TipoPagamentoModel tipoPagamento)
        {
            _context.Add(tipoPagamento);
            return Save();
        }

        public bool Delete(TipoPagamentoModel tipoPagamento)
        {
            var tipoPagamentoDeletado = _context.TipoPagamentos.Where(t => t.Id == tipoPagamento.Id).FirstOrDefault();
            _context.Remove(tipoPagamentoDeletado);

            return Save();
        } 

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(TipoPagamentoModel tipoPagamento)
        {
            _context.Update(tipoPagamento);
            return Save();
        }

        public async Task<List<TipoPagamentoModel>> GetAllTiposPagamentoAsync()
        {
            return await _context.TipoPagamentos.ToListAsync();
        }

        public async Task<TipoPagamentoModel> GetTipoPagamentoByIdAsNoTrackingAsync(int id)
        {
            return await _context.TipoPagamentos.Where(t => t.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
