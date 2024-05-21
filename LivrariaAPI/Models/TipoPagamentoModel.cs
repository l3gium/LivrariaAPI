using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Models
{
    public class TipoPagamentoModel
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string TipoPagto { get; set; }
    }
}
