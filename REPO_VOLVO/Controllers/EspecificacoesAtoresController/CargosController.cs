using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas a cargos.
    [Route("[controller]")]
    [ApiController]
    public class CargosController : Controller
    {
        // Método para cadastrar um novo cargo.
        [HttpPost("Cadastrar")]
        public IActionResult PostCargo([FromForm] Cargo cargo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Define o código do cargo como 0 e adiciona ao contexto.
                cargo.CodCargo = 0;
                _context.Cargos.Add(cargo);
                _context.SaveChanges();
                return Ok();
            }
        }

        // Método para listar todos os cargos.
        [HttpGet("Listar")]
        public List<Cargo> GetTodosCargos()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Cargos.ToList();
            }
        }

        // Método para buscar um cargo pelo código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetCargo(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o cargo pelo código.
                var item = _context.Cargos.FirstOrDefault(t => t.CodCargo == Codigo);

                // Se não encontrar, retorna NotFound.
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        // Método para atualizar um cargo.
        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutCargo(int Codigo, [FromForm] Cargo Cargo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o cargo pelo código.
                var item = _context.Cargos.FirstOrDefault(t => t.CodCargo == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                // Atualiza os dados do cargo e salva as mudanças no banco de dados.
                item.NomeCargo = Cargo.NomeCargo;
                item.SalarioBase = Cargo.SalarioBase;
                item.PorcentagemComissao = Cargo.PorcentagemComissao;
                _context.SaveChanges();
                return Ok();
            }
        }

        // Método para desativar um cargo e todos os funcionários associados a ele.
        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteCargo(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o cargo pelo código.
                var item = _context.Cargos.FirstOrDefault(t => t.CodCargo == Codigo);

                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cargo registrado possui esse Código.");
                }
                try
                {
                    // Obtém todos os funcionários associados a esse cargo e os desativa.
                    var funcionariosComCargo = _context.Funcionarios.Where(f => f.FkCargosCodCargo == Codigo);
                    foreach (var funcionario in funcionariosComCargo)
                    {
                        funcionario.FuncionarioAtivo = false;
                    }
                    // Desativa o cargo e salva as mudanças no banco de dados.
                    item.CargoAtivo = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para excluir um cargo e desassociar todos os funcionários associados a ele.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteCargo(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Procura o cargo pelo código.
                var cargoParaExcluir = _context.Cargos.FirstOrDefault(c => c.CodCargo == Codigo);

                // Se não encontrar, retorna NotFound.
                if (cargoParaExcluir == null)
                {
                    return NotFound();
                }

                // Obtém todos os funcionários associados a esse cargo e desassocia-os.
                var funcionariosComCargo = _context.Funcionarios.Where(f => f.FkCargosCodCargo == Codigo);
                foreach (var funcionario in funcionariosComCargo)
                {
                    funcionario.FkCargosCodCargo = null;
                }
                // Remove o cargo do contexto e salva as mudanças no banco de dados.
                ManipulacaoDadosHelper.RegistrarDelete("Cargos", "Cargo", cargoParaExcluir);
                _context.Cargos.Remove(cargoParaExcluir);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
