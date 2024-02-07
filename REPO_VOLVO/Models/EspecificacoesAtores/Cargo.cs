using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa um cargo.
    public class Cargo
    {
        private int _CodCargo; // Identificador único do cargo.
        private string? _NomeCargo; // Nome do cargo.
        private bool _CargoAtivo; // Indica se o cargo está ativo.
        private double _SalarioBase; // Salário base do cargo.
        private double _PorcentagemComissao; // Porcentagem de comissão do cargo.

        // Propriedade que representa o código do cargo.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCargo 
        { 
            get { return _CodCargo; } 
            set { _CodCargo = value; }
        }

        // Propriedade que representa o nome do cargo.
        [Required]
        [MaxLength(30)]
        public string? NomeCargo 
        { 
            get { return _NomeCargo; } 
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeCargo = value; } }
        }

        // Propriedade que indica se o cargo está ativo.
        [Required]
        public bool CargoAtivo 
        { 
            get { return _CargoAtivo; } 
            set { _CargoAtivo = value; }
        }

        // Propriedade que representa o salário base do cargo.
        public double SalarioBase 
        { 
            get { return _SalarioBase; }
            set { _SalarioBase = value; }
        }

        // Propriedade que representa a porcentagem de comissão do cargo.
        public double PorcentagemComissao 
        { 
            get { return _PorcentagemComissao; } 
            set { _PorcentagemComissao = value; }
        }
    }
}
