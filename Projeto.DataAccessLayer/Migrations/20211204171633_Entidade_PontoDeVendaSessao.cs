using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Entidade_PontoDeVendaSessao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontoDeVendaSessoes",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PontoDeVendaIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UtilizadorIdentificador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataLogout = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoDeVendaSessoes", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_PontoDeVendaSessoes_PontoDeVendas_PontoDeVendaIdentificador",
                        column: x => x.PontoDeVendaIdentificador,
                        principalTable: "PontoDeVendas",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PontoDeVendaSessoes_Utilizadores_UtilizadorIdentificador",
                        column: x => x.UtilizadorIdentificador,
                        principalTable: "Utilizadores",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PontoDeVendaSessoes_PontoDeVendaIdentificador",
                table: "PontoDeVendaSessoes",
                column: "PontoDeVendaIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_PontoDeVendaSessoes_UtilizadorIdentificador",
                table: "PontoDeVendaSessoes",
                column: "UtilizadorIdentificador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontoDeVendaSessoes");
        }
    }
}
