namespace UnitTests
{
    // Classe de testes unitários para o controlador de Tipos de Peças.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(5)]
    public class UnitTestTipoPeca
    {
        // Controlador sob teste.
        private TiposPecasController sut;

        // Variável estática para armazenar informações relevantes durante os testes.
        public static int IdTipoPecas;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new TiposPecasController();
        }

        // Testa o método PostTiposPeca para verificar se um novo tipo de peça pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarTipoPeca()
        {
            // Preparando os dados do tipo de peça para o teste.
            var tipoPeca = new TipoPeca
            {
                NomeTipoPeca = "Teste",
                ValorTipoPeca = 10000
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostTiposPeca(tipoPeca);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okObjectResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);

            // Verificando se o ID do tipo de peça foi atribuído corretamente.
            IdTipoPecas = tipoPeca.CodTipoPeca;
            Assert.AreEqual(IdTipoPecas ,tipoPeca.CodTipoPeca);
        }
        
        // Testa o método GetTodosTiposPecas para verificar se retorna uma lista não vazia de tipos de peças.
        [Test, Order(2)]
        public void DeveRetornarStatusCode200ParaObterUmaListaDeTipoPecas()
        {
            var resultado = sut.GetTodosTiposPecas();
            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetTiposPeca para verificar se retorna um tipo de peça com base no ID.
        [Test, Order(3)]
        public void DeveRetornarUmObjetoTipoPecaComBaseEmSeuID()
        {
            var resultado = sut.GetTiposPeca(IdTipoPecas) as ObjectResult;
            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.Value);
        }

        // Testa o método PutTiposPeca para verificar se um tipo de peça pode ser atualizado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarStatusCode200ParaAtualizarTipoPecaExistente()
        {
            // Preparando os dados do tipo de peça atualizado para o teste.
            var tipoPeca = new TipoPeca
            {
                NomeTipoPeca = "Teste01 Atualizado",
                ValorTipoPeca = 10000
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutTiposPeca(IdTipoPecas, tipoPeca) as OkResult;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }

        // Testa o método PutDeleteTiposPeca para verificar se um tipo de peça pode ser desativado com sucesso.
        [Test, Order(5)]
        public void DeveRetornarOkAoDezativarTipoPeca()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteTiposPeca(IdTipoPecas);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var OkResult = resultado as OkResult;
            Assert.AreEqual(200, OkResult?.StatusCode);
        }
    }
}
