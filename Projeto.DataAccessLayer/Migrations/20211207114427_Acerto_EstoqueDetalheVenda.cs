using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Acerto_EstoqueDetalheVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueProdutoIdentificador",
                table: "DetalheVendas");

            migrationBuilder.DropIndex(
                name: "IX_DetalheVendas_EstoqueProdutoIdentificador",
                table: "DetalheVendas");

            migrationBuilder.DropColumn(
                name: "EstoqueProdutoIdentificador",
                table: "DetalheVendas");

            migrationBuilder.AddColumn<Guid>(
                name: "DetalheVendaId",
                table: "Estoques",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_DetalheVendaId",
                table: "Estoques",
                column: "DetalheVendaId",
                unique: true,
                filter: "[DetalheVendaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_DetalheVendas_DetalheVendaId",
                table: "Estoques",
                column: "DetalheVendaId",
                principalTable: "DetalheVendas",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_DetalheVendas_DetalheVendaId",
                table: "Estoques");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_DetalheVendaId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "DetalheVendaId",
                table: "Estoques");

            migrationBuilder.AddColumn<Guid>(
                name: "EstoqueProdutoIdentificador",
                table: "DetalheVendas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalheVendas_EstoqueProdutoIdentificador",
                table: "DetalheVendas",
                column: "EstoqueProdutoIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueProdutoIdentificador",
                table: "DetalheVendas",
                column: "EstoqueProdutoIdentificador",
                principalTable: "Estoques",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
