using LivrariaAPI.Models;

namespace LivrariaAPI.DTO
{
    public class VendaPagtoRequestDto
    {
        public int VendaId { get; set; }
        public int TipoPag { get; set; }    
        public List<VendaProdutoModel> VendaProdutos { get; set; }
    }
}
