using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class AcertoRelacaoEstoque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProdutoIdentificador",
                table: "Estoques",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoIdentificador",
                table: "Estoques",
                column: "ProdutoIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoIdentificador",
                table: "Estoques",
                column: "ProdutoIdentificador",
                principalTable: "Produtos",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoIdentificador",
                table: "Estoques");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_ProdutoIdentificador",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "ProdutoIdentificador",
                table: "Estoques");
        }
    }
}
