using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface ITipoPagamento
    {
        Task<List<TipoPagamentoModel>> GetAllTiposPagamentoAsync();
        Task<TipoPagamentoModel> GetTipoPagamentoByIdAsNoTrackingAsync(int id);
        bool Add(TipoPagamentoModel tipoPagamento);
        bool Update(TipoPagamentoModel tipoPagamento);
        bool Delete(TipoPagamentoModel tipoPagamento);
        bool Save();
    }
}
