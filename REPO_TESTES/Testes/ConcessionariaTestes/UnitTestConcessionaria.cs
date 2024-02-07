namespace UnitTests
{
    // Classe de testes unitários para o controlador de Concessionárias.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(1)]
    public class UnitTestConcessionarias
    {
        // Controlador sob teste.
        private ConcessionariasController sut;

        // Variáveis estáticas para armazenar informações relevantes durante os testes.
        public static int IdConcessionaria;
        public static string cepConcessionaria;

        // Método executado antes de cada teste para inicializar o controlador e os dados necessários.
        [SetUp]
        public void Setup()
        {
            sut = new ConcessionariasController();
            cepConcessionaria = "12345";
        }

        // Testa o método PostConcessionaria para verificar se uma nova concessionária pode ser cadastrada com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarConcessionaria()
        {
            // Preparando os dados da concessionária para o teste.
            var concessionaria = new Concessionaria
            {
                NomeConc = "Volvo Teste",
                CepConcessionaria = cepConcessionaria,
                Pais = "Br",
                Estado = "Pr",
                Cidade = "Cu",
                Rua = "Rua teste",
                Numero = "12345"
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostConcessionaria(concessionaria);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID da concessionária foi atribuído corretamente.
            IdConcessionaria = concessionaria.CodConc;
            Assert.AreEqual(IdConcessionaria, concessionaria.CodConc);
        }

        // Testa o método GetTodasConcessionarias para verificar se retorna uma lista não vazia de concessionárias.
        [Test, Order(2)]
        public void DeveRetornarListaDeConcessionariasAoChamarListar()
        {
            var resultado = sut.GetTodasConcessionarias();

            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetConcessionaria para verificar se retorna uma concessionária com base no CEP.
        [Test, Order(3)]
        public void DeveRetornarConcessionariaAoBuscarPorCepExistente()
        {
            Assert.DoesNotThrow(() => sut.GetConcessionaria(cepConcessionaria));
        }

        // Testa o método PutConcessionaria para verificar se uma concessionária existente pode ser atualizada com sucesso.
        [Test, Order(4)]
        public void DeveRetornarOkAoAtualizarConcessionaria()
        {
            // Preparando os dados da concessionária atualizada para o teste.
            var concessionaria = new Concessionaria
            {
                NomeConc = "Volvo Teste Atualizada",
                CepConcessionaria = cepConcessionaria,
                Pais = "Br",
                Estado = "Pr",
                Cidade = "Cu",
                Rua = "Rua teste Atualizada",
                Numero = "12345"
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutConcessionaria(cepConcessionaria, concessionaria);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }

        // Testa o método PutDeleteConcessionaria para verificar se uma concessionária pode ser desativada com sucesso.
        [Test, Order(5)]
        public void DeveRetornarOkAoDesativarConcessionaria()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteConcessionaria(cepConcessionaria);
            
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }
    }
}
