using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Acerto_EstoqueDetalheVenda_relacoes_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueId",
                table: "DetalheVendas");

            migrationBuilder.DropIndex(
                name: "IX_DetalheVendas_EstoqueId",
                table: "DetalheVendas");

            migrationBuilder.AlterColumn<Guid>(
                name: "EstoqueId",
                table: "DetalheVendas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalheVendas_EstoqueId",
                table: "DetalheVendas",
                column: "EstoqueId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueId",
                table: "DetalheVendas",
                column: "EstoqueId",
                principalTable: "Estoques",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Estoques_EstoqueId",
                table: "DetalheVendas");

            migrationBuilder.DropIndex(
                name: "IX_DetalheVendas_EstoqueId",
                table: "DetalheVendas");

            migrationBuilder.AlterColumn<Guid>(
                name: "EstoqueId",
                table: "DetalheVendas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}
