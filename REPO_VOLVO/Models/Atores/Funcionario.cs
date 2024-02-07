using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa um funcionário.
    public class Funcionario
    {
        private int _CodFuncionario; // Identificador único do funcionário.
        private string? _NomeFuncionario; // Nome do funcionário.
        private string? _CpfFuncionario; // CPF do funcionário.
        private bool _FuncionarioAtivo; // Indica se o funcionário está ativo ou não.
        private string? _NumeroContatoFuncionario; // Número de contato do funcionário.
        private int? _FkCargosCodCargo; // Chave estrangeira para o código do cargo associado ao funcionário.
        private int _FkConcessionariasCodConc; // Chave estrangeira para o código da concessionária associada ao funcionário.

        // Propriedade que representa o código do funcionário.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodFuncionario
        {
            get { return _CodFuncionario; }
            set { _CodFuncionario = value; }
        }

        // Propriedade que representa o nome do funcionário.
        [Required]
        [MaxLength(50)]
        public string? NomeFuncionario
        {
            get { return _NomeFuncionario; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeFuncionario = value; } }
        }

        // Propriedade que representa o CPF do funcionário.
        [Required]
        [MaxLength(20)]
        public string? CpfFuncionario
        {
            get { return _CpfFuncionario; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CpfFuncionario = value; } }
        }

        // Propriedade que indica se o funcionário está ativo.
        [Required]
        public bool FuncionarioAtivo
        {
            get { return _FuncionarioAtivo; }
            set { _FuncionarioAtivo = value; }
        }

        // Propriedade que representa o número de contato do funcionário.
        [Required]
        [MaxLength(25)]
        public string? NumeroContatoFuncionario
        {
            get { return _NumeroContatoFuncionario; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _NumeroContatoFuncionario = value; } }
        }

        // Propriedade que representa a chave estrangeira para o código do cargo associado ao funcionário.
        [Required]
        [ForeignKey("FK_Funcionarios_Cargos")]
        public int? FkCargosCodCargo
        {
            get { return _FkCargosCodCargo; }
            set { _FkCargosCodCargo = value; }
        }

        // Propriedade que representa a chave estrangeira para o código da concessionária associada ao funcionário.
        [ForeignKey("FK_Funcionarios_Concessionarias")]
        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }
    }
}
