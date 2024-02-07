using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa um caminhão.
    public class Caminhao
    {
        private int _CodCaminhao; // Identificador único do caminhão.
        private double _Quilometragem; // Quilometragem do caminhão.
        private string? _PlacaCaminhao; // Placa do caminhão.
        private string? _CorCaminhao; // Cor do caminhão.
        private string? _CodChassiCaminhao; // Código do chassi do caminhão.
        private DateOnly _DataFabricacao; // Data de fabricação do caminhão.
        private bool _CaminhaoAtivo; // Indica se o caminhão está ativo.
        private int _FkClientesCodCliente; // Chave estrangeira para o cliente associado ao caminhão.
        private int _FkModelosCaminhoesCodModelo; // Chave estrangeira para o modelo do caminhão.

        // Propriedade que representa o código do caminhão.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCaminhao
        {
            get { return _CodCaminhao; }
            set { _CodCaminhao = value; }
        }

        // Propriedade que representa a quilometragem do caminhão.
        [Required]
        public double Quilometragem
        {
            get { return _Quilometragem; }
            set { _Quilometragem = value; }
        }

        // Propriedade que representa a placa do caminhão.
        [Required]
        public string? PlacaCaminhao
        {
            get { return _PlacaCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _PlacaCaminhao = value; } }
        }

        // Propriedade que representa a cor do caminhão.
        [Required]
        public string? CorCaminhao
        {
            get { return _CorCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CorCaminhao = value; } }
        }

        // Propriedade que representa o código do chassi do caminhão.
        [Required]
        public string? CodChassiCaminhao
        {
            get { return _CodChassiCaminhao; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CodChassiCaminhao = value; } }
        }

        // Propriedade que representa a data de fabricação do caminhão.
        [Required]
        public DateOnly DataFabricacao
        {
            get { return _DataFabricacao; }
            set { _DataFabricacao = value; }
        }

        // Propriedade que indica se o caminhão está ativo.
        [Required]
        public bool CaminhaoAtivo
        {
            get { return _CaminhaoAtivo; }
            set { _CaminhaoAtivo = value; }
        }

        // Propriedade que representa a chave estrangeira para o cliente associado ao caminhão.
        [Required]
        [ForeignKey("FK_Caminhoes_Clientes")]
        public int FkClientesCodCliente
        {
            get { return _FkClientesCodCliente; }
            set { _FkClientesCodCliente = value; }
        }

        // Propriedade que representa a chave estrangeira para o modelo do caminhão.
        [Required]
        [ForeignKey("FK_Caminhoes_ModelosCaminhao")]
        public int FkModelosCaminhoesCodModelo
        {
            get { return _FkModelosCaminhoesCodModelo; }
            set { _FkModelosCaminhoesCodModelo = value; }
        }
    }
}
