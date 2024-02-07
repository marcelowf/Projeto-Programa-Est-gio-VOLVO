using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas a tipos de peças.
    [Route("[controller]")]
    [ApiController]
    public class TiposPecasController : Controller
    {
        // Método para cadastrar um novo tipo de peça.
        [HttpPost("Cadastrar")]
        public IActionResult PostTiposPeca([FromForm] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Define o código do tipo de peça como 0 e ativa por padrão.
                tipoPeca.CodTipoPeca = 0;
                tipoPeca.PecaAtivo = true;
                
                // Adiciona o tipo de peça ao contexto e salva as mudanças no banco de dados.
                _context.TiposPeca.Add(tipoPeca);
                _context.SaveChanges();
                
                return Ok("Tipo de peça cadastrado com sucesso.");
            }
        }

        // Método para listar todos os tipos de peças.
        [HttpGet("Listar")]
        public List<TipoPeca> GetTodosTiposPecas()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.TiposPeca.ToList();
            }
        }

        // Método para buscar um tipo de peça pelo código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetTiposPeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o tipo de peça pelo código.
                var item = _context.TiposPeca.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                
                // Se não encontrar, retorna NotFound.
                if (item == null)
                {
                    return NotFound();
                }
                
                return new ObjectResult(item);
            }
        }

        // Método para atualizar um tipo de peça.
        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutTiposPeca(int Codigo, [FromForm] TipoPeca tipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o tipo de peça pelo código.
                var item = _context.TiposPeca.FirstOrDefault(t => t.CodTipoPeca == Codigo);
                
                // Se não encontrar, retorna NotFound.
                if (item == null)
                {
                    return NotFound();
                }
                
                // Atualiza os dados do tipo de peça e salva as mudanças no banco de dados.
                item.NomeTipoPeca = tipoPeca.NomeTipoPeca;
                item.ValorTipoPeca = tipoPeca.ValorTipoPeca;
                _context.SaveChanges();
                return Ok();
            }
        }

        // Método para desativar um tipo de peça e todas as suas peças correspondentes no estoque.
        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteTiposPeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o tipo de peça pelo código.
                var item = _context.TiposPeca.FirstOrDefault(t => t.CodTipoPeca == Codigo);

                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma Peca registrado possui esse Codigo.");
                }

                try
                {
                    // Obtém todas as peças no estoque correspondentes a esse tipo de peça e desativa-as.
                    var EstoquePeca = _context.EstoquePecas.Where(f => f.FkTiposPecaCodTipoPeca == Codigo);
                    foreach (var estoquePeca in EstoquePeca)
                    {
                        estoquePeca.PecaEstoqueAtiva = false;
                    }
                    
                    // Desativa o tipo de peça e salva as mudanças no banco de dados.
                    item.PecaAtivo = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para excluir um tipo de peça e todas as suas peças correspondentes no estoque.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteTiposPeca(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o tipo de peça pelo código.
                var item = _context.TiposPeca.FirstOrDefault(c => c.CodTipoPeca == Codigo);

                // Se não encontrar, retorna NotFound.
                if (item == null)
                {
                    return NotFound();
                }

                try
                {
                    // Obtém todas as peças no estoque correspondentes a esse tipo de peça e as remove.
                    var EstoquePeca = _context.EstoquePecas.Where(f => f.FkTiposPecaCodTipoPeca == Codigo);
                    foreach (var estoquePeca in EstoquePeca)
                    {
                        ManipulacaoDadosHelper.RegistrarDelete("EstoquePecas", "PecaEstoque", item);
                        _context.EstoquePecas.Remove(estoquePeca);
                    }
                    
                    // Remove o tipo de peça do contexto e salva as mudanças no banco de dados.
                    ManipulacaoDadosHelper.RegistrarDelete("TiposPeca", "TipoPeca", item);
                    _context.TiposPeca.Remove(item);
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
