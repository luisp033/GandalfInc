using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Entidade_PontoDeVendaSessao_AcertoVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_PontoDeVendas_PontoDeVendaIdentificador",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Utilizadores_VendedorIdentificador",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_PontoDeVendaIdentificador",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "PontoDeVendaIdentificador",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "VendedorIdentificador",
                table: "Vendas",
                newName: "PontoDeVendaSessaoIdentificador");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_VendedorIdentificador",
                table: "Vendas",
                newName: "IX_Vendas_PontoDeVendaSessaoIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_PontoDeVendaSessoes_PontoDeVendaSessaoIdentificador",
                table: "Vendas",
                column: "PontoDeVendaSessaoIdentificador",
                principalTable: "PontoDeVendaSessoes",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_PontoDeVendaSessoes_PontoDeVendaSessaoIdentificador",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "PontoDeVendaSessaoIdentificador",
                table: "Vendas",
                newName: "VendedorIdentificador");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_PontoDeVendaSessaoIdentificador",
                table: "Vendas",
                newName: "IX_Vendas_VendedorIdentificador");

            migrationBuilder.AddColumn<Guid>(
                name: "PontoDeVendaIdentificador",
                table: "Vendas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_PontoDeVendaIdentificador",
                table: "Vendas",
                column: "PontoDeVendaIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_PontoDeVendas_PontoDeVendaIdentificador",
                table: "Vendas",
                column: "PontoDeVendaIdentificador",
                principalTable: "PontoDeVendas",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Utilizadores_VendedorIdentificador",
                table: "Vendas",
                column: "VendedorIdentificador",
                principalTable: "Utilizadores",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
