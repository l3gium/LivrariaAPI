using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Models
{
    public class ClienteModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public short Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public int Cep { get; set; }
        public string UF { get; set; }
        public DateTime DataNasc { get; set; }
    }
}
