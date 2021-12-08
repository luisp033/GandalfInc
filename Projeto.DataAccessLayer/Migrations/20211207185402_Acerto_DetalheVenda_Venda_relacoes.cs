using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Acerto_DetalheVenda_Venda_relacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Vendas_VendaIdentificador",
                table: "DetalheVendas");

            migrationBuilder.RenameColumn(
                name: "VendaIdentificador",
                table: "DetalheVendas",
                newName: "VendaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalheVendas_VendaIdentificador",
                table: "DetalheVendas",
                newName: "IX_DetalheVendas_VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalheVendas_Vendas_VendaId",
                table: "DetalheVendas",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalheVendas_Vendas_VendaId",
                table: "DetalheVendas");

            migrationBuilder.RenameColumn(
                name: "VendaId",
                table: "DetalheVendas",
                newName: "VendaIdentificador");

            migrationBuilder.RenameIndex(
                name: "IX_DetalheVendas_VendaId",
                table: "DetalheVendas",
                newName: "IX_DetalheVendas_VendaIdentificador");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalheVendas_Vendas_VendaIdentificador",
                table: "DetalheVendas",
                column: "VendaIdentificador",
                principalTable: "Vendas",
                principalColumn: "Identificador",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
