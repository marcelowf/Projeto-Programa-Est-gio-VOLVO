using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas a serviços de manutenção.
    [Route("[controller]")]
    [ApiController]
    public class ServicoManutencaoController : Controller
    {
        // Método para cadastrar um novo serviço de manutenção.
        [HttpPost("Cadastrar")]
        public IActionResult PostServicoManutencao([FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var caminhao = _context.Caminhoes.FirstOrDefault(c => c.CodCaminhao == servicoManutencao.FkCaminhoesCodCaminhao);
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        caminhao.Quilometragem = servicoManutencao.QuilometragemCaminhao;
                        servicoManutencao.CodManutencao = 0;
                        _context.ServicosManutencao.Add(servicoManutencao);
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("O servico foi registrado com sucesso.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Método para cadastrar uma peça usada em um serviço de manutenção.
        [HttpPost("CadastrarPecaUsada/{CodigoManutencao}/{CodigoPeca}")]
        public IActionResult PostServicoTipoPeca([FromForm] ServicoTipoPeca servicoTipoPeca)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        servicoTipoPeca.CodServicoTipoPeca = 0;
                        var pecaEstoque = _context.EstoquePecas.FirstOrDefault(c => c.FkTiposPecaCodTipoPeca == servicoTipoPeca.FkTiposPecaCodTipoPeca);
                        if (pecaEstoque == null)
                        {
                            throw new FKNotFoundException("A concessionaria nao possui estoque desse tipo de peca no momento.");
                        }
                        _context.EstoquePecas.Remove(pecaEstoque);
                        _context.ServicoTiposPeca.Add(servicoTipoPeca);
                        _context.SaveChanges();
                        transaction.Commit();
                        return Ok("A peca foi registrada no servico e removida do estoque");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Método para obter todos os serviços de manutenção cadastrados.
        [HttpGet("Listar")]
        public List<ServicoManutencao> GetTodosServicosManutencao()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.ServicosManutencao.ToList();
            }
        }

        // Método para buscar um serviço de manutenção pelo seu código.
        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetServicoManutencao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                return new ObjectResult(item);
            }
        }

        // Método para obter as peças utilizadas em um serviço de manutenção.
        [HttpGet("Pecas/byServicoID/{Codigo}")]
        public List<int> GetPecasServicoManutencao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var servico = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);
                if (servico == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                return _context.ServicoTiposPeca
                    .Where(s => s.FkCodManutencao == servico.CodManutencao)
                    .Select(s => s.FkTiposPecaCodTipoPeca)
                    .ToList();
            }
        }

        // Método para atualizar os dados de um serviço de manutenção.
        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutServicoManutencao(int Codigo, [FromForm] ServicoManutencao servicoManutencao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                item.DescricaoManutencao = servicoManutencao.DescricaoManutencao;
                _context.SaveChanges();
                return Ok("Os dados do servico foram atualizados com sucesso.");
            }
        }

        // Método para deletar um serviço de manutenção.
        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteServicoManutencao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ServicosManutencao.FirstOrDefault(t => t.CodManutencao == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum Servico registrado possui esse codigo.");
                }
                try
                {
                    ManipulacaoDadosHelper.RegistrarDelete("ServicosManutencao", "ServicoManutencao", item);
                    _context.ServicosManutencao.Remove(item);
                    _context.SaveChanges();
                    return Ok("O Servico de Manutencao foi deletado.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
