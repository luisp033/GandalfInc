using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class Add_TipoUtilizador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Utilizadores");

            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Utilizadores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoUtilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUtilizador", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TipoUtilizador",
                columns: new[] { "Id", "Name" },
                values: new object[] { 0, "Indefenido" });

            migrationBuilder.InsertData(
                table: "TipoUtilizador",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Empregado" });

            migrationBuilder.InsertData(
                table: "TipoUtilizador",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Gerente" });

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_TipoId",
                table: "Utilizadores",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizadores_TipoUtilizador_TipoId",
                table: "Utilizadores",
                column: "TipoId",
                principalTable: "TipoUtilizador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilizadores_TipoUtilizador_TipoId",
                table: "Utilizadores");

            migrationBuilder.DropTable(
                name: "TipoUtilizador");

            migrationBuilder.DropIndex(
                name: "IX_Utilizadores_TipoId",
                table: "Utilizadores");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Utilizadores");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Utilizadores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
