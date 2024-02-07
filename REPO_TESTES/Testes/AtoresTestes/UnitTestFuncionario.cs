namespace UnitTests
{
    // Classe de testes unitários para o controlador de Funcionários.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(3)]
    public class UnitTestFuncionarios
    {
        // Controlador sob teste.
        private FuncionariosController sut;

        // Variáveis estáticas para armazenar informações relevantes durante os testes.
        public static int IdFuncionario;
        public static string cpfFuncionario;

        // Método executado antes de cada teste para inicializar o controlador e os dados necessários.
        [SetUp]
        public void Setup()
        {
            sut = new FuncionariosController();
            cpfFuncionario = "12345";
        }

        // Testa o método PostFuncionario para verificar se um novo funcionário pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarStatusCode200ParaCadastrarUmFuncionario()
        {
            // Preparando os dados do funcionário para o teste.
            var funcionario = new Funcionario
            {
                NomeFuncionario = "Marcelo",
                CpfFuncionario = cpfFuncionario,
                NumeroContatoFuncionario = cpfFuncionario,
                FkCargosCodCargo = UnitTestCargos.IdCargo,
                FkConcessionariasCodConc = UnitTestConcessionarias.IdConcessionaria
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostFuncionario(funcionario);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do funcionário foi atribuído corretamente.
            IdFuncionario = funcionario.CodFuncionario;
            Assert.AreEqual(IdFuncionario, funcionario.CodFuncionario);
        }
        
        // Testa o método GetTodosFuncionarios para verificar se retorna uma lista não vazia de funcionários.
        [Test, Order(2)]
        public void DeveRetornarStatusCode200ParaObterUmaListaDeFuncionarios()
        {
            var resultado = sut.GetTodosFuncionarios();
            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetFuncionario para verificar se retorna um funcionário com base no CPF.
        [Test, Order(3)]
        public void DeveRetornarUmObjetoFuncionarioComBaseEmSeuCPF()
        {
            var resultado = sut.GetFuncionario(cpfFuncionario) as ObjectResult;
            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.Value);
        }

        // Testa o método PutFuncionario para verificar se um funcionário existente pode ser atualizado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarStatusCode200ParaAtualizarFuncionarioExistente()
        {
            // Preparando os dados do funcionário atualizado para o teste.
            var funcionario = new Funcionario
            {
                NomeFuncionario = "Marcelo Atualizado",
                NumeroContatoFuncionario = cpfFuncionario,
                FkCargosCodCargo = UnitTestCargos.IdCargo,
                FkConcessionariasCodConc = UnitTestConcessionarias.IdConcessionaria
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutFuncionario(cpfFuncionario, funcionario);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }

        // Testa o método PutDeleteFuncionario para verificar se um funcionário pode ser desativado com sucesso.
        [Test, Order(5)]
        public void DeveRetornarOkAoDeletarFuncionario()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteFuncionario(cpfFuncionario);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }
    }
}
