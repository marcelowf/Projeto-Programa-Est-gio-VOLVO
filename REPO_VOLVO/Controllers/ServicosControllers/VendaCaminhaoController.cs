using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas a vendas de caminhões.
    [Route("[controller]")]
    [ApiController]
    public class VendaCaminhaoController : Controller
    {
        // Método para cadastrar uma nova venda de caminhão.
        [HttpPost("Cadastrar")]
        public IActionResult PostVendaCaminhao([FromForm] VendaCaminhao vendaCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Valida a venda de caminhão antes de proceder com o cadastro.
                ValidationHelper.ValidatePostVenda(vendaCaminhao, _context);
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Busca o caminhão no estoque pelo seu código.
                        var estoqueCaminhao = _context.EstoqueCaminhao.FirstOrDefault(c => c.CodCaminhaoEstoque == vendaCaminhao.FkEstoqueCaminhoesCodCaminhaoEstoque);
                        
                        // Busca o modelo do caminhão para obter seu valor.
                        var modelo = _context.ModelosCaminhoes.FirstOrDefault(c => c.CodModelo == estoqueCaminhao.FkModelosCaminhoesCodModelo);
                        
                        // Cria um novo caminhão com os dados do estoque.
                        var novoCaminhao = new Caminhao
                        {
                            CodCaminhao = 0,
                            Quilometragem = 0,
                            PlacaCaminhao = "Undefined",
                            CorCaminhao = estoqueCaminhao.CorEstoqueCaminhao,
                            CodChassiCaminhao = estoqueCaminhao.CodChassiEstoque,
                            DataFabricacao = estoqueCaminhao.DataFabricacao,
                            FkModelosCaminhoesCodModelo = estoqueCaminhao.FkModelosCaminhoesCodModelo,
                            FkClientesCodCliente = vendaCaminhao.FkClientesCodCliente
                        };
                        
                        // Define o código da venda como 0 para que o banco atribua automaticamente.
                        vendaCaminhao.CodVenda = 0;
                        vendaCaminhao.ValorVenda = modelo.ValorModeloCaminhao;
                        
                        // Adiciona a venda e o novo caminhão ao contexto.
                        _context.VendaCaminhoes.Add(vendaCaminhao);
                        _context.Caminhoes.Add(novoCaminhao);
                        
                        // Desativa o caminhão no estoque.
                        estoqueCaminhao.CaminhaoEstoqueAtivo = false;
                        
                        // Salva as mudanças no banco de dados.
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("Venda registrada com sucesso.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        // Filtra exceções específicas e lança uma exceção genérica.
                        ValidationHelper.FilterExceptionsPostVendaCaminhao(e.InnerException.Message);
                        throw new NotImplementedException();
                    }
                }
            }
        }

        // Método para obter todas as vendas de caminhões cadastradas.
        [HttpGet("Listar")]
        public List<VendaCaminhao> GetTodasVendaCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.VendaCaminhoes.ToList();
            }
        }

        // Método para buscar uma venda de caminhão pelo seu código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetVendaCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.VendaCaminhoes.FirstOrDefault(t => t.CodVenda == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma venda registrada possui esse codigo");
                }
                return new ObjectResult(item);
            }
        }

        // Método para deletar uma venda de caminhão pelo seu código.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteVendaCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.VendaCaminhoes.FirstOrDefault(t => t.CodVenda == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma venda registrada possui esse codigo");
                }
                try
                {
                    // Registra o delete da venda.
                    ManipulacaoDadosHelper.RegistrarDelete("VendaCaminhoes", "VendaCaminhao", item);
                    
                    // Remove a venda do contexto.
                    _context.VendaCaminhoes.Remove(item);
                    
                    // Salva as mudanças no banco de dados.
                    _context.SaveChanges();
                    return Ok("A Venda foi deletada.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
