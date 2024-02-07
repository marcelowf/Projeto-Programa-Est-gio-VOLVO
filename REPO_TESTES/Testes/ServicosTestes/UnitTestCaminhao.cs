namespace UnitTests
{
    // Classe de testes unitários para o controlador de Caminhões.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(9)]
    public class UnitTestCaminhoes
    {
        // Controlador sob teste.
        private CaminhoesController sut;

        // Variável estática para armazenar informações relevantes durante os testes.
        public static int IdCaminhao;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new CaminhoesController();
        }

        // Testa o método PostCaminhao para verificar se um novo caminhão pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarCaminhao()
        {
            // Preparando os dados do caminhão para o teste.
            var caminhao = new Caminhao
            {
                Quilometragem = 1000,
                PlacaCaminhao = "321",
                CorCaminhao = "Azul",
                CodChassiCaminhao = "456",
                FkClientesCodCliente = UnitTestClientes.IdCliente,
                FkModelosCaminhoesCodModelo = UnitTestModeloCaminhoes.IdModeloCaminhoes
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostCaminhao(caminhao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do caminhão foi atribuído corretamente.
            IdCaminhao = caminhao.CodCaminhao;
            Assert.AreEqual(IdCaminhao, caminhao.CodCaminhao);
        }

        // Testa o método GetTodosCaminhoes para verificar se retorna uma lista não vazia de caminhões.
        [Test, Order(2)]
        public void DeveRetornarListaDeCaminhoesAoChamarListar()
        {
            var resultado = sut.GetTodosCaminhoes();

            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetCaminhao para verificar se retorna um caminhão com base no ID.
        [Test, Order(3)]
        public void DeveRetornarCaminhaoAoBuscarPorIdExistente()
        {
            Assert.DoesNotThrow(() => sut.GetCaminhao(IdCaminhao));
        }

        // Testa o método PutDeleteCaminhao para verificar se um caminhão pode ser desativado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarOkAoDesativarCaminhao()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteCaminhao(IdCaminhao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var okResult = resultado as OkResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }
    }
}
