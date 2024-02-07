using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa um caminhão em estoque.
    public class CaminhaoEstoque
    {
        private int _CodCaminhaoEstoque; // Identificador único do caminhão em estoque.
        private string? _CodChassiEstoque; // Código do chassi do caminhão em estoque.
        private string? _CorEstoqueCaminhao; // Cor do caminhão em estoque.
        private bool _CaminhaoEstoqueAtivo; // Indica se o caminhão em estoque está ativo.
        private DateOnly _DataFabricacao; // Data de fabricação do caminhão em estoque.
        private int _FkModelosCaminhoesCodModelo; // Chave estrangeira para o código do modelo de caminhão associado.
        private int _FkConcessionariasCodConc; // Chave estrangeira para o código da concessionária associada.

        // Propriedade que representa o código do caminhão em estoque.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCaminhaoEstoque
        {
            get { return _CodCaminhaoEstoque; }
            set { _CodCaminhaoEstoque = value; }
        }

        // Propriedade que representa o código do chassi do caminhão em estoque.
        [Required]
        public string? CodChassiEstoque
        {
            get { return _CodChassiEstoque; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CodChassiEstoque = value; } }
        }

        // Propriedade que representa a cor do caminhão em estoque.
        [Required]
        public string? CorEstoqueCaminhao
        {
            get { return _CorEstoqueCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CorEstoqueCaminhao = value; } }
        }

        // Propriedade que indica se o caminhão em estoque está ativo.
        [Required]
        public bool CaminhaoEstoqueAtivo 
        { 
            get { return _CaminhaoEstoqueAtivo; } 
            set { _CaminhaoEstoqueAtivo = value; }
        }

        // Propriedade que representa a data de fabricação do caminhão em estoque.
        [Required]
        public DateOnly DataFabricacao
        {
            get { return _DataFabricacao; }
            set { _DataFabricacao = value; }
        }

        // Propriedade que representa a chave estrangeira para o código do modelo de caminhão associado.
        [Required]
        [ForeignKey("FK_EstoqueCaminhao_ModelosCaminhoes")]
        public int FkModelosCaminhoesCodModelo
        {
            get { return _FkModelosCaminhoesCodModelo; }
            set { _FkModelosCaminhoesCodModelo = value; }
        }

        // Propriedade que representa a chave estrangeira para o código da concessionária associada.
        [Required]
        [ForeignKey("FK_EstoqueCaminhao_Concessionarias")]
        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }
    }
}
