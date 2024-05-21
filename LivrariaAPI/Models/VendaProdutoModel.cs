using System.ComponentModel.DataAnnotations.Schema;

namespace LivrariaAPI.Models
{
    public class VendaProdutoModel
    {
        public int Id { get; set; }
        [ForeignKey("Venda")]
        public int VendaId { get; set; }
        [ForeignKey("Produto")]
        public int ProdutoId { get; set; }
        public short Qtde { get; set; }
        public decimal Valor { get; set; }
        public decimal Custo { get; set; }

    }
}
