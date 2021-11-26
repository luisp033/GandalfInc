using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProdutos",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrdemApresentacao = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProdutos", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ean = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroSerie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVenda = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdentificadorVenda = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "MarcaProdutos",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Origem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaProdutos", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Morada",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Morada", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ean = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoriaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarcaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Produtos_CategoriaProdutos_CategoriaIdentificador",
                        column: x => x.CategoriaIdentificador,
                        principalTable: "CategoriaProdutos",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produtos_MarcaProdutos_MarcaIdentificador",
                        column: x => x.MarcaIdentificador,
                        principalTable: "MarcaProdutos",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumeroFiscal = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    MoradaEntregaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MoradaFaturacaoIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Clientes_Morada_MoradaEntregaIdentificador",
                        column: x => x.MoradaEntregaIdentificador,
                        principalTable: "Morada",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Morada_MoradaFaturacaoIdentificador",
                        column: x => x.MoradaFaturacaoIdentificador,
                        principalTable: "Morada",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumeroFiscal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoradaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Lojas_Morada_MoradaIdentificador",
                        column: x => x.MoradaIdentificador,
                        principalTable: "Morada",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lojas_Utilizadores_ResponsavelIdentificador",
                        column: x => x.ResponsavelIdentificador,
                        principalTable: "Utilizadores",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PontoDeVendas",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LojaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UtilizadorLogadoIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoDeVendas", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_PontoDeVendas_Lojas_LojaIdentificador",
                        column: x => x.LojaIdentificador,
                        principalTable: "Lojas",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PontoDeVendas_Utilizadores_UtilizadorLogadoIdentificador",
                        column: x => x.UtilizadorLogadoIdentificador,
                        principalTable: "Utilizadores",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PontoDeVendaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VendedorIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClienteIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataHoraVenda = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumeroSerie = table.Column<int>(type: "int", nullable: false),
                    TipoPagamento = table.Column<int>(type: "int", nullable: true),
                    ValorPagamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Vendas_Clientes_ClienteIdentificador",
                        column: x => x.ClienteIdentificador,
                        principalTable: "Clientes",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_PontoDeVendas_PontoDeVendaIdentificador",
                        column: x => x.PontoDeVendaIdentificador,
                        principalTable: "PontoDeVendas",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Utilizadores_VendedorIdentificador",
                        column: x => x.VendedorIdentificador,
                        principalTable: "Utilizadores",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalheVendas",
                columns: table => new
                {
                    EstoqueIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalheVendas", x => x.EstoqueIdentificador);
                    table.ForeignKey(
                        name: "FK_DetalheVendas_Produtos_ProdutoIdentificador",
                        column: x => x.ProdutoIdentificador,
                        principalTable: "Produtos",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalheVendas_Vendas_VendaIdentificador",
                        column: x => x.VendaIdentificador,
                        principalTable: "Vendas",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_MoradaEntregaIdentificador",
                table: "Clientes",
                column: "MoradaEntregaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_MoradaFaturacaoIdentificador",
                table: "Clientes",
                column: "MoradaFaturacaoIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_DetalheVendas_ProdutoIdentificador",
                table: "DetalheVendas",
                column: "ProdutoIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_DetalheVendas_VendaIdentificador",
                table: "DetalheVendas",
                column: "VendaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Lojas_MoradaIdentificador",
                table: "Lojas",
                column: "MoradaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Lojas_ResponsavelIdentificador",
                table: "Lojas",
                column: "ResponsavelIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_PontoDeVendas_LojaIdentificador",
                table: "PontoDeVendas",
                column: "LojaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_PontoDeVendas_UtilizadorLogadoIdentificador",
                table: "PontoDeVendas",
                column: "UtilizadorLogadoIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaIdentificador",
                table: "Produtos",
                column: "CategoriaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_MarcaIdentificador",
                table: "Produtos",
                column: "MarcaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_ClienteIdentificador",
                table: "Vendas",
                column: "ClienteIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_PontoDeVendaIdentificador",
                table: "Vendas",
                column: "PontoDeVendaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VendedorIdentificador",
                table: "Vendas",
                column: "VendedorIdentificador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheVendas");

            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "CategoriaProdutos");

            migrationBuilder.DropTable(
                name: "MarcaProdutos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PontoDeVendas");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Morada");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
