using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRABALHO_VOLVO
{
    // Classe que representa um cliente.
    public class Cliente
    {
        private int _CodCliente; // Identificador único do cliente.
        private string? _NomeCliente; // Nome do cliente.
        private string? _DocIdentificador; // Documento identificador do cliente.
        private bool _ClienteAtivo; // Indica se o cliente está ativo ou não.
        private string? _EmailCliente; // Endereço de email do cliente.
        private string? _NumeroContatoCliente; // Número de contato do cliente.

        // Propriedade que representa o código do cliente.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodCliente
        {
            get { return _CodCliente; }
            set { _CodCliente = value; }
        }

        // Propriedade que representa o nome do cliente.
        [Required]
        [MaxLength(50)]
        public string? NomeCliente
        {
            get { return _NomeCliente; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _NomeCliente = value; } }
        }

        // Propriedade que representa o documento identificador do cliente.
        [Required]
        [MaxLength(20)]
        public string? DocIdentificadorCliente
        {
            get { return _DocIdentificador; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _DocIdentificador = value; } }
        }

        // Propriedade que indica se o cliente está ativo.
        [Required]
        public bool ClienteAtivo
        {
            get { return _ClienteAtivo; }
            set { _ClienteAtivo = value; }
        }

        // Propriedade que representa o email do cliente.
        [MaxLength(100)]
        public string? EmailCliente
        {
            get { return _EmailCliente; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _EmailCliente = value; } }
        }

        // Propriedade que representa o número de contato do cliente.
        [Required]
        [MaxLength(25)]
        public string? NumeroContatoCliente
        {
            get { return _NumeroContatoCliente; }
            set { if (!string.IsNullOrWhiteSpace(value)) { _NumeroContatoCliente = value; } }
        }
    }
}
