namespace UnitTests
{
    // Classe de testes unitários para o controlador de Modelos de Caminhões.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(6)]
    public class UnitTestModeloCaminhoes
    {
        // Controlador sob teste.
        private ModelosCaminhoesController sut;

        // Variável estática para armazenar informações relevantes durante os testes.
        public static int IdModeloCaminhoes;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new ModelosCaminhoesController();
        }

        // Testa o método PostModelosCaminhao para verificar se um novo modelo de caminhão pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarModeloCaminhao()
        {
            // Preparando os dados do modelo de caminhão para o teste.
            var modelosCaminhao = new ModelosCaminhao
            {
                NomeModelo = "Volvo Teste",
                ValorModeloCaminhao = 2200000
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostModelosCaminhao(modelosCaminhao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do modelo de caminhão foi atribuído corretamente.
            IdModeloCaminhoes = modelosCaminhao.CodModelo;
            Assert.AreEqual(IdModeloCaminhoes, modelosCaminhao.CodModelo);
        }
        
        // Testa o método GetTodosModelosCaminhoes para verificar se retorna uma lista não vazia de modelos de caminhões.
        [Test, Order(2)]
        public void DeveRetornarStatusCode200ParaObterUmaListaDeModeloCaminhoes()
        {
            var resultado = sut.GetTodosModelosCaminhoes();
            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetModelosCaminhao para verificar se retorna um modelo de caminhão com base no ID.
        [Test, Order(3)]
        public void DeveRetornarUmObjetoModeloCaminhaoComBaseEmSeuID()
        {
            var resultado = sut.GetModelosCaminhao(IdModeloCaminhoes) as ObjectResult;
            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.Value);
        }

        // Testa o método PutModelosCaminhao para verificar se um modelo de caminhão pode ser atualizado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarStatusCode200ParaAtualizarModeloCaminhaoExistente()
        {
            // Preparando os dados do modelo de caminhão atualizado para o teste.
            var modelosCaminhao = new ModelosCaminhao
            {
                NomeModelo = "Volvo Teste Atualizado",
                ValorModeloCaminhao = 2200000
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutModelosCaminhao(IdModeloCaminhoes, modelosCaminhao) as OkResult;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }

        // Testa o método PutDeleteModeloCaminhao para verificar se um modelo de caminhão pode ser desativado com sucesso.
        [Test, Order(5)]
        public void DeveRetornarOkAoDezativarModeloCaminhao()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteModeloCaminhao(IdModeloCaminhoes);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var OkResult = resultado as OkResult;
            Assert.AreEqual(200, OkResult?.StatusCode);
        }
    }
}
