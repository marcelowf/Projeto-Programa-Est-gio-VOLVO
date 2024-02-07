using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas aos funcionários.
    [Route("[controller]")]
    [ApiController]
    public class FuncionariosController : Controller
    {
        // Método para cadastrar um novo funcionário.
        [HttpPost("Cadastrar")]
        public IActionResult PostFuncionario([FromForm] Funcionario funcionario)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Define o código do funcionário como 0 para que seja gerado automaticamente.
                funcionario.CodFuncionario = 0;
                // Define o funcionário como ativo.
                funcionario.FuncionarioAtivo = true;
                // Verifica se o CPF do funcionário é único no contexto.
                ValidationHelper.CheckUniqueDoc(_context, funcionario.CpfFuncionario);
                // Valida os formatos dos dados do funcionário.
                ValidationHelper.ValidateNameFormat(funcionario.NomeFuncionario, "Nome inválido.");
                ValidationHelper.ValidateNumericFormat(funcionario.CpfFuncionario, "Formato de CPF inválido.");
                ValidationHelper.ValidateNumericFormat(funcionario.NumeroContatoFuncionario, "Formato de telefone inválido.");
                try
                {
                    // Adiciona o funcionário ao contexto e salva as mudanças no banco de dados.
                    _context.Funcionarios.Add(funcionario);
                    _context.SaveChanges();
                    return Ok("Funcionário cadastrado com sucesso.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para listar todos os funcionários.
        [HttpGet("Listar")]
        public List<Funcionario> GetTodosFuncionarios()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Funcionarios.ToList();
            }
        }

        // Método para buscar um funcionário pelo CPF.
        [HttpGet("Buscar/{Documento}")]
        public IActionResult GetFuncionario(string Documento)
        {
            try
            {
                using (var _context = new TrabalhoVolvoContext())
                {
                    var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);
                    // Se não encontrar, lança uma exceção.
                    if (item == null)
                    {
                        throw new FKNotFoundException("Nenhum funcionário foi encontrado com esse CPF.");
                    }
                    return new ObjectResult(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Método para atualizar os dados de um funcionário pelo CPF.
        [HttpPut("Atualizar/{Documento}")]
        public IActionResult PutFuncionario(string Documento, [FromForm] Funcionario funcionario)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Funcionário foi encontrado com esse CPF.");
                }
                // Verifica se as chaves estrangeiras de concessionária e cargo existem.
                if (!_context.Concessionarias.Any(c => c.CodConc == funcionario.FkConcessionariasCodConc))
                {
                    throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse código.");
                }
                else if (!_context.Cargos.Any(c => c.CodCargo == funcionario.FkCargosCodCargo))
                {
                    throw new FKNotFoundException("Nenhum Cargo registrado possui esse código.");
                }
                // Valida os formatos dos dados do funcionário.
                ValidationHelper.ValidateNameFormat(funcionario.NomeFuncionario, "Nome inválido.");
                ValidationHelper.ValidateNumericFormat(funcionario.NumeroContatoFuncionario, "Formato de telefone inválido.");
                try
                {
                    // Atualiza os dados do funcionário no contexto e salva as mudanças no banco de dados.
                    item.NomeFuncionario = funcionario.NomeFuncionario;
                    item.NumeroContatoFuncionario = funcionario.NumeroContatoFuncionario;
                    _context.SaveChanges();
                    return Ok("Os dados do funcionário foram atualizados.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para desativar um funcionário pelo CPF.
        [HttpPut("Desativar/{Documento}")]
        public IActionResult PutDeleteFuncionario(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);
                // Se não encontrar, retorna um erro 404.
                if (item == null)
                {
                    return NotFound();
                }
                // Define o funcionário como inativo e salva as mudanças no banco de dados.
                item.FuncionarioAtivo = false;
                _context.SaveChanges();
                return Ok("O status do funcionário foi desativado.");
            }
        }

        // Método para deletar um funcionário pelo CPF.
        [HttpDelete("Deletar/{Documento}")]
        public IActionResult DeleteFuncionario(string Documento)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Documento);
                // Se não encontrar, retorna um erro 404.
                if (item == null)
                {
                    return NotFound();
                }
                // Registra a deleção do funcionário.
                ManipulacaoDadosHelper.RegistrarDelete("Funcionarios", "Funcionario", item);
                // Remove o funcionário do contexto e salva as mudanças no banco de dados.
                _context.Funcionarios.Remove(item);
                _context.SaveChanges();
                return Ok("O funcionário foi deletado.");
            }
        }
    }
}
