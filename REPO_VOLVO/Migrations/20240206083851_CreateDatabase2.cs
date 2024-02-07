using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRABALHO_volvo.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PecaEstoqueAtiva",
                table: "EstoquePecas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CaminhaoEstoqueAtivo",
                table: "EstoqueCaminhao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CargoAtivo",
                table: "Cargos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PecaEstoqueAtiva",
                table: "EstoquePecas");

            migrationBuilder.DropColumn(
                name: "CaminhaoEstoqueAtivo",
                table: "EstoqueCaminhao");

            migrationBuilder.DropColumn(
                name: "CargoAtivo",
                table: "Cargos");
        }
    }
}