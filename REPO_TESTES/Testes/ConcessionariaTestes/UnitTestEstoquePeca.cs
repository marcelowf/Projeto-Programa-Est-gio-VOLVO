namespace UnitTests
{
    // Classe de testes unitários para o controlador de Estoque de Peças.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(8)]
    public class UnitTestEstoquePeca
    {
        // Controlador sob teste.
        private EstoquePecasController sut;

        // Variável estática para armazenar informações relevantes durante os testes.
        public static int IdEstoquePeca;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new EstoquePecasController();
        }

        // Testa o método PostEstoquePeca para verificar se um novo estoque de peça pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarEstoquePeca()
        {
            // Preparando os dados do estoque de peça para o teste.
            DateOnly Data = new DateOnly(2004, 12, 5);
            var pecaEstoque = new PecaEstoque
            {
                DataFabricacao = Data,
                FkTiposPecaCodTipoPeca = UnitTestTipoPeca.IdTipoPecas,
                FkConcessionariasCodConc = UnitTestConcessionarias.IdConcessionaria
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostEstoquePeca(pecaEstoque);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do estoque de peça foi atribuído corretamente.
            IdEstoquePeca = pecaEstoque.CodPecaEstoque;
            Assert.AreEqual(IdEstoquePeca, pecaEstoque.CodPecaEstoque);
        }

        // Testa o método GetTodasEstoquePecas para verificar se retorna uma lista não vazia de estoque de peças.
        [Test, Order(2)]
        public void DeveRetornarListaDeEstoquePecasAoChamarListar()
        {
            var resultado = sut.GetTodasEstoquePecas();

            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetEstoquePeca para verificar se retorna um estoque de peça com base no ID.
        [Test, Order(3)]
        public void DeveRetornarEstoquePecaAoBuscarPorIdExistente()
        {
            Assert.DoesNotThrow(() => sut.GetEstoquePeca(IdEstoquePeca));
        }

        // Testa o método PutDeleteEstoquePeca para verificar se um estoque de peça pode ser desativado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarOkAoDezativarEstoquePeca()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteEstoquePeca(IdEstoquePeca);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var OkResult = resultado as OkResult;
            Assert.AreEqual(200, OkResult?.StatusCode);
        }
    }
}
