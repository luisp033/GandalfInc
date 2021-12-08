﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto.DataAccessLayer;

namespace Projeto.DataAccessLayer.Migrations
{
    [DbContext(typeof(ProjetoDBContext))]
    [Migration("20211207120153_Acerto_EstoqueDetalheVenda_relacoes_3")]
    partial class Acerto_EstoqueDetalheVenda_relacoes_3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.CategoriaProduto", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("OrdemApresentacao")
                        .HasColumnType("int");

                    b.HasKey("Identificador");

                    b.ToTable("CategoriaProdutos");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Cliente", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("MoradaEntregaIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MoradaFaturacaoIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NumeroFiscal")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Identificador");

                    b.HasIndex("MoradaEntregaIdentificador");

                    b.HasIndex("MoradaFaturacaoIdentificador");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.DetalheVenda", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("EstoqueId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PrecoFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("VendaIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Identificador");

                    b.HasIndex("EstoqueId")
                        .IsUnique();

                    b.HasIndex("VendaIdentificador");

                    b.ToTable("DetalheVendas");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Estoque", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DetalheVendaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NumeroSerie")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("ProdutoIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Identificador");

                    b.HasIndex("ProdutoIdentificador");

                    b.ToTable("Estoques");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Loja", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("MoradaIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NumeroFiscal")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<Guid?>("ResponsavelIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Identificador");

                    b.HasIndex("MoradaIdentificador");

                    b.HasIndex("ResponsavelIdentificador");

                    b.ToTable("Lojas");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.MarcaProduto", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Origem")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Identificador");

                    b.ToTable("MarcaProdutos");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Morada", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Localidade")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Observacoes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Identificador");

                    b.ToTable("Morada");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.PontoDeVenda", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LojaIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Identificador");

                    b.HasIndex("LojaIdentificador");

                    b.ToTable("PontoDeVendas");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.PontoDeVendaSessao", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataLogin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataLogout")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PontoDeVendaIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UtilizadorIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Identificador");

                    b.HasIndex("PontoDeVendaIdentificador");

                    b.HasIndex("UtilizadorIdentificador");

                    b.ToTable("PontoDeVendaSessoes");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Produto", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("CategoriaIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ean")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("MarcaIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Identificador");

                    b.HasIndex("CategoriaIdentificador");

                    b.HasIndex("MarcaIdentificador");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.TipoPagamento", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TipoPagamentos");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Indefenido"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Multibanco"
                        },
                        new
                        {
                            Id = 2,
                            Name = "MbWay"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dinheiro"
                        });
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Utilizador", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Senha")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Identificador");

                    b.ToTable("Utilizadores");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Venda", b =>
                {
                    b.Property<Guid>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClienteIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataHoraVenda")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumeroSerie")
                        .HasColumnType("int");

                    b.Property<Guid?>("PontoDeVendaSessaoIdentificador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("TipoPagamentoId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorPagamento")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Identificador");

                    b.HasIndex("ClienteIdentificador");

                    b.HasIndex("PontoDeVendaSessaoIdentificador");

                    b.HasIndex("TipoPagamentoId");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Cliente", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.Morada", "MoradaEntrega")
                        .WithMany()
                        .HasForeignKey("MoradaEntregaIdentificador");

                    b.HasOne("Projeto.DataAccessLayer.Entidades.Morada", "MoradaFaturacao")
                        .WithMany()
                        .HasForeignKey("MoradaFaturacaoIdentificador");

                    b.Navigation("MoradaEntrega");

                    b.Navigation("MoradaFaturacao");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.DetalheVenda", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.Estoque", "Estoque")
                        .WithOne("DetalheVenda")
                        .HasForeignKey("Projeto.DataAccessLayer.Entidades.DetalheVenda", "EstoqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projeto.DataAccessLayer.Entidades.Venda", "Venda")
                        .WithMany()
                        .HasForeignKey("VendaIdentificador");

                    b.Navigation("Estoque");

                    b.Navigation("Venda");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Estoque", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoIdentificador");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Loja", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.Morada", "Morada")
                        .WithMany()
                        .HasForeignKey("MoradaIdentificador");

                    b.HasOne("Projeto.DataAccessLayer.Entidades.Utilizador", "Responsavel")
                        .WithMany()
                        .HasForeignKey("ResponsavelIdentificador");

                    b.Navigation("Morada");

                    b.Navigation("Responsavel");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.PontoDeVenda", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.Loja", "Loja")
                        .WithMany()
                        .HasForeignKey("LojaIdentificador");

                    b.Navigation("Loja");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.PontoDeVendaSessao", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.PontoDeVenda", "PontoDeVenda")
                        .WithMany()
                        .HasForeignKey("PontoDeVendaIdentificador");

                    b.HasOne("Projeto.DataAccessLayer.Entidades.Utilizador", "Utilizador")
                        .WithMany()
                        .HasForeignKey("UtilizadorIdentificador");

                    b.Navigation("PontoDeVenda");

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Produto", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.CategoriaProduto", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaIdentificador");

                    b.HasOne("Projeto.DataAccessLayer.Entidades.MarcaProduto", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaIdentificador");

                    b.Navigation("Categoria");

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Venda", b =>
                {
                    b.HasOne("Projeto.DataAccessLayer.Entidades.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteIdentificador");

                    b.HasOne("Projeto.DataAccessLayer.Entidades.PontoDeVendaSessao", "PontoDeVendaSessao")
                        .WithMany()
                        .HasForeignKey("PontoDeVendaSessaoIdentificador");

                    b.HasOne("Projeto.DataAccessLayer.Entidades.TipoPagamento", "TipoPagamento")
                        .WithMany()
                        .HasForeignKey("TipoPagamentoId");

                    b.Navigation("Cliente");

                    b.Navigation("PontoDeVendaSessao");

                    b.Navigation("TipoPagamento");
                });

            modelBuilder.Entity("Projeto.DataAccessLayer.Entidades.Estoque", b =>
                {
                    b.Navigation("DetalheVenda");
                });
#pragma warning restore 612, 618
        }
    }
}
