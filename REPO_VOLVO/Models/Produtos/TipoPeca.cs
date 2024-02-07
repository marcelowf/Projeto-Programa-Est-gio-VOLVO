using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa um tipo de peça.
    public class TipoPeca
    {
        private int _CodTipoPeca; // Identificador único do tipo de peça.
        private string? _NomeTipoPeca; // Nome do tipo de peça.
        private bool _PecaAtivo; // Indica se o tipo de peça está ativo.
        private double _ValorTipoPeca; // Valor do tipo de peça.

        // Propriedade que representa o código do tipo de peça.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodTipoPeca 
        { 
            get { return _CodTipoPeca; } 
            set { _CodTipoPeca = value; }
        }

        // Propriedade que representa o nome do tipo de peça.
        [Required]
        [MaxLength(40)]
        public string? NomeTipoPeca 
        { 
            get { return _NomeTipoPeca; } 
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeTipoPeca = value; } } 
        }

        // Propriedade que indica se o tipo de peça está ativo.
        [Required]
        public bool PecaAtivo 
        { 
            get { return _PecaAtivo; } 
            set { _PecaAtivo = value; }
        }

        // Propriedade que representa o valor do tipo de peça.
        [Required]
        public double ValorTipoPeca 
        { 
            get { return _ValorTipoPeca; } 
            set { _ValorTipoPeca = value; }
        }
    }
}
