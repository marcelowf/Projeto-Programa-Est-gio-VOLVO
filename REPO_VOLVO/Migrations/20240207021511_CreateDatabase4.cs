using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRABALHO_volvo.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodChassiEstoque",
                table: "EstoqueCaminhao",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CodChassiCaminhao",
                table: "Caminhoes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_CpfFuncionario",
                table: "Funcionarios",
                column: "CpfFuncionario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstoqueCaminhao_CodChassiEstoque",
                table: "EstoqueCaminhao",
                column: "CodChassiEstoque",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Concessionarias_CepConcessionaria",
                table: "Concessionarias",
                column: "CepConcessionaria",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_DocIdentificadorCliente",
                table: "Clientes",
                column: "DocIdentificadorCliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Caminhoes_CodChassiCaminhao",
                table: "Caminhoes",
                column: "CodChassiCaminhao",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_CpfFuncionario",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_EstoqueCaminhao_CodChassiEstoque",
                table: "EstoqueCaminhao");

            migrationBuilder.DropIndex(
                name: "IX_Concessionarias_CepConcessionaria",
                table: "Concessionarias");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_DocIdentificadorCliente",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Caminhoes_CodChassiCaminhao",
                table: "Caminhoes");

            migrationBuilder.AlterColumn<string>(
                name: "CodChassiEstoque",
                table: "EstoqueCaminhao",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CodChassiCaminhao",
                table: "Caminhoes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
