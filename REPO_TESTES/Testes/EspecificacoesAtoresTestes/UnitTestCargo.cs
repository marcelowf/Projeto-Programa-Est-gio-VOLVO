namespace UnitTests
{
    // Classe de testes unitários para o controlador de Cargos.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(2)]
    public class UnitTestCargos
    {
        // Controlador sob teste.
        private CargosController sut;

        // Variável estática para armazenar informações relevantes durante os testes.
        public static int IdCargo;

        // Método executado antes de cada teste para inicializar o controlador.
        [SetUp]
        public void Setup()
        {
            sut = new CargosController();
        }

        // Testa o método PostCargo para verificar se um novo cargo pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarStatusCode200ParaCadastrarUmCargo()
        {
            // Preparando os dados do cargo para o teste.
            var cargo = new Cargo
            {
                NomeCargo = "Mecanico",
                SalarioBase = 1500,
                PorcentagemComissao = 3
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostCargo(cargo);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var okResult = resultado as OkResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do cargo foi atribuído corretamente.
            IdCargo = cargo.CodCargo;
            Assert.AreEqual(IdCargo, cargo.CodCargo);
        }

        // Testa o método GetTodosCargos para verificar se retorna uma lista não vazia de cargos.
        [Test, Order(2)]
        public void DeveRetornarStatusCode200ParaObterUmaListaDeCargos()
        {
            var resultado = sut.GetTodosCargos();
            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetCargo para verificar se retorna um cargo com base no ID.
        [Test, Order(3)]
        public void DeveRetornarUmObjetoCargoComBaseEmSeuID()
        {
            var resultado = sut.GetCargo(IdCargo) as ObjectResult;
            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.Value);
        }

        // Testa o método PutCargo para verificar se um cargo pode ser atualizado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarStatusCode200ParaAtualizarCargoExistente()
        {
            // Preparando os dados do cargo atualizado para o teste.
            var cargo = new Cargo
            {
                NomeCargo = "Mecanico Atualizado",
                SalarioBase = 1800,
                PorcentagemComissao = 4
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutCargo(IdCargo, cargo) as OkResult;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }

        // Testa o método PutDeleteCargo para verificar se um cargo pode ser desativado com sucesso.
        [Test, Order(5)]
        public void DeveRetornarOkAoDesativarCargo()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteCargo(IdCargo) as OkResult;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }
    }
}
