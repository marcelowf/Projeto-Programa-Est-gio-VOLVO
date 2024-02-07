using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace TRABALHO_VOLVO
{
    // Classe que representa um serviço de manutenção.
    public class ServicoManutencao
    {
        private int _CodManutencao; // Identificador único do serviço de manutenção.
        private DateTime _DataManutencao; // Data em que a manutenção foi realizada.
        private double _ValorServicoManutencao; // Valor do serviço de manutenção.
        private double _QuilometragemCaminhao; // Quilometragem do caminhão no momento da manutenção.
        private string? _DescricaoManutencao; // Descrição da manutenção realizada.
        private int _FkConcessionariasCodConc; // Chave estrangeira para a concessionária associada ao serviço de manutenção.
        private int _FkFuncionariosCodFuncionario; // Chave estrangeira para o funcionário responsável pela manutenção.
        private int _FkCaminhoesCodCaminhaoEstoque; // Chave estrangeira para o caminhão associado ao serviço de manutenção.

        // Propriedade que representa o código do serviço de manutenção.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodManutencao
        {
            get { return _CodManutencao; }
            set { _CodManutencao = value; }
        }

        // Propriedade que representa a data da manutenção.
        [Required]
        public DateTime DataManutencao
        {
            get { return _DataManutencao; }
            set { _DataManutencao = value; }
        }

        // Propriedade que representa o valor do serviço de manutenção.
        [Required]
        public double ValorServicoManutencao
        {
            get { return _ValorServicoManutencao; }
            set { _ValorServicoManutencao = value; }
        }

        // Propriedade que representa a quilometragem do caminhão no momento da manutenção.
        [Required]
        public double QuilometragemCaminhao
        {
            get { return _QuilometragemCaminhao; }
            set { _QuilometragemCaminhao = value; }
        }

        // Propriedade que representa a descrição da manutenção.
        [Required]
        [MaxLength(250)]
        public string? DescricaoManutencao
        {
            get { return _DescricaoManutencao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _DescricaoManutencao = value; } }
        }

        // Propriedade que representa a chave estrangeira para a concessionária associada ao serviço de manutenção.
        [Required]
        [ForeignKey("FK_ServicosManutencao_Concessionarias")]
        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }

        // Propriedade que representa a chave estrangeira para o funcionário responsável pela manutenção.
        [Required]
        [ForeignKey("FK_ServicosManutencao_Funcionarios")]
        public int FkFuncionariosCodFuncionario
        {
            get { return _FkFuncionariosCodFuncionario; }
            set { _FkFuncionariosCodFuncionario = value; }
        }

        // Propriedade que representa a chave estrangeira para o caminhão associado ao serviço de manutenção.
        [Required]
        [ForeignKey("FK_ServicosManutencao_Caminhoes")]
        public int FkCaminhoesCodCaminhao
        {
            get { return _FkCaminhoesCodCaminhaoEstoque; }
            set { _FkCaminhoesCodCaminhaoEstoque = value; }
        }
    }
}
