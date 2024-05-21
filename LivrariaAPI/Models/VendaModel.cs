using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace LivrariaAPI.Models
{
    public class VendaModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
    }
}
