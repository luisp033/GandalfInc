using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Faturacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer
{
    public class ProjetoDBContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Database=ProjectoDB;Trusted_Connection=True;";
            options.UseSqlServer(connectionString);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<CategoriaProduto> CategoriaProdutos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Loja> Lojas{ get; set; }
        public DbSet<MarcaProduto> MarcaProdutos { get; set; }
        public DbSet<PontoDeVenda> PontoDeVendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<DetalheVenda> DetalheVendas { get; set; }
    }
}
