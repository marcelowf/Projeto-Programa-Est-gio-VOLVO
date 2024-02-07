namespace UnitTests
{
    // Classe de testes unitários para o controlador de Estoque de Caminhões.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(7)]
    public class UnitTestEstoqueCaminhoes
    {
        // Controlador sob teste.
        private EstoqueCaminhoesController sut;

        // Variável estática para armazenar informações relevantes durante os testes.
        public static int IdEstoqueCaminhao;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new EstoqueCaminhoesController();
        }

        // Testa o método PostEstoqueCaminhao para verificar se um novo estoque de caminhão pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarEstoqueCaminhao()
        {
            // Preparando os dados do estoque de caminhão para o teste.
            DateOnly Data = new DateOnly(2004, 12, 5);
            var caminhaoEstoque = new CaminhaoEstoque
            {
                CodChassiEstoque = "Teste01",
                CorEstoqueCaminhao = "Verde",
                FkModelosCaminhoesCodModelo = UnitTestModeloCaminhoes.IdModeloCaminhoes,
                FkConcessionariasCodConc = UnitTestConcessionarias.IdConcessionaria
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostEstoqueCaminhao(caminhaoEstoque);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do estoque de caminhão foi atribuído corretamente.
            IdEstoqueCaminhao = caminhaoEstoque.CodCaminhaoEstoque;
            Assert.AreEqual(IdEstoqueCaminhao, caminhaoEstoque.CodCaminhaoEstoque);
        }

        // Testa o método GetTodosEstoqueCaminhoes para verificar se retorna uma lista não vazia de estoque de caminhões.
        [Test, Order(2)]
        public void DeveRetornarListaDeEstoqueCaminhoesAoChamarListar()
        {
            var resultado = sut.GetTodosEstoqueCaminhoes();

            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetEstoqueCaminhao para verificar se retorna um estoque de caminhão com base no ID.
        [Test, Order(3)]
        public void DeveRetornarEstoqueCaminhaoAoBuscarPorIdExistente()
        {
            Assert.DoesNotThrow(() => sut.GetEstoqueCaminhao(IdEstoqueCaminhao));
        }

        // Testa o método PutDeleteEstoqueCaminhao para verificar se um estoque de caminhão pode ser desativado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarOkAoDezativarEstoqueCaminhao()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteEstoqueCaminhao(IdEstoqueCaminhao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var OkResult = resultado as OkResult;
            Assert.AreEqual(200, OkResult?.StatusCode);
        }
    }
}
