using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas às concessionárias.
    [Route("[controller]")]
    [ApiController]
    public class ConcessionariasController : Controller
    {
        // Método para cadastrar uma nova concessionária.
        [HttpPost("Cadastrar")]
        public IActionResult PostConcessionaria([FromForm] Concessionaria concessionaria)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                // Define o código da concessionária como 0 para que seja gerado automaticamente.
                concessionaria.CodConc = 0;
                // Define a concessionária como ativa.
                concessionaria.ConcessionariaAtivo = true;
                // Valida os formatos dos dados da concessionária.
                ValidationHelper.ValidateNameFormat(concessionaria.NomeConc, "Nome inválido.");
                ValidationHelper.ValidateNumericFormat(concessionaria.CepConcessionaria, "Formato do CEP inválido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Pais, "País inválido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Estado, "Estado inválido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Cidade, "Cidade inválida.");
                ValidationHelper.ValidateNameFormat(concessionaria.Rua, "Rua inválida.");
                try
                {
                    // Adiciona a concessionária ao contexto e salva as mudanças no banco de dados.
                    _context.Concessionarias.Add(concessionaria);
                    _context.SaveChanges();
                    return Ok("Concessionária registrada com sucesso.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para listar todas as concessionárias.
        [HttpGet("Listar")]
        public List<Concessionaria> GetTodasConcessionarias()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Concessionarias.ToList();
            }
        }

        // Método para buscar uma concessionária pelo CEP.
        [HttpGet("Buscar/{Cep}")]
        public IActionResult GetConcessionaria(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma concessionária registrada possui esse CEP.");
                }
                return new ObjectResult(item);
            }
        }

        // Método para atualizar os dados de uma concessionária pelo CEP.
        [HttpPut("Atualizar/{Cep}")]
        public IActionResult PutConcessionaria(string Cep, [FromForm] Concessionaria concessionaria)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma concessionária registrada possui esse CEP.");
                }
                // Valida os formatos dos dados da concessionária.
                ValidationHelper.ValidateNameFormat(concessionaria.NomeConc, "Nome inválido.");
                ValidationHelper.ValidateNumericFormat(concessionaria.CepConcessionaria, "Formato do CEP inválido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Pais, "País inválido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Estado, "Estado inválido.");
                ValidationHelper.ValidateAlphaFormat(concessionaria.Cidade, "Cidade inválida.");
                ValidationHelper.ValidateNameFormat(concessionaria.Rua, "Rua inválida.");
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Atualiza os dados da concessionária no contexto e salva as mudanças no banco de dados.
                        item.NomeConc = concessionaria.NomeConc;
                        item.CepConcessionaria = concessionaria.CepConcessionaria;
                        item.Pais = concessionaria.Pais;
                        item.Estado = concessionaria.Estado;
                        item.Cidade = concessionaria.Cidade;
                        item.Rua = concessionaria.Rua;
                        item.Numero = concessionaria.Numero;
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("Os dados da concessionária foram atualizados.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Método para desativar uma concessionária pelo CEP.
        [HttpPut("Desativar/{Cep}")]
        public IActionResult PutDeleteConcessionaria(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma concessionária registrada possui esse CEP.");
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Desativa todos os funcionários associados à concessionária.
                        var funcionariosConcessionaria = _context.Funcionarios.Where(f => f.FkConcessionariasCodConc == item.CodConc);
                        foreach (var funcionario in funcionariosConcessionaria)
                        {
                            funcionario.FuncionarioAtivo = false;
                        }
                        // Desativa todos os estoques de caminhões associados à concessionária.
                        var estoqueCaminhaoConcessionaria = _context.EstoqueCaminhao.Where(f => f.FkConcessionariasCodConc == item.CodConc);
                        foreach (var estoque in estoqueCaminhaoConcessionaria)
                        {
                            estoque.CaminhaoEstoqueAtivo = false;
                        }
                        // Desativa todos os estoques de peças associados à concessionária.
                        var estoquePecaConcessionaria = _context.EstoquePecas.Where(f => f.FkConcessionariasCodConc == item.CodConc);
                        foreach (var estoque in estoqueCaminhaoConcessionaria)
                        {
                            estoque.CaminhaoEstoqueAtivo = false;
                        }
                        // Desativa a concessionária.
                        item.ConcessionariaAtivo = false;
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("A concessionária foi desativada com sucesso.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Método para deletar uma concessionária pelo CEP.
        [HttpDelete("Deletar/{Cep}")]
        public IActionResult DeleteConcessionaria(string Cep)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Concessionarias.FirstOrDefault(t => t.CepConcessionaria == Cep);
                // Se não encontrar, lança uma exceção.
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhuma concessionária registrada possui esse CEP.");
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Remove todos os funcionários associados à concessionária.
                        var funcionariosConcessionaria = _context.Funcionarios.Where(f => f.FkConcessionariasCodConc == item.CodConc);
                        foreach (var funcionario in funcionariosConcessionaria)
                        {
                            _context.Funcionarios.Remove(funcionario);
                        }
                        // Registra a deleção da concessionária.
                        ManipulacaoDadosHelper.RegistrarDelete("Concessionarias", "Concessionaria", item);
                        // Remove a concessionária do contexto e salva as mudanças no banco de dados.
                        _context.Concessionarias.Remove(item);
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
