namespace UnitTests
{
    // Classe de teste para operações de exclusão.
    [TestFixture, Order(12)]
    public class UnitTestDelete
    {
        // Instância dos controladores a serem testados.
        private VendaCaminhaoController sutVendaCaminhao = new VendaCaminhaoController();
        private ServicoManutencaoController sutServicoManutencao = new ServicoManutencaoController();
        private EstoquePecasController sutEstoquePeca = new EstoquePecasController();
        private EstoqueCaminhoesController sutEstoqueCaminhao = new EstoqueCaminhoesController();
        private CaminhoesController sutCaminhao = new CaminhoesController();
        private ModelosCaminhoesController sutModeloCaminhao = new ModelosCaminhoesController();
        private TiposPecasController sutTipoPeca = new TiposPecasController();
        private ClientesController sutClientes = new ClientesController();
        private FuncionariosController sutFuncionarios = new FuncionariosController();
        private CargosController sutCargos = new CargosController();
        private ConcessionariasController sutConcessionaria = new ConcessionariasController();
        
        // Teste para verificar se a exclusão de uma venda de caminhão retorna o status code 200.
        [Test, Order(1)]
        public void DeveRetornarStatusCode200ParaDeletarVendaCaminhao()
        {
            var resultado = sutVendaCaminhao.DeleteVendaCaminhao(UnitTestVendaCaminhao.IdVendaCaminhao);
            
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }

        // Teste para verificar se a exclusão de um serviço de manutenção retorna o status code 200.
        [Test, Order(2)]
        public void DeveRetornarStatusCode200ParaDeletarServicoManutencao()
        {
            var resultado = sutServicoManutencao.DeleteServicoManutencao(UnitTestServicoManutencao.IdServicoManutencao);
            
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }
        

        // Teste para verificar se a exclusão de um item do estoque de caminhões retorna OK.
        [Test, Order(3)]
        public void DeveRetornarOkAoDeletarEstoqueCaminhao()
        {
            var resultado = sutEstoqueCaminhao.DeleteEstoqueCaminhao(UnitTestEstoqueCaminhoes.IdEstoqueCaminhao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var OkObjectResult = resultado as OkObjectResult;
            Assert.AreEqual(200, OkObjectResult?.StatusCode);
        }
        
        // Teste para verificar se a exclusão de um item do estoque de peças retorna OK.
        [Test, Order(4)]
        public void DeveRetornarOkAoDeletarEstoquePeca()
        {
            var resultado = sutEstoquePeca.DeleteEstoquePeca(UnitTestEstoquePeca.IdEstoquePeca);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var OkObjectResult = resultado as OkObjectResult;
            Assert.AreEqual(200, OkObjectResult?.StatusCode);
        }

        // Teste para verificar se a exclusão de um caminhão retorna OK.
        [Test, Order(5)]
        public void DeveRetornarOkAoDeletarCaminhao()
        {
            var resultado = sutCaminhao.DeleteCaminhao(UnitTestCaminhoes.IdCaminhao);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var okResult = resultado as OkResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }

        // Teste para verificar se a exclusão de um modelo de caminhão retorna OK.
        [Test, Order(6)]
        public void DeveRetornarOkAoDeletarModeloCaminhao()
        {
            var resultado = sutModeloCaminhao.DeleteModeloCaminhao(UnitTestModeloCaminhoes.IdModeloCaminhoes);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var OkResult = resultado as OkResult;
            Assert.AreEqual(200, OkResult?.StatusCode);
        }

        // Teste para verificar se a exclusão de um tipo de peça retorna OK.
        [Test, Order(7)]
        public void DeveRetornarOkAoDeletarTipoPeca()
        {
            var resultado = sutTipoPeca.DeleteTiposPeca(UnitTestTipoPeca.IdTipoPecas);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var OkResult = resultado as OkResult;
            Assert.AreEqual(200, OkResult?.StatusCode);
        }

        // Teste para verificar se a exclusão de um cliente retorna OK.
        [Test, Order(8)]
        public void DeveRetornarOkAoDeletarCliente()
        {
            var resultado = sutClientes.DeleteCliente(UnitTestClientes.docIdentificadorCliente);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }

        // Teste para verificar se a exclusão de um funcionário retorna OK.
        [Test, Order(9)]
        public void DeveRetornarOkAoDeletarFuncionario()
        {
            var resultado = sutFuncionarios.DeleteFuncionario(UnitTestFuncionarios.cpfFuncionario);

            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkObjectResult>(resultado);

            var okResult = resultado as OkObjectResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }

        // Teste para verificar se a exclusão de um cargo retorna OK.
        [Test, Order(10)]
        public void DeveRetornarOkAoDeletarCargo()
        {
            var resultado = sutCargos.DeleteCargo(UnitTestCargos.IdCargo) as OkResult;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }

        // Teste para verificar se a exclusão de uma concessionária retorna o status code 200.
        [Test, Order(11)]
        public void DeveRetornarStatusCode200ParaDeletarConcessionaria()
        {
            var resultado = sutConcessionaria.DeleteConcessionaria(UnitTestConcessionarias.cepConcessionaria);
            
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<OkResult>(resultado);

            var okResult = resultado as OkResult;
            Assert.AreEqual(200, okResult?.StatusCode);
        }
    }
}
