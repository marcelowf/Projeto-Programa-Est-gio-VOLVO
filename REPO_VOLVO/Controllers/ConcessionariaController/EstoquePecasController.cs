using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas ao estoque de peças.
    [Route("[controller]")]
    [ApiController]
    public class EstoquePecasController : Controller
    {
        // Método para cadastrar uma nova peça no estoque.
        [HttpPost("Cadastrar")]
        public IActionResult PostEstoquePeca([FromForm] PecaEstoque peca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                try
                {
                    // Valida a data de fabricação da peça.
                    ValidationHelper.ValidateDateOnly($"{peca.DataFabricacao}", "Data de fabricação inválida.");
                    // Adiciona a peça ao estoque e salva as mudanças no banco de dados.
                    _context.EstoquePecas.Add(peca);
                    _context.SaveChanges();
                    return Ok("A peça foi adicionada ao estoque.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para listar todas as peças no estoque.
        [HttpGet("Listar")]
        public List<PecaEstoque> GetTodasEstoquePecas()
        {
            try
            {
                using (var _context = new TrabalhoVolvoContext())
                {
                    return _context.EstoquePecas.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Método para buscar uma peça no estoque pelo código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma peça registrada possui esse código.");
                }
                return new ObjectResult(item);
            }
        }

        // Método para desativar uma peça no estoque.
        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum estoque registrado possui esse código.");
                }
                try
                {
                    // Define a peça como inativa e salva as mudanças no banco de dados.
                    item.PecaEstoqueAtiva = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para excluir uma peça do estoque.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteEstoquePeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.EstoquePecas.FirstOrDefault(t => t.CodPecaEstoque == Codigo);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma peça registrada possui esse código.");
                }
                try
                {
                    // Remove a peça do estoque e salva as mudanças no banco de dados.
                    _context.EstoquePecas.Remove(item);
                    _context.SaveChanges();
                    return Ok("A peça foi removida do estoque com sucesso.");
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}
