namespace UnitTests
{
    // Classe de testes unitários para o controlador de Venda de Caminhão.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(11)]
    public class UnitTestVendaCaminhao
    {
        // Controlador sob teste.
        private VendaCaminhaoController sut;

        // Variável estática para armazenar o ID da venda de caminhão.
        public static int IdVendaCaminhao;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new VendaCaminhaoController();
        }

        // Testa o método PostVendaCaminhao para verificar se uma nova venda de caminhão pode ser cadastrada com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarVendaCaminhao()
        {
            DateOnly Data = new DateOnly(2004, 12, 5);

            // Preparando os dados da venda de caminhão para o teste.
            var vendaCaminhao = new VendaCaminhao
            {
                DataVenda = Data,
                FkClientesCodCliente = UnitTestClientes.IdCliente,
                FkConcessionariasCodConc = UnitTestConcessionarias.IdConcessionaria,
                FkFuncionariosCodFuncionario = UnitTestFuncionarios.IdFuncionario,
                FkEstoqueCaminhoesCodCaminhaoEstoque = UnitTestEstoqueCaminhoes.IdEstoqueCaminhao
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostVendaCaminhao(vendaCaminhao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID da venda de caminhão foi atribuído corretamente.
            IdVendaCaminhao = vendaCaminhao.CodVenda;
            Assert.AreEqual(IdVendaCaminhao, vendaCaminhao.CodVenda);
        }

        // Testa o método GetTodasVendaCaminhoes para verificar se retorna uma lista não vazia de vendas de caminhão.
        [Test, Order(2)]
        public void DeveRetornarListaDeVendaCaminhoesAoChamarListar()
        {
            var resultado = sut.GetTodasVendaCaminhoes();

            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetVendaCaminhao para verificar se retorna uma venda de caminhão com base no ID.
        [Test, Order(3)]
        public void DeveRetornarVendaCaminhaoAoBuscarPorIdExistente()
        {
            Assert.DoesNotThrow(() => sut.GetVendaCaminhao(IdVendaCaminhao));
        }
    }
}
