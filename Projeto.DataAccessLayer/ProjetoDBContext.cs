using Microsoft.EntityFrameworkCore;
using Projeto.DataAccessLayer.Entidades;
using System;

namespace Projeto.DataAccessLayer
{
    public class ProjetoDBContext: DbContext
    {

        private readonly DataBaseType Tipo;
        private readonly string CnnString;
        public ProjetoDBContext(DataBaseType tipo = DataBaseType.SqlServer)
        {
            Tipo = tipo;

            if (Tipo is DataBaseType.Sqlite)
            {
                CnnString = "Data Source=DBTeste.sqlite";
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            else
            {
                CnnString = @"Server=(LocalDB)\MSSQLLocalDB;Database=ProjectoDB;Trusted_Connection=True;MultipleActiveResultSets=true;";
                //Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TipoPagamento>()
                   .HasData(
                     new TipoPagamento { Id = 0, Name = "Indefenido" },
                     new TipoPagamento { Id = 1, Name = "Multibanco" },
                     new TipoPagamento { Id = 2, Name = "MbWay" },
                     new TipoPagamento { Id = 3, Name = "Dinheiro" }
                   );

            modelBuilder.Entity<TipoUtilizador>()
                   .HasData(
                     new TipoUtilizador { TipoId = 0, Name = "Indefenido" },
                     new TipoUtilizador { TipoId = 1, Name = "Empregado" },
                     new TipoUtilizador { TipoId = 2, Name = "Gerente" }
                   );


            modelBuilder.Entity<DetalheVenda>()
                .HasOne(b => b.Estoque)
                .WithOne(i => i.DetalheVenda)
                .HasForeignKey<Estoque>(b => b.DetalheVendaId);


            modelBuilder.Entity<Estoque>()
                .HasOne(b => b.DetalheVenda)
                .WithOne(i => i.Estoque)
                .HasForeignKey<DetalheVenda>(b => b.EstoqueId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (Tipo is DataBaseType.Sqlite)
            {
                options.UseSqlite(CnnString);
            }
            else
            {
                options.UseSqlServer(CnnString);
            }
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is Entidade entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.DataCriacao = now;
                            entity.DataUltimaAlteracao = null;
                            break;
                        case EntityState.Modified:
                            entity.DataUltimaAlteracao = now;
                            break;
                    }
                }
            }

            return base.SaveChanges();

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
        public DbSet<PontoDeVendaSessao> PontoDeVendaSessoes { get; set; }
        public DbSet<TipoPagamento> TipoPagamentos { get; set; }
        public DbSet<TipoUtilizador> TipoUtilizadores { get; set; }
    }


}
