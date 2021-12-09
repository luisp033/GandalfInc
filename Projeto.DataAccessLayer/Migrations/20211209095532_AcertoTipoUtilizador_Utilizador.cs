using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.DataAccessLayer.Migrations
{
    public partial class AcertoTipoUtilizador_Utilizador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilizadores_TipoUtilizador_TipoId",
                table: "Utilizadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoUtilizador",
                table: "TipoUtilizador");

            migrationBuilder.RenameTable(
                name: "TipoUtilizador",
                newName: "TipoUtilizadores");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Utilizadores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Utilizadores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoUtilizadores",
                table: "TipoUtilizadores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizadores_TipoUtilizadores_TipoId",
                table: "Utilizadores",
                column: "TipoId",
                principalTable: "TipoUtilizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilizadores_TipoUtilizadores_TipoId",
                table: "Utilizadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoUtilizadores",
                table: "TipoUtilizadores");

            migrationBuilder.RenameTable(
                name: "TipoUtilizadores",
                newName: "TipoUtilizador");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Utilizadores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Utilizadores",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoUtilizador",
                table: "TipoUtilizador",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizadores_TipoUtilizador_TipoId",
                table: "Utilizadores",
                column: "TipoId",
                principalTable: "TipoUtilizador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
