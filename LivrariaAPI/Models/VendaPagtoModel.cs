using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaAPI.Models
{
    public class VendaPagtoModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Venda")]
        public int VendaId { get; set; }
        public int TipoPag { get; set; }
        public decimal ValorPago { get; set; }
    }
}
