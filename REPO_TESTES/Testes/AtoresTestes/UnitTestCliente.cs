namespace UnitTests
{
    // Classe de testes unitários para o controlador de Clientes.
    // A ordem de execução dos testes é determinada pela diretiva Order().
    [TestFixture, Order(4)]
    public class UnitTestClientes
    {
        // Controlador sob teste.
        private ClientesController sut;

        // Variáveis estáticas para armazenar informações relevantes durante os testes.
        public static int IdCliente;
        public static string docIdentificadorCliente;

        // Método executado antes de cada teste para inicializar o controlador e os dados necessários.
        [SetUp]
        public void Setup()
        {
            sut = new ClientesController();
            docIdentificadorCliente = "123";
        }

        // Testa o método PostCliente para verificar se um novo cliente pode ser cadastrado com sucesso.
        [Test, Order(1)]
        public void DeveRetornarOkAoCadastrarCliente()
        {
            // Preparando os dados do cliente para o teste.
            var cliente = new Cliente
            {
                NomeCliente = "Cliente Teste",
                DocIdentificadorCliente = docIdentificadorCliente,
                EmailCliente = "clienteTeste@gmail.com",
                NumeroContatoCliente = "123"
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PostCliente(cliente);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se o ID do cliente foi atribuído corretamente.
            IdCliente = cliente.CodCliente;
            Assert.AreEqual(IdCliente ,cliente.CodCliente);
        }

        // Testa o método GetTodosClientes para verificar se retorna uma lista não vazia de clientes.
        [Test, Order(2)]
        public void DeveRetornarListaDeClientesAoChamarListar()
        {
            var resultado = sut.GetTodosClientes();

            Assert.That(resultado, Is.Not.Empty);
        }

        // Testa o método GetCliente para verificar se retorna um cliente com base no documento identificador.
        [Test, Order(3)]
        public void DeveRetornarClienteAoBuscarPorDocumentoExistente()
        {
            Assert.DoesNotThrow(() => sut.GetCliente(docIdentificadorCliente));
        }

        // Testa o método PutCliente para verificar se um cliente existente pode ser atualizado com sucesso.
        [Test, Order(4)]
        public void DeveRetornarOkAoAtualizarCliente()
        {
            // Preparando os dados do cliente atualizado para o teste.
            var cliente = new Cliente
            {
                NomeCliente = "Cliente Atualizado",
                DocIdentificadorCliente = docIdentificadorCliente,
                EmailCliente = "clienteTeste@gmail.com",
                NumeroContatoCliente = "123"
            };

            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutCliente(docIdentificadorCliente ,cliente);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);

            // Verificando se uma mensagem de sucesso é retornada.
            var mensagem = okResult?.Value as string;
            Assert.IsNotEmpty(mensagem);
        }

        // Testa o método PutDeleteCliente para verificar se um cliente pode ser desativado com sucesso.
        [Test, Order(5)]
        public void DeveRetornarOkAoDesativarCliente()
        {
            // Executando o método a ser testado e verificando o resultado.
            var resultado = sut.PutDeleteCliente(docIdentificadorCliente);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }
    }
}
