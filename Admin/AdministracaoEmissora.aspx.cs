using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;
using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Servicos;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class AdministracaoEmissora : System.Web.UI.Page
    {
        private static ServicoEmissoraSegmento servicoEmissoraSegmento = new ServicoEmissoraSegmento();

        private class EmissoraSegmentoApresentacao
        {
            public EmissoraSegmento EmissoraSegmento { get; set; }
            public bool EstaAssociada { get; set; }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPracas();
                CarregarVeiculos();
                CarregarEmissoras();
            }

            CarregaTabSelecionada();
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            int emissoraId;

            if (Int32.TryParse(ddlEmissora.SelectedValue, out emissoraId))
            {
                Emissora emissoraEditavel = FabricaDeRepositorio.Emissoras().ConsultarPorId(emissoraId);
                emissoraEditavel.Ativo = !emissoraEditavel.Ativo;

                FabricaDeRepositorio.Emissoras().AlterarStatusAtivo(emissoraEditavel);

                CarregarEmissoras();

                ResetarListaEmissoraSegmentoEdicao();
            }
        }

        protected void btnAplicarAssociacao_Click(object sender, EventArgs e)
        {
            int idEmissoraSelecionada = Int32.Parse(ddlEmissora.SelectedValue);

            if (idEmissoraSelecionada > 0)
            {
                foreach (RepeaterItem item in rptEmissoras.Items)
                {
                    int idSegmento;

                    if (Int32.TryParse(((HiddenField)item.FindControl("hdnSegmentoId")).Value, out idSegmento))
                    {
                        EmissoraSegmento emissoraSegmento = new EmissoraSegmento();
                        emissoraSegmento.Emissora = new Emissora() { Id = idEmissoraSelecionada };
                        emissoraSegmento.Segmento = new Segmento() { Id = idSegmento };

                        if (!((CheckBox)item.FindControl("cbxAssociado")).Checked)
                        {
                            emissoraSegmento.Ativo = false;
                            servicoEmissoraSegmento.Atualizar(emissoraSegmento);
                        }

                        if (((CheckBox)item.FindControl("cbxAssociado")).Checked)
                        {
                            emissoraSegmento.Ativo = true;
                            servicoEmissoraSegmento.InserirAtualizar(emissoraSegmento);
                        }
                    }
                }
            }

            CarregarEmissoraSegmentos(idEmissoraSelecionada);
        }

        protected void ddlEmissora_Changed(object sender, EventArgs e)
        {
            int emissoraId = int.Parse(ddlEmissora.SelectedValue);

            CarregaReplicadoras(emissoraId);

            if (emissoraId > 0)
                CarregarEmissoraSegmentos(emissoraId);

            ResetarListaEmissoraSegmentoEdicao();
        }

        private void CarregarPracas()
        {
            List<Praca> pracas = FabricaDeRepositorio.Pracas().ListarTodos().OrderBy(x => x.Nome).ToList();
            pracas.Insert(0, new Praca() { Id = 0, Nome = String.Empty });

            ddlPraca.DataSource = pracas;
            ddlPraca.DataTextField = "Nome";
            ddlPraca.DataValueField = "Id";

            ddlPraca.DataBind();
        }

        private void CarregarVeiculos()
        {
            List<Veiculo> veiculos = FabricaDeRepositorio.Veiculos().ListarTodosAtivos();
            foreach (Veiculo veiculo in veiculos) veiculo.Nome = String.Format("{0} - {1}", veiculo.Nome, veiculo.IdExterno);

            veiculos = veiculos.OrderBy(x => x.Nome).ToList();

            veiculos.Insert(0, new Veiculo() { Id = 0, Nome = String.Empty });

            ddlVeiculo.DataSource = veiculos;
            ddlVeiculo.DataTextField = "Nome";
            ddlVeiculo.DataValueField = "id";

            ddlVeiculo.DataBind();
        }

        private void CarregarEmissoras()
        {
            List<Emissora> emissoras = FabricaDeRepositorio.Emissoras().ListarTodos().OrderBy(x => x.Praca.Nome).ToList();
            foreach (Emissora emissora in emissoras)
                emissora.Nome = string.Format("{0} [{1}], Emissora:{2} Veículo:{3} ", 
                    emissora.Nome, 
                    emissora.EstaValida() ? "ATIVO" : "INATIVO",
                    emissora.EstaAtiva() ? "ATIVO" : "INATIVO",
                    emissora.Veiculo.EAtivo() ? "ATIVO" : "INATIVO");

            emissoras.Insert(0, new Emissora() { Id = 0, Nome = "Escolha uma emissora para editar..." });

            ddlEmissora.DataSource = emissoras;
            ddlEmissora.DataValueField = "Id";
            ddlEmissora.DataTextField = "Nome";
            ddlEmissora.DataBind();
        }

        private void CarregarEmissoraSegmentos(int idEmissora)
        {
            List<Segmento> segmentosAtivos = RecuperarSegmentosAtivos(FabricaDeRepositorio.Segmentos().ListarTodos());

            List<EmissoraSegmento> emissoraSegmentos = FabricaDeRepositorio.EmissoraSegmentos().ListarPorEmissoraValidas(idEmissora);

            Emissora emissoraCorrente = FabricaDeRepositorio.Emissoras().ConsultarPorId(idEmissora);

            rptEmissoras.DataSource = FormatarEmissoraSegmentoParaRepeater(segmentosAtivos, emissoraSegmentos, emissoraCorrente);
            rptEmissoras.DataBind();

            hndIdPracaSelecionada.Value = emissoraCorrente.Praca.Id.ToString();
            hndIdVeiculoSelecionado.Value = emissoraCorrente.Veiculo.Id.ToString();
            hndIdMediaSelecionado.Value = emissoraCorrente.IdMediaDna.ToString();
            hndIdEmissoraSelecionada.Value = emissoraCorrente.Id.ToString();
        }

        private void ResetarListaEmissoraSegmentoEdicao()
        {
            if (Int32.Parse(ddlEmissora.SelectedValue) <= 0)
            {
                pnlEmissoraSegmentos.Visible = false;
                pnlEmissorasReplicadoras.Visible = false;
                btnAplicarAssociacao.Visible = false;
                btnExcluir.Visible = false;
                btnEditar.Visible = false;
            }
            else
            {
                pnlEmissoraSegmentos.Visible = true;
                pnlEmissorasReplicadoras.Visible = true;
                btnAplicarAssociacao.Visible = true;
                btnExcluir.Visible = true;
                btnEditar.Visible = true;
            }
        }

        private void ResetarCamposInclusao()
        {
            ddlPraca.SelectedValue = "0";
            ddlVeiculo.SelectedValue = "0";
            txtIdMediaDna.Text = String.Empty;
        }

        private List<Segmento> RecuperarSegmentosAtivos(List<Segmento> segmentosCadastrados)
        {
            List<Segmento> segmentosAtivos = new List<Segmento>();

            foreach (Segmento segmentoCadastrado in segmentosCadastrados)
            {
                if (segmentoCadastrado.EstaAtivo())
                {
                    segmentosAtivos.Add(segmentoCadastrado);
                }
            }

            return segmentosAtivos;
        }

        private List<EmissoraSegmentoApresentacao> FormatarEmissoraSegmentoParaRepeater(List<Segmento> todosSegmentos,
            List<EmissoraSegmento> emissorasSegmentosAssociados, Emissora emissoraCorrente)
        {
            List<EmissoraSegmentoApresentacao> emissoraSegmentos = new List<EmissoraSegmentoApresentacao>();
            EmissoraSegmentoApresentacao emissoraSegmentoApresentacao;
            EmissoraSegmento emissoraSegmentoJaAssociada;

            foreach (Segmento segmento in todosSegmentos)
            {
                emissoraSegmentoApresentacao = new EmissoraSegmentoApresentacao();

                emissoraSegmentoJaAssociada = emissorasSegmentosAssociados.FirstOrDefault(x => x.Segmento.Id == segmento.Id);

                if (emissoraSegmentoJaAssociada == null)
                {
                    EmissoraSegmento emissoraSegmento = new EmissoraSegmento();
                    emissoraSegmento.Emissora = emissoraCorrente;
                    emissoraSegmento.Segmento = segmento;

                    emissoraSegmentoApresentacao.EmissoraSegmento = emissoraSegmento;
                    emissoraSegmentoApresentacao.EstaAssociada = false;
                }
                else
                {
                    emissoraSegmentoApresentacao.EmissoraSegmento = emissoraSegmentoJaAssociada;
                    emissoraSegmentoApresentacao.EstaAssociada = true;                    
                }

                emissoraSegmentos.Add(emissoraSegmentoApresentacao);
            }

            return emissoraSegmentos;
        }

        private static Emissora FormatarEmissora(int idPraca, int idVeiculo, int idMediaDna, int idEmissora)
        {
            Praca praca = new Praca()
            {
                Id = idPraca
            };

            Veiculo veiculo = new Veiculo()
            {
                Id = idVeiculo
            };

            Emissora emissora = new Emissora()
            {
                Id = idEmissora,
                IdMediaDna = idMediaDna,
                Praca = praca,
                Veiculo = veiculo
            };

            return emissora;
        }

        [WebMethod]
        public static ArrayList ProcessarEmissora(int pracaId, int veiculoId, int mediaDnaId, int emissoraId)
        {
            List<bool> validacoes = new List<bool>() { true, true, true };

            if (pracaId <= 0)
                validacoes[0] = false;

            if (veiculoId <= 0)
                validacoes[1] = false;

            if (mediaDnaId <= 0)
                validacoes[2] = false;

            if (!validacoes.Contains<bool>(false))
            {
                Emissora emissora = FormatarEmissora(pracaId, veiculoId, mediaDnaId, emissoraId);

                validacoes = ValidarDadosDuplicados(emissora);

                if (!validacoes.Contains<bool>(false))
                    if (emissoraId == 0)
                        FabricaDeRepositorio.Emissoras().InserirERecuperarId(emissora);
                    else
                        FabricaDeRepositorio.Emissoras().Atualizar(emissora);
            }

            ArrayList retorno = new ArrayList();

            foreach (bool validacao in validacoes)
                retorno.Add(validacao);

            return retorno;
        }

        private static List<bool> ValidarDadosDuplicados(Emissora emissora)
        {
            List<bool> validacoes = new List<bool>() { true, true, true };

            List<Emissora> emissorasEncontradas = FabricaDeRepositorio.Emissoras().ListarPorIdMediaDnaOuPracaEVeiculo(emissora.Praca.Id, emissora.Veiculo.Id, emissora.IdMediaDna);

            if (emissora.Id > 0)
                emissorasEncontradas.Remove(FabricaDeRepositorio.Emissoras().ConsultarPorId(emissora.Id));

            if (emissorasEncontradas.Count > 0)
            {
                if (emissorasEncontradas.FirstOrDefault(x => x.Praca.Id == emissora.Praca.Id && x.Veiculo.Id == emissora.Veiculo.Id) != null)
                {
                    validacoes[0] = false;
                    validacoes[1] = false;
                }

                if (emissorasEncontradas.FirstOrDefault(x => x.IdMediaDna == emissora.IdMediaDna) != null)
                    validacoes[2] = false;
            }

            return validacoes;
        }

        protected void btnVincular_Click(object sender, EventArgs e)
        {
            if (ValidaVinculoReplicadoras())
            {
                int emissoraId = int.Parse(ddlEmissora.SelectedValue);
                int emissoraReplicadoraId = int.Parse(ddlEmissorasReplicadoras.SelectedValue);

                FabricaDeRepositorio.EmissorasReplicadoras().Vincular(emissoraId, emissoraReplicadoraId);

                CarregaReplicadoras(emissoraId);
            }
        }

        protected void rptEmissorasReplicadoras_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Desvincular")
                throw new Exception("Comando desconhecido.");

            int emissoraId = int.Parse(ddlEmissora.SelectedValue);
            int emissoraReplicadoraId = int.Parse(((HiddenField)e.Item.FindControl("hdnRptReplicadoraId")).Value);

            FabricaDeRepositorio.EmissorasReplicadoras().Desvincular(emissoraId, emissoraReplicadoraId);

            CarregaReplicadoras(emissoraId);
        }

        private void CarregaReplicadoras(int emissoraId)
        {
            rptEmissorasReplicadoras.DataSource = FabricaDeRepositorio.EmissorasReplicadoras().ListarPorEmissora(emissoraId).OrderBy(x => x.Nome).ToList();
            rptEmissorasReplicadoras.DataBind();

            List<EmissoraReplicadora> replicadoras = FabricaDeRepositorio.EmissorasReplicadoras().ListarTodas().OrderBy(x => x.Nome).ToList();
            replicadoras.Insert(0, new EmissoraReplicadora());

            ddlEmissorasReplicadoras.DataSource = replicadoras;
            ddlEmissorasReplicadoras.DataTextField = "Nome";
            ddlEmissorasReplicadoras.DataValueField = "Id";
            ddlEmissorasReplicadoras.DataBind();
        }

        public void CarregaTabSelecionada()
        {
            //string hiddenFieldValue = hdnTabSelecionada.Value == string.Empty ? "0" : hdnTabSelecionada.Value;
            //string script = string.Format("<script type='text/javascript'>var tabSelecionada = {0};</script>", hiddenFieldValue);
            //this.Header.Controls.Add(new LiteralControl(script));
        }

        private bool ValidaVinculoReplicadoras()
        {
            string mensagemErro = string.Empty;
            int emissoraId = int.Parse(ddlEmissora.SelectedValue);
            int emissoraReplicadoraId = int.Parse(ddlEmissorasReplicadoras.SelectedValue);

            if (emissoraId == default(int) || emissoraReplicadoraId == default(int))
            {
                mensagemErro += "Preencha os campos:<br />";

                if (emissoraId == default(int))
                    mensagemErro += "<b> - Emissora</b><br />";

                if (emissoraReplicadoraId == default(int))
                    mensagemErro += "<b> - Emissora Replicadora</b><br />";
            }

            if (string.IsNullOrEmpty(mensagemErro))
                if (FabricaDeRepositorio.EmissorasReplicadoras().VerificaSeExisteVinculo(emissoraId, emissoraReplicadoraId))
                    mensagemErro += "Emissora Replicadora já vinculada";

            if (string.IsNullOrEmpty(mensagemErro))
                return true;
            else
            {
                WebUtilitarios.Util.ExibirMensagem(mensagemErro, Page);
                return false;
            }
        }
    }
}
