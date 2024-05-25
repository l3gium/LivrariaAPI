using LivrariaAPI.Models;
using LivrariaAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set;}
        public DbSet<ProdutoModel> Produtos { get; set;}
        public DbSet<TipoPagamentoModel> TipoPagamentos { get;set;}
        public DbSet<VendaModel> Vendas { get; set;}
        public DbSet<VendaProdutoModel> VendaProdutos { get; set; }
        public DbSet<VendedorModel> Vendedores { get; set;}
        public DbSet<VendaPagtoModel> VendaPagto { get; set;}
    }
}
