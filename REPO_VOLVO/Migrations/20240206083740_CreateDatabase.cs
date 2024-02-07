using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRABALHO_volvo.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposPeca",
                columns: table => new
                {
                    CodTipoPeca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoPeca = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PecaAtivo = table.Column<bool>(type: "bit", nullable: false),
                    ValorTipoPeca = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPeca", x => x.CodTipoPeca);
                });

            migrationBuilder.CreateTable(
                name: "Concessionarias",
                columns: table => new
                {
                    CodConc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeConc = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CepConcessionaria = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ConcessionariaAtivo = table.Column<bool>(type: "bit", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concessionarias", x => x.CodConc);
                });


            migrationBuilder.CreateTable(
                name: "ModelosCaminhoes",
                columns: table => new
                {
                    CodModelo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeModelo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModelosAtivo = table.Column<bool>(type: "bit", nullable: false),
                    ValorModeloCaminhao = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosCaminhoes", x => x.CodModelo);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    CodCargo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCargo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SalarioBase = table.Column<double>(type: "float", nullable: false),
                    PorcentagemComissao = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.CodCargo);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CodCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocIdentificadorCliente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClienteAtivo = table.Column<bool>(type: "bit", nullable: false),
                    EmailCliente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroContatoCliente = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CodCliente);
                });


            //========================================= FKS COMECAM AQUI!!!! =====================================================


            migrationBuilder.CreateTable(
                name: "Caminhoes",
                columns: table => new
                {
                    CodCaminhao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quilometragem = table.Column<double>(type: "float", nullable: false),
                    PlacaCaminhao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorCaminhao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodChassiCaminhao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataFabricacao = table.Column<DateOnly>(type: "date", nullable: false),
                    CaminhaoAtivo = table.Column<bool>(type: "bit", nullable: false),
                    FkClientesCodCliente = table.Column<int>(type: "int", nullable: false),
                    FkModelosCaminhoesCodModelo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhoes", x => x.CodCaminhao);
                    table.ForeignKey(
                        name: "FK_Caminhoes_Clientes",
                        column: x => x.FkClientesCodCliente,
                        principalTable: "Clientes",
                        principalColumn: "CodCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caminhoes_ModelosCaminhoes",
                        column: x => x.FkModelosCaminhoesCodModelo,
                        principalTable: "ModelosCaminhoes",
                        principalColumn: "CodModelo",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateTable(
                name: "EstoqueCaminhao",
                columns: table => new
                {
                    CodCaminhaoEstoque = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodChassiEstoque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorEstoqueCaminhao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataFabricacao = table.Column<DateOnly>(type: "date", nullable: false),
                    FkModelosCaminhoesCodModelo = table.Column<int>(type: "int", nullable: false),
                    FkConcessionariasCodConc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstoqueCaminhao", x => x.CodCaminhaoEstoque);
                    table.ForeignKey(
                        name: "FK_EstoqueCaminhao_ModelosCaminhoes",
                        column: x => x.FkModelosCaminhoesCodModelo,
                        principalTable: "ModelosCaminhoes",
                        principalColumn: "CodModelo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstoqueCaminhao_Concessionarias",
                        column: x => x.FkConcessionariasCodConc,
                        principalTable: "Concessionarias",
                        principalColumn: "CodConc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstoquePecas",
                columns: table => new
                {
                    CodPecaEstoque = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFabricacao = table.Column<DateOnly>(type: "date", nullable: false),
                    FkTiposPecaCodTipoPeca = table.Column<int>(type: "int", nullable: false),
                    FkConcessionariasCodConc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstoquePecas", x => x.CodPecaEstoque);
                    table.ForeignKey(
                        name: "FK_EstoquePecas_TiposPeca",
                        column: x => x.FkTiposPecaCodTipoPeca,
                        principalTable: "TiposPeca",
                        principalColumn: "CodTipoPeca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstoquePecas_Concessionarias",
                        column: x => x.FkConcessionariasCodConc,
                        principalTable: "Concessionarias",
                        principalColumn: "CodConc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    CodFuncionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFuncionario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CpfFuncionario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FuncionarioAtivo = table.Column<bool>(type: "bit", nullable: false),
                    NumeroContatoFuncionario = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    FkCargosCodCargo = table.Column<int>(type: "int", nullable: false),
                    FkConcessionariasCodConc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.CodFuncionario);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Cargos",
                        column: x => x.FkCargosCodCargo,
                        principalTable: "Cargos",
                        principalColumn: "CodCargo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Concessionarias",
                        column: x => x.FkConcessionariasCodConc,
                        principalTable: "Concessionarias",
                        principalColumn: "CodConc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicosManutencao",
                columns: table => new
                {
                    CodManutencao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataManutencao = table.Column<DateOnly>(type: "date", nullable: false),
                    ValorServicoManutencao = table.Column<double>(type: "float", nullable: false),
                    QuilometragemCaminhao = table.Column<double>(type: "float", nullable: false),
                    DescricaoManutencao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FkConcessionariasCodConc = table.Column<int>(type: "int", nullable: false),
                    FkFuncionariosCodFuncionario = table.Column<int>(type: "int", nullable: false),
                    FkCaminhoesCodCaminhao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicosManutencao", x => x.CodManutencao);
                    table.ForeignKey(
                        name: "FK_ServicosManutencao_Concessionarias",
                        column: x => x.FkConcessionariasCodConc,
                        principalTable: "Concessionarias",
                        principalColumn: "CodConc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicosManutencao_Funcionarios",
                        column: x => x.FkFuncionariosCodFuncionario,
                        principalTable: "Funcionarios",
                        principalColumn: "CodFuncionario",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ServicosManutencao_Caminhoes",
                        column: x => x.FkCaminhoesCodCaminhao,
                        principalTable: "Caminhoes",
                        principalColumn: "CodCaminhao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicoTiposPeca",
                columns: table => new
                {
                    CodServicoTipoPeca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkCodManutencao = table.Column<int>(type: "int", nullable: false),
                    FkTiposPecaCodTipoPeca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicoTiposPeca", x => x.CodServicoTipoPeca);
                    table.ForeignKey(
                        name: "FK_ServicoTiposPeca_ServicosManutencao",
                        column: x => x.FkCodManutencao,
                        principalTable: "ServicosManutencao",
                        principalColumn: "CodManutencao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Fk_ServicoTiposPeca_TiposPeca",
                        column: x => x.FkTiposPecaCodTipoPeca,
                        principalTable: "TiposPeca",
                        principalColumn: "CodTipoPeca",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendaCaminhoes",
                columns: table => new
                {
                    CodVenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataVenda = table.Column<DateOnly>(type: "date", nullable: false),
                    FkClientesCodCliente = table.Column<int>(type: "int", nullable: false),
                    FkConcessionariasCodConc = table.Column<int>(type: "int", nullable: false),
                    FkFuncionariosCodFuncionario = table.Column<int>(type: "int", nullable: false),
                    FkEstoqueCaminhoesCodCaminhaoEstoque = table.Column<int>(type: "int", nullable: false),
                    ValorVenda = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaCaminhoes", x => x.CodVenda);
                    table.ForeignKey(
                        name: "Fk_VendaCaminhoes_Clientes_CodCliente",
                        column: x => x.FkClientesCodCliente,
                        principalTable: "Clientes",
                        principalColumn: "CodCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Fk_VendaCaminhoes_Concessionarias_CodConc",
                        column: x => x.FkConcessionariasCodConc,
                        principalTable: "Concessionarias",
                        principalColumn: "CodConc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Fk_VendaCaminhoes_Funcionarios_CodFuncionario",
                        column: x => x.FkFuncionariosCodFuncionario,
                        principalTable: "Funcionarios",
                        principalColumn: "CodFuncionario",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "Fk_VendaCaminhoes_EstoqueCaminhao_CodCaminhaoEstoque",
                        column: x => x.FkEstoqueCaminhoesCodCaminhaoEstoque,
                        principalTable: "EstoqueCaminhao",
                        principalColumn: "CodCaminhaoEstoque",
                        onDelete: ReferentialAction.NoAction);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhoes");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Concessionarias");

            migrationBuilder.DropTable(
                name: "EstoqueCaminhao");

            migrationBuilder.DropTable(
                name: "EstoquePecas");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "ModelosCaminhoes");

            migrationBuilder.DropTable(
                name: "ServicosManutencao");

            migrationBuilder.DropTable(
                name: "ServicoTiposPeca");

            migrationBuilder.DropTable(
                name: "TiposPeca");

            migrationBuilder.DropTable(
                name: "VendaCaminhoes");
        }
    }
}
