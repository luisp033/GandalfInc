using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Acerto_EstoqueDetalheVenda_relacoes_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_DetalheVendas_DetalheVendaId",
                table: "Estoques");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_DetalheVendaId",
                table: "Estoques");

            migrationBuilder.AddColumn<Guid>(
                name: "EstoqueId",
                table: "DetalheVendas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalheVendas_EstoqueId",
                table: "DetalheVendas",
                column: "EstoqueId",
                unique: true,
                filter: "[EstoqueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueId",
                table: "DetalheVendas",
                column: "EstoqueId",
                principalTable: "Estoques",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueId",
                table: "DetalheVendas");

            migrationBuilder.DropIndex(
                name: "IX_DetalheVendas_EstoqueId",
                table: "DetalheVendas");

            migrationBuilder.DropColumn(
                name: "EstoqueId",
                table: "DetalheVendas");

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
    }
}
