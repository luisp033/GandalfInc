using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Entidade_PontoVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PontoDeVendas_Utilizadores_UtilizadorLogadoIdentificador",
                table: "PontoDeVendas");

            migrationBuilder.DropIndex(
                name: "IX_PontoDeVendas_UtilizadorLogadoIdentificador",
                table: "PontoDeVendas");

            migrationBuilder.DropColumn(
                name: "UtilizadorLogadoIdentificador",
                table: "PontoDeVendas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UtilizadorLogadoIdentificador",
                table: "PontoDeVendas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PontoDeVendas_UtilizadorLogadoIdentificador",
                table: "PontoDeVendas",
                column: "UtilizadorLogadoIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_PontoDeVendas_Utilizadores_UtilizadorLogadoIdentificador",
                table: "PontoDeVendas",
                column: "UtilizadorLogadoIdentificador",
                principalTable: "Utilizadores",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
