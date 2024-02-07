using System.Collections.Generic;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace TRABALHO_VOLVO
{
    // Classe que representa uma venda de caminhão.
    public class VendaCaminhao
    {
        private int _CodVenda; // Identificador único da venda.
        private DateOnly _DataVenda; // Data da venda.
        private int _FkClientesCodCliente; // Chave estrangeira para o cliente comprador.
        private int _FkConcessionariasCodConc; // Chave estrangeira para a concessionária onde ocorreu a venda.
        private int _FkFuncionariosCodFuncionario; // Chave estrangeira para o funcionário responsável pela venda.
        private int _FkEstoqueCaminhoesCodCaminhaoEstoque; // Chave estrangeira para o estoque de caminhões (caminhão vendido).
        private double _ValorVenda; // Valor da venda.

        // Propriedade que representa o código da venda.
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodVenda
        {
            get { return _CodVenda; }
            set { _CodVenda = value; }
        }

        // Propriedade que representa a data da venda.
        [Required]
        public DateOnly DataVenda
        {
            get { return _DataVenda; }
            set { _DataVenda = value; }
        }

        // Propriedade que representa a chave estrangeira para o cliente comprador.
        [Required]
        [ForeignKey("Fk_VendaCaminhoes_Clientes_CodCliente")]
        public int FkClientesCodCliente
        {
            get { return _FkClientesCodCliente; }
            set { _FkClientesCodCliente = value; }
        }

        // Propriedade que representa a chave estrangeira para a concessionária onde ocorreu a venda.
        [Required]
        [ForeignKey("Fk_VendaCaminhoes_Concessionarias_CodConc")]
        public int FkConcessionariasCodConc
        {
            get { return _FkConcessionariasCodConc; }
            set { _FkConcessionariasCodConc = value; }
        }

        // Propriedade que representa a chave estrangeira para o funcionário responsável pela venda.
        [Required]
        [ForeignKey("Fk_VendaCaminhoes_Funcionarios_CodFuncionario")]
        public int FkFuncionariosCodFuncionario
        {
            get { return _FkFuncionariosCodFuncionario; }
            set { _FkFuncionariosCodFuncionario = value; }
        }

        // Propriedade que representa a chave estrangeira para o estoque de caminhões (caminhão vendido).
        [Required]
        [ForeignKey("Fk_VendaCaminhoes_EstoqueCaminhao_CodCaminhaoEstoque")]
        public int FkEstoqueCaminhoesCodCaminhaoEstoque
        {
            get { return _FkEstoqueCaminhoesCodCaminhaoEstoque; }
            set { _FkEstoqueCaminhoesCodCaminhaoEstoque = value; }
        }

        [Required]
        public double ValorVenda
        {
            get { return _ValorVenda; }
            set { _ValorVenda = value; }
        }
    }
}
