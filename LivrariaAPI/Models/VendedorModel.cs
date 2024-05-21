using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Models
{
    public class VendedorModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Comissao { get; set; }
    }
}
