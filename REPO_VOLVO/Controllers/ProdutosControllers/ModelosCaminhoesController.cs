using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas a modelos de caminhões.
    [Route("[controller]")]
    [ApiController]
    public class ModelosCaminhoesController : Controller
    {
        // Método para cadastrar um novo modelo de caminhão.
        [HttpPost("Cadastrar")]
        public IActionResult PostModelosCaminhao([FromForm] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                try
                {
                    // Define o código do modelo como 0 e ativo por padrão.
                    modelosCaminhao.CodModelo = 0;
                    modelosCaminhao.ModelosAtivo = true;
                    
                    // Valida o valor do modelo antes de adicionar ao contexto.
                    ValidationHelper.IsValidDouble($"{modelosCaminhao.ValorModeloCaminhao}", "Valor do modelo inválido.");
                    
                    // Adiciona o modelo ao contexto e salva as mudanças no banco de dados.
                    _context.ModelosCaminhoes.Add(modelosCaminhao);
                    _context.SaveChanges();
                    
                    return Ok("O modelo foi registrado com sucesso.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para listar todos os modelos de caminhões.
        [HttpGet("Listar")]
        public List<ModelosCaminhao> GetTodosModelosCaminhoes()
        {
            try
            {
                using (var _context = new TrabalhoVolvoContext())
                {
                    return _context.ModelosCaminhoes.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Método para buscar um modelo de caminhão pelo código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetModelosCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o modelo pelo código.
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                
                // Se não encontrar, retorna NotFound.
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        // Método para atualizar um modelo de caminhão.
        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutModelosCaminhao(int Codigo, [FromForm] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o modelo pelo código.
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum modelo registrado possui esse código.");
                }
                try
                {
                    // Valida o valor do modelo antes de atualizar.
                    ValidationHelper.IsValidDouble($"{modelosCaminhao.ValorModeloCaminhao}", "Valor do modelo inválido.");
                    
                    // Atualiza os dados do modelo e salva as mudanças no banco de dados.
                    item.NomeModelo = modelosCaminhao.NomeModelo;
                    item.ValorModeloCaminhao = modelosCaminhao.ValorModeloCaminhao;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para desativar um modelo de caminhão e todos os caminhões correspondentes no estoque.
        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteModeloCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o modelo pelo código.
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);

                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum modelo registrado possui esse código.");
                }

                try
                {
                    // Obtém todos os caminhões no estoque correspondentes a esse modelo e desativa-os.
                    var EstoqueModelo = _context.EstoqueCaminhao.Where(f => f.FkModelosCaminhoesCodModelo == Codigo);
                    foreach (var estoqueModelo in EstoqueModelo)
                    {
                        estoqueModelo.CaminhaoEstoqueAtivo = false;
                    }

                    // Desativa o modelo e salva as mudanças no banco de dados.
                    item.ModelosAtivo = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para excluir um modelo de caminhão e todos os caminhões correspondentes no estoque.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteModeloCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o modelo pelo código.
                var item = _context.ModelosCaminhoes.FirstOrDefault(c => c.CodModelo == Codigo);

                // Se não encontrar, retorna NotFound.
                if (item == null)
                {
                    return NotFound();
                }

                try
                {
                    // Obtém todos os caminhões no estoque correspondentes a esse modelo e os remove.
                    var EstoqueModelo = _context.EstoqueCaminhao.Where(f => f.FkModelosCaminhoesCodModelo == Codigo);
                    foreach (var estoqueModelo in EstoqueModelo)
                    {
                        ManipulacaoDadosHelper.RegistrarDelete("EstoqueCaminhao", "CaminhaoEstoque", estoqueModelo);
                        _context.EstoqueCaminhao.Remove(estoqueModelo);
                    }
                    
                    // Remove o modelo do contexto e salva as mudanças no banco de dados.
                    ManipulacaoDadosHelper.RegistrarDelete("ModelosCaminhoes", "ModelosCaminhao", item);
                    _context.ModelosCaminhoes.Remove(item);
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
