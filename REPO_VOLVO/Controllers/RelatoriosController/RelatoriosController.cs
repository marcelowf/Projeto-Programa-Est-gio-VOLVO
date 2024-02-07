using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TRABALHO_VOLVO
{
    // Controlador para operações relacionadas a relatórios.
    [Route("[controller]")]
    [ApiController]
    public class RelatoriosController : Controller
    {
        // Método para calcular a média de tempo entre manutenções de um caminhão específico.
        [HttpGet("CalcularMediaTempoEntreManutencoesCaminhao/{CodigoCaminhao}")]
        public IActionResult GetPreverRevisaoCaminhao(int CodigoCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var caminhaoEscolhido = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == CodigoCaminhao);

                // Verifica se o caminhão existe no banco de dados.
                if (caminhaoEscolhido == null)
                {
                    throw new FKNotFoundException("Nenhum Caminhao registrado possui esse codigo.");
                }

                // Calcula a média de tempo entre as manutenções do caminhão selecionado.
                double mediaDias = ManipulacaoDadosHelper.GerarTempoEntreManutencoesCaminhao(_context, CodigoCaminhao);
                try
                {
                    // Retorna uma mensagem com a média de tempo entre as manutenções do caminhão.
                    if (mediaDias > 0)
                    {
                        return Ok($"Media de tempo entre as manutenções do caminhão selecionado é {mediaDias} dias.");
                    }
                    else
                    {
                        return Ok($"Manutenções do caminhão selecionado insuficientes para relatorio.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("GetListaClientesContatarManutencao")]
        public IActionResult GetListaClientesContatarManutencao()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                try
                {
                    //====================================== PARTE 1 =====================================================
                    Dictionary<int, List<double>> modelosTemposInicial = ManipulacaoDadosHelper.GerarModelosTempos(_context);
                    //====================================== PARTE 2 =====================================================
                    Dictionary<int, List<double>> modelosTemposPreenchida = ManipulacaoDadosHelper.PreencherModelosTempos(_context, modelosTemposInicial);
                    //====================================== PARTE 3 =====================================================
                    Dictionary<int, double> modelosMediaTempos = ManipulacaoDadosHelper.CalcularMediaTempoModelosTempos(modelosTemposPreenchida);
                    //====================================== PARTE 4 =====================================================
                    List<Caminhao> caminhoes = _context.Caminhoes.Where(c => c.CaminhaoAtivo == true).ToList();
                    Dictionary<Cliente, List<Caminhao>> clientesContatoCaminhoes = new Dictionary<Cliente, List<Caminhao>>();
                    foreach (Caminhao c in caminhoes)
                    {
                        double tempoDecorrido = ManipulacaoDadosHelper.GetTempoDesdeUltimaManutencao(_context, c);
                        if (tempoDecorrido >= modelosMediaTempos[c.FkModelosCaminhoesCodModelo])
                        {
                            Cliente cliente = _context.Clientes.Find(c.FkClientesCodCliente);
                            if (!clientesContatoCaminhoes.ContainsKey(cliente))
                            {
                                List<Caminhao> caminhoesAtrasados = [c];
                                clientesContatoCaminhoes.Add(cliente, caminhoesAtrasados);
                            }
                            else
                            {
                                clientesContatoCaminhoes[cliente].Add(c);
                            }
                        }
                    }
                    
                    return View(@"Views\ContatarClientesManutencao.cshtml", clientesContatoCaminhoes);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para obter o tempo médio entre as manutenções para cada modelo de caminhão.
        [HttpGet("TempoMedioEntreManutencoesModelos")]
        public Dictionary<int, double> GetTempoMedioEntreManutencoesModelos()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                try
                {
                    // Gera um dicionário com os modelos e uma lista vazia para os tempos entre manutenções.
                    Dictionary<int, List<double>> modelosTemposInicial = ManipulacaoDadosHelper.GerarModelosTempos(_context);
                    
                    // Preenche a lista de tempos entre manutenções para cada modelo.
                    Dictionary<int, List<double>> modelosTemposPreenchida = ManipulacaoDadosHelper.PreencherModelosTempos(_context, modelosTemposInicial);
                    
                    // Calcula a média de tempo entre manutenções para cada modelo.
                    Dictionary<int, double> modelosMediaTempos = ManipulacaoDadosHelper.CalcularMediaTempoModelosTempos(modelosTemposPreenchida);
                    return modelosMediaTempos;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        // Método para listar o estoque de caminhões por concessionária.
        [HttpGet("ListarEstoqueCaminhoesPorConcessionaria/{CodigoConcessionaria}")]
        public IActionResult GetListarModelosConcessionaria(int CodigoConcessionaria)
        {
            try
            {
                // Obtém o estoque de caminhões por concessionária.
                List<object> estoqueCaminhoes = ManipulacaoDadosHelper.GetEstoqueCaminhaoPorConcessionaria(CodigoConcessionaria);
                return Ok($"{string.Join(",", estoqueCaminhoes)}.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Método para listar o estoque de peças por concessionária.
        [HttpGet("ListarEstoquePecasPorConcessionaria/{CodigoConcessionaria}")]
        public IActionResult GetListarEstoquePecaPorConcessionaria(int CodigoConcessionaria)
        {
            try
            {
                // Obtém o estoque de peças por concessionária.
                List<object> listaPecas = ManipulacaoDadosHelper.GetEstoquePecaPorConcesiionaria(CodigoConcessionaria);
                return Ok($"{string.Join(",", listaPecas)}.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
