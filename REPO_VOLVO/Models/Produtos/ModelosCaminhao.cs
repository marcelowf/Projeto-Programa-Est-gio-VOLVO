using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa um modelo de caminhão.
    public class ModelosCaminhao
    {
        private int _CodModelo; // Identificador único do modelo de caminhão.
        private string? _NomeModelo; // Nome do modelo de caminhão.
        private bool _ModelosAtivo; // Indica se o modelo de caminhão está ativo.
        private double _ValorModeloCaminhao; // Valor do modelo de caminhão.

        // Propriedade que representa o código do modelo de caminhão.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodModelo 
        { 
            get { return _CodModelo; } 
            set { _CodModelo = value; }
        }

        // Propriedade que representa o nome do modelo de caminhão.
        [Required]
        [MaxLength(30)]
        public string? NomeModelo 
        { 
            get { return _NomeModelo; } 
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeModelo = value; } }
        }

        // Propriedade que indica se o modelo de caminhão está ativo.
        [Required]
        public bool ModelosAtivo 
        { 
            get { return _ModelosAtivo; } 
            set { _ModelosAtivo = value; }
        }

        // Propriedade que representa o valor do modelo de caminhão.
        [Required]
        public double ValorModeloCaminhao 
        { 
            get { return _ValorModeloCaminhao; } 
            set { _ValorModeloCaminhao = value; }
        }
    }
}
