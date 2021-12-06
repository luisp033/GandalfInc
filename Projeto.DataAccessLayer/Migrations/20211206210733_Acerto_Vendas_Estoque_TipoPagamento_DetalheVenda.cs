using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Acerto_Vendas_Estoque_TipoPagamento_DetalheVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Produtos_ProdutoIdentificador",
                table: "DetalheVendas");

            migrationBuilder.DropColumn(
                name: "DataVenda",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "Ean",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "NumeroSerie",
                table: "DetalheVendas");

            migrationBuilder.RenameColumn(
                name: "TipoPagamento",
                table: "Vendas",
                newName: "TipoPagamentoId");

            migrationBuilder.RenameColumn(
                name: "IdentificadorVenda",
                table: "Estoques",
                newName: "VendaIdentificador");

            migrationBuilder.RenameColumn(
                name: "ProdutoIdentificador",
                table: "DetalheVendas",
                newName: "EstoqueProdutoIdentificador");

            migrationBuilder.RenameColumn(
                name: "EstoqueIdentificador",
                table: "DetalheVendas",
                newName: "Identificador");

            migrationBuilder.RenameIndex(
                name: "IX_DetalheVendas_ProdutoIdentificador",
                table: "DetalheVendas",
                newName: "IX_DetalheVendas_EstoqueProdutoIdentificador");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroSerie",
                table: "Estoques",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TipoPagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPagamentos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TipoPagamentos",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Indefenido" },
                    { 1, "Multibanco" },
                    { 2, "MbWay" },
                    { 3, "Dinheiro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_TipoPagamentoId",
                table: "Vendas",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_VendaIdentificador",
                table: "Estoques",
                column: "VendaIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueProdutoIdentificador",
                table: "DetalheVendas",
                column: "EstoqueProdutoIdentificador",
                principalTable: "Estoques",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Vendas_VendaIdentificador",
                table: "Estoques",
                column: "VendaIdentificador",
                principalTable: "Vendas",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_TipoPagamentos_TipoPagamentoId",
                table: "Vendas",
                column: "TipoPagamentoId",
                principalTable: "TipoPagamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueProdutoIdentificador",
                table: "DetalheVendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Vendas_VendaIdentificador",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_TipoPagamentos_TipoPagamentoId",
                table: "Vendas");

            migrationBuilder.DropTable(
                name: "TipoPagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_TipoPagamentoId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_VendaIdentificador",
                table: "Estoques");

            migrationBuilder.RenameColumn(
                name: "TipoPagamentoId",
                table: "Vendas",
                newName: "TipoPagamento");

            migrationBuilder.RenameColumn(
                name: "VendaIdentificador",
                table: "Estoques",
                newName: "IdentificadorVenda");

            migrationBuilder.RenameColumn(
                name: "EstoqueProdutoIdentificador",
                table: "DetalheVendas",
                newName: "ProdutoIdentificador");

            migrationBuilder.RenameColumn(
                name: "Identificador",
                table: "DetalheVendas",
                newName: "EstoqueIdentificador");

            migrationBuilder.RenameIndex(
                name: "IX_DetalheVendas_EstoqueProdutoIdentificador",
                table: "DetalheVendas",
                newName: "IX_DetalheVendas_ProdutoIdentificador");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroSerie",
                table: "Estoques",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVenda",
                table: "Estoques",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ean",
                table: "Estoques",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroSerie",
                table: "DetalheVendas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalheVendas_Produtos_ProdutoIdentificador",
                table: "DetalheVendas",
                column: "ProdutoIdentificador",
                principalTable: "Produtos",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
