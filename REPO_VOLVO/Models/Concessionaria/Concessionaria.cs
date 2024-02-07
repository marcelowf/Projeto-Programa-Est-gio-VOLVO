using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa uma concessionária.
    public class Concessionaria
    {
        private int _CodConc; // Identificador único da concessionária.
        private string? _NomeConc; // Nome da concessionária.
        private string? _CepConcessionaria; // CEP da concessionária.
        private bool _ConcessionariaAtivo; // Indica se a concessionária está ativa.
        private string? _Pais; // País da concessionária.
        private string? _Estado; // Estado da concessionária.
        private string? _Cidade; // Cidade da concessionária.
        private string? _Rua; // Rua da concessionária.
        private string? _Numero; // Número da concessionária.

        // Propriedade que representa o código da concessionária.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodConc
        {
            get { return _CodConc; }
            set { _CodConc = value; }
        }

        // Propriedade que representa o nome da concessionária.
        [Required]
        [MaxLength(40)]
        public string? NomeConc
        {
            get { return _NomeConc; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeConc = value; } }
        }

        // Propriedade que representa o CEP da concessionária.
        [Required]
        [MaxLength(10)]
        public string? CepConcessionaria
        {
            get { return _CepConcessionaria; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _CepConcessionaria = value; } }
        }

        // Propriedade que indica se a concessionária está ativa.
        [Required]
        public bool ConcessionariaAtivo
        {
            get { return _ConcessionariaAtivo; }
            set { { _ConcessionariaAtivo = value; } }
        }

        // Propriedade que representa o país da concessionária.
        [Required]
        [MaxLength(30)]
        public string? Pais
        {
            get { return _Pais; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _Pais = value.ToUpper(); } }
        }

        // Propriedade que representa o estado da concessionária.
        [Required]
        [MaxLength(30)]
        public string? Estado
        {
            get { return _Estado; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _Estado = value.ToUpper(); } }
        }

        // Propriedade que representa a cidade da concessionária.
        [Required]
        [MaxLength(30)]
        public string? Cidade
        {
            get { return _Cidade; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _Cidade = value.ToUpper(); } }
        }

        // Propriedade que representa a rua da concessionária.
        [Required]
        [MaxLength(30)]
        public string? Rua
        {
            get { return _Rua; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _Rua = value; } }
        }

        // Propriedade que representa o número da concessionária.
        [Required]
        [MaxLength(5)]
        public string? Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
    }
}
