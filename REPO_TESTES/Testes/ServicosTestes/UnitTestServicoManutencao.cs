namespace UnitTests
{
    // Classe de testes unitários para o controlador de Serviço de Manutenção.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(10)]
    public class UnitTestServicoManutencao
    {
        // Controlador sob teste.
        private ServicoManutencaoController sut;

        // Variáveis estáticas para armazenar informações relevantes durante os testes.
        public static int IdServicoManutencao;
        public static int IdServicoTipoPeca;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new ServicoManutencaoController();
        }

        // Testa o método PostServicoManutencao para verificar se um novo serviço de manutenção pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarServicoManutencao()
        {
            DateTime? Data = new DateTime(2024, 2, 8);

            // Preparando os dados do serviço de manutenção para o teste.
            var servicoManutencao = new ServicoManutencao
            {
                DataManutencao = Data.HasValue ? Data.Value : default(DateTime),
                ValorServicoManutencao = 20000,
                QuilometragemCaminhao = 100000,
                DescricaoManutencao = "Teste Manutencao",
                FkConcessionariasCodConc = UnitTestConcessionarias.IdConcessionaria,
                FkFuncionariosCodFuncionario = UnitTestFuncionarios.IdFuncionario,
                FkCaminhoesCodCaminhao = UnitTestCaminhoes.IdCaminhao,
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostServicoManutencao(servicoManutencao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do serviço de manutenção foi atribuído corretamente.
            IdServicoManutencao = servicoManutencao.CodManutencao;
            Assert.AreEqual(IdServicoManutencao, servicoManutencao.CodManutencao);
        }
        
        // Testa o método GetTodosServicosManutencao para verificar se retorna uma lista não vazia de serviços de manutenção.
        [Test, Order(2)]
        public void DeveRetornarListaDeServicoManutencoesAoChamarListar()
        {
            var resultado = sut.GetTodosServicosManutencao();

            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetServicoManutencao para verificar se retorna um serviço de manutenção com base no ID.
        [Test, Order(3)]
        public void DeveRetornarServicoManutencaoAoBuscarPorIdExistente()
        {
            Assert.DoesNotThrow(() => sut.GetServicoManutencao(IdServicoManutencao));
        }

        // Testa o método PutServicoManutencao para verificar se um serviço de manutenção pode ser atualizado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarStatusCode200ParaAtualizarServicoManutencao()
        {
            // Preparando os dados do serviço de manutenção para atualização.
            var servicoManutencao = new ServicoManutencao
            {
                DescricaoManutencao = "Teste Manutencao Atualizado"
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutServicoManutencao(IdServicoManutencao, servicoManutencao);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, (int)resultado.GetType().GetProperty("StatusCode").GetValue(resultado));
        }
    }
}
