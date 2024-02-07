using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.IO;

namespace TRABALHO_VOLVO
{
    // Classe utilitária para manipulação de dados.
    public class ManipulacaoDadosHelper
    {
        // Método para registrar exclusões.
        public async static void RegistrarDelete(string tabela, string model, Object objeto)
        {
            using (StreamWriter writer = new StreamWriter(@"logs\DeletesLog.txt", true))
            {
                await writer.WriteLineAsync(JsonConvert.SerializeObject(new
                {
                    DataHora = DateTime.Now.ToString("dd/MM/yy HH:mm:ss"),
                    Por = Environment.MachineName,
                    Model = model,
                    Tabela = tabela,
                    Objeto = objeto
                }));
                writer.Flush();
            }
        }

        // Método para gerar o tempo médio entre manutenções de um caminhão.
        public static double GerarTempoEntreManutencoesCaminhao(TrabalhoVolvoContext context, int Codigo)
        {
            try
            {
                var historicoManutencoes = context.ServicosManutencao
                    .Where(m => m.FkCaminhoesCodCaminhao == Codigo)
                    .OrderBy(m => m.DataManutencao)
                    .ToList();
                double totalDias = 0;
                int numManutencoes = historicoManutencoes.Count;
                if (numManutencoes >= 2)
                {
                    for (int i = 1; i < numManutencoes; i++)
                    {
                        totalDias += (historicoManutencoes[i].DataManutencao - historicoManutencoes[i - 1].DataManutencao).TotalDays;
                    }
                    double mediaDias = numManutencoes > 1 ? totalDias / (numManutencoes - 1) : 0;
                    return Math.Round(mediaDias, 1);
                }
                else { return 0; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Método para gerar um dicionário com os modelos de caminhões e seus tempos médios.
        public static Dictionary<int, List<double>> GerarModelosTempos(TrabalhoVolvoContext context)
        {
            Dictionary<int, List<double>> modelosTempos = new Dictionary<int, List<double>>();
            List<ModelosCaminhao> ListaModelos = context.ModelosCaminhoes.ToList();
            foreach (ModelosCaminhao mc in ListaModelos)
            {
                List<double> values = new List<double>();
                modelosTempos.Add(mc.CodModelo, values);
            }
            return modelosTempos;
        }
        
        // Método para preencher o dicionário de modelos de caminhões e seus tempos médios.
        public static Dictionary<int, List<double>> PreencherModelosTempos(TrabalhoVolvoContext context, Dictionary<int, List<double>> modelosTempos)
        {
            List<ServicoManutencao> ListaServicosManutencao = context.ServicosManutencao.ToList();
            foreach (ServicoManutencao sm in ListaServicosManutencao)
            {
                Caminhao caminhao = context.Caminhoes.FirstOrDefault(c => c.CodCaminhao == sm.FkCaminhoesCodCaminhao);
                List<double> valuesList = modelosTempos[caminhao.FkModelosCaminhoesCodModelo];
                valuesList.Add(GerarTempoEntreManutencoesCaminhao(context, caminhao.CodCaminhao));
            }
            return modelosTempos;
        }

        // Método para calcular a média dos tempos dos modelos de caminhões.
        public static Dictionary<int, double> CalcularMediaTempoModelosTempos(Dictionary<int, List<double>> modelosTempos)
        {
            Dictionary<int, double> modelosMediaTempos = new Dictionary<int, double>();
            foreach (var pair in modelosTempos)
            {
                List<double> Tempos = pair.Value;
                if (Tempos.Count > 0)
                {
                    double mediaTempo = Tempos.Average();
                    modelosMediaTempos.Add(pair.Key, Math.Round(mediaTempo, 1));
                }
                else
                {
                    modelosMediaTempos.Add(pair.Key, 0);
                }
            }
            return modelosMediaTempos;
        }

        public static double GetTempoDesdeUltimaManutencao(TrabalhoVolvoContext context, Caminhao caminhao)
        {
            var historicoManutencoes = context.ServicosManutencao
                    .Where(m => m.FkCaminhoesCodCaminhao == caminhao.CodCaminhao)
                    .OrderBy(m => m.DataManutencao)
                    .ToList();
            if (historicoManutencoes == null || historicoManutencoes.Count() == 0)
            {
                return 0;
            }
            DateTime dataUltimaManutencao = historicoManutencoes[historicoManutencoes.Count() - 1].DataManutencao;

            double tempoDecorrido = (DateTime.Now - dataUltimaManutencao).TotalDays;
            return tempoDecorrido;
        }

        // Método para obter o estoque de caminhões por concessionária.
        public static List<object> GetEstoqueCaminhaoPorConcessionaria(int Codigo)
        {
            try
            {
                using (var context = new TrabalhoVolvoContext())
                {
                    var concessionariaEscolhida = context.Concessionarias.FirstOrDefault(t => t.CodConc == Codigo);

                    if (concessionariaEscolhida == null)
                    {
                        throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                    }

                    var result = context.ModelosCaminhoes
                        .GroupJoin(
                            context.EstoqueCaminhao
                                .Where(ec => ec.FkConcessionariasCodConc == Codigo && ec.CaminhaoEstoqueAtivo),
                            nm => nm.CodModelo,
                            ec => ec.FkModelosCaminhoesCodModelo,
                            (nm, ecGroup) => new
                            {
                                nm.CodModelo,
                                nm.NomeModelo,
                                Qtde_Disponivel = ecGroup.Count()
                            }
                        )
                        .ToList();

                    // Converte o resultado para uma lista de objetos
                    var resultList = result.Select(r => new { r.CodModelo, r.NomeModelo, r.Qtde_Disponivel }).Cast<object>().ToList();

                    if (resultList.Count > 0)
                    {
                        return resultList;
                    }
                    throw new DadosInsuficientesException($"Número de Modelos insuficientes para gerar um relatório da concessionaria {concessionariaEscolhida.NomeConc}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Método para obter o estoque de peças por concessionária.
        public static List<object> GetEstoquePecaPorConcesiionaria(int Codigo)
        {
            try
            {
                using (var context = new TrabalhoVolvoContext())
                {
                    var concessionariaEscolhida = context.Concessionarias.FirstOrDefault(t => t.CodConc == Codigo);

                    if (concessionariaEscolhida == null)
                    {
                        throw new FKNotFoundException("Nenhuma Concessionaria registrada possui esse codigo.");
                    }

                    var result = context.TiposPeca
                        .GroupJoin(
                            context.EstoquePecas
                                .Where(ec => ec.FkConcessionariasCodConc == Codigo),
                            nm => nm.CodTipoPeca,
                            ec => ec.FkTiposPecaCodTipoPeca,
                            (nm, ecGroup) => new
                            {
                                nm.CodTipoPeca,
                                nm.NomeTipoPeca,
                                Qtde_Disponivel = ecGroup.Count()
                            }
                        )
                        .ToList();

                    // Converte o resultado para uma lista de objetos
                    var resultList = result.Select(r => new { r.CodTipoPeca, r.NomeTipoPeca, r.Qtde_Disponivel }).Cast<object>().ToList();

                    if (resultList.Count > 0)
                    {
                        return resultList;
                    }
                    throw new DadosInsuficientesException($"Número de Modelos insuficientes para gerar um relatório da concessionaria {concessionariaEscolhida.NomeConc}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
