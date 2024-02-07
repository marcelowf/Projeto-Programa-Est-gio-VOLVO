using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas ao estoque de caminhões.
    [Route("[controller]")]
    [ApiController]
    public class EstoqueCaminhoesController : Controller
    {
        // Método para cadastrar um novo caminhão no estoque.
        [HttpPost("Cadastrar")]
        public IActionResult PostEstoqueCaminhao([FromForm] CaminhaoEstoque caminhaoEstoque)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Define o código do caminhão como 0 para que seja gerado automaticamente.
                        caminhaoEstoque.CodCaminhaoEstoque = 0;
                        // Valida os dados do caminhão.
                        ValidationHelper.CheckUniqueChassi(_context, caminhaoEstoque.CodChassiEstoque);
                        ValidationHelper.ValidateAlphaNumericFormat(caminhaoEstoque.CodChassiEstoque, "Código do chassi inválido.");
                        ValidationHelper.ValidateAlphaFormat(caminhaoEstoque.CorEstoqueCaminhao, "Cor do caminhão inválida.");
                        // Adiciona o caminhão ao estoque.
                        _context.EstoqueCaminhao.Add(caminhaoEstoque);
                        // Salva as mudanças no banco de dados.
                        _context.SaveChanges();
                        // Confirma a transação.
                        transaction.Commit();
                        return Ok("O caminhão foi adicionado ao estoque.");
                    }
                    catch (Exception)
                    {
                        // Em caso de erro, realiza o rollback da transação e lança a exceção.
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Método para listar todos os caminhões no estoque.
        [HttpGet("ListarEstoque")]
        public List<CaminhaoEstoque> GetTodosEstoqueCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.EstoqueCaminhao.ToList();
            }
        }

        // Método para buscar um caminhão no estoque pelo código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetEstoqueCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum modelo de caminhão registrado possui esse código.");
                }
                return new ObjectResult(item);
            }
        }

        // Método para desativar um caminhão no estoque.
        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteEstoqueCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum estoque registrado possui esse código.");
                }
                try
                {
                    // Define o caminhão como inativo e salva as mudanças no banco de dados.
                    item.CaminhaoEstoqueAtivo = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para excluir um caminhão do estoque.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteEstoqueCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoqueCaminhao.FirstOrDefault(t => t.CodCaminhaoEstoque == Codigo);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum modelo de caminhão registrado possui esse código.");
                }
                try
                {
                    // Registra a exclusão do caminhão do estoque e remove-o do contexto.
                    ManipulacaoDadosHelper.RegistrarDelete("EstoqueCaminhao", "CaminhaoEstoque", item);
                    _context.EstoqueCaminhao.Remove(item);
                    _context.SaveChanges();
                    return Ok("O caminhão foi removido do estoque com sucesso.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
