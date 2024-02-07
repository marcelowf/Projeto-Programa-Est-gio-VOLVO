using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas a caminhões.
    [Route("[controller]")]
    [ApiController]
    public class CaminhoesController : Controller
    {
        // Método para cadastrar um novo caminhão.
        [HttpPost("Cadastrar")]
        public IActionResult PostCaminhao([FromForm] Caminhao caminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Verifica a integridade das FKs fornecidas
                // e lança uma exceção se alguma delas não existir no banco de dados.
                // Se as FKs estiverem corretas, tenta registrar o caminhão no banco de dados.
                try
                {
                    caminhao.CodCaminhao = 0;
                    caminhao.CaminhaoAtivo = true;
                    // Validar os dados do caminhão
                    ValidationHelper.CheckUniqueChassi(_context, caminhao.CodChassiCaminhao);
                    ValidationHelper.ValidateAlphaNumericFormat(caminhao.CodChassiCaminhao, "Codigo chassi invalido.");
                    ValidationHelper.ValidateAlphaFormat(caminhao.CorCaminhao, "Cor de caminhao invalida.");
                    ValidationHelper.ValidateAlphaNumericFormat(caminhao.CodChassiCaminhao, "Placa invalida.");
                    // Feita a validação, tenta adicionar o caminhão ao estoque
                    _context.Caminhoes.Add(caminhao);
                    // Hora de gerar o registro da aquisição
                    _context.SaveChanges();
                    return Ok("O caminhao foi Registrado com sucesso.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para obter todos os caminhões cadastrados.
        [HttpGet("Listar")]
        public List<Caminhao> GetTodosCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Caminhoes.ToList();
            }
        }

        // Método para buscar um caminhão pelo seu código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum caminhao registrado possui esse codigo.");
                }
                return new ObjectResult(item);
            }
        }

        // Método para marcar um caminhão como inativo.
        [HttpPut("Deletar/{Codigo}")]
        public IActionResult PutDeleteCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum caminhao registrado possui esse codigo.");
                }
                item.CaminhaoAtivo = false;
                _context.SaveChanges();
                return Ok();
            }
        }

        // Método para deletar um caminhão.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(c => c.CodCaminhao == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum caminhao registrado possui esse codigo.");
                }
                try
                {
                    ManipulacaoDadosHelper.RegistrarDelete("Caminhoes", "Caminhao", item);
                    _context.Caminhoes.Remove(item);
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
