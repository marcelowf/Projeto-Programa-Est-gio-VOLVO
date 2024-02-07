using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa uma peça em estoque.
    public class PecaEstoque
    {
        private int _CodPecaEstoque; // Identificador único da peça em estoque.
        private bool _PecaEstoqueAtiva; // Indica se a peça está ativa no estoque.
        private DateOnly _DataFabricacao; // Data de fabricação da peça.
        private int _FkTiposPecaCodTipoPeca; // Chave estrangeira para o tipo de peça.
        private int _FkConcessionariasCodConc; // Chave estrangeira para a concessionária.

        // Propriedade que representa o código da peça em estoque.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodPecaEstoque 
        { 
            get { return _CodPecaEstoque; } 
            set { _CodPecaEstoque = value; } 
        }

        // Propriedade que indica se a peça está ativa no estoque.
        [Required]
        public bool PecaEstoqueAtiva 
        { 
            get { return _PecaEstoqueAtiva; } 
            set { _PecaEstoqueAtiva = value; }
        }

        // Propriedade que representa a data de fabricação da peça.
        [Required]
        public DateOnly DataFabricacao 
        { 
            get { return _DataFabricacao; } 
            set { _DataFabricacao = value; } 
        }

        // Propriedade que representa a chave estrangeira para o tipo de peça.
        [Required]
        [ForeignKey("FK_EstoquePecas_TiposPeca")]
        public int FkTiposPecaCodTipoPeca 
        { 
            get { return _FkTiposPecaCodTipoPeca; } 
            set { _FkTiposPecaCodTipoPeca = value; } 
        }

        // Propriedade que representa a chave estrangeira para a concessionária.
        [Required]
        [ForeignKey("FK_EstoquePecas_Concessionarias")]
        public int FkConcessionariasCodConc 
        { 
            get { return _FkConcessionariasCodConc; } 
            set { _FkConcessionariasCodConc = value; } 
        }
    }
}
