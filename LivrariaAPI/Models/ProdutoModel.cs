using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Models
{
    public class ProdutoModel
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public string Autor  { get; set; }
        public string Assunto { get; set; }
        public decimal Valor { get; set; }
        public decimal Custo { get; set; }
    }
}
