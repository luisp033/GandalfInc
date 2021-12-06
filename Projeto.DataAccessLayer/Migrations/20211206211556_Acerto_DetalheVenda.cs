using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Acerto_DetalheVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Vendas_VendaIdentificador",
                table: "Estoques");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_VendaIdentificador",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "VendaIdentificador",
                table: "Estoques");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VendaIdentificador",
                table: "Estoques",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_VendaIdentificador",
                table: "Estoques",
                column: "VendaIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Vendas_VendaIdentificador",
                table: "Estoques",
                column: "VendaIdentificador",
                principalTable: "Vendas",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
