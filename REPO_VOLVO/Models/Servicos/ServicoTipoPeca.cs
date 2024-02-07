using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa a associação entre um serviço de manutenção e um tipo de peça utilizado.
    public class ServicoTipoPeca
    {
        private int _CodServicoTipoPeca; // Identificador único da associação.
        private int _FkServicosManutencaoCodManutencao; // Chave estrangeira para o serviço de manutenção.
        private int _FkTiposPecaCodTipoPeca; // Chave estrangeira para o tipo de peça.

        // Propriedade que representa o código da associação.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodServicoTipoPeca
        {
            get { return _CodServicoTipoPeca; }
            set { _CodServicoTipoPeca = value; }
        }

        // Propriedade que representa a chave estrangeira para o serviço de manutenção.
        [Required]
        [ForeignKey("FK_ServicoTiposPeca_ServicosManutencao")]
        public int FkCodManutencao
        {
            get { return _FkServicosManutencaoCodManutencao; }
            set { _FkServicosManutencaoCodManutencao = value; }
        }

        // Propriedade que representa a chave estrangeira para o tipo de peça.
        [Required]
        [ForeignKey("Fk_ServicoTiposPeca_TiposPeca")]
        public int FkTiposPecaCodTipoPeca
        {
            get { return _FkTiposPecaCodTipoPeca; }
            set { _FkTiposPecaCodTipoPeca = value; }
        }
    }
}
