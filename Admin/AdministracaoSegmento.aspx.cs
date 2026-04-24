using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using Ibope.MediaPricing.Dominio.Enumeradores;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class AdministracaoSegmento : System.Web.UI.Page
    {
        private const string CONST_ATIVAR =
            "<a id='btnAtivarDesativar' segmentoId='{0}' class='botao_azul jquerySegmento' " +
                "href=javascript:__doPostBack('ctl00$MainContent$btnAtivarDesativar','')> <span>Ativar/Inativar</span> <a>";

        private class SegmentoTableData
        {
            public string Nome { get; set; }
            public int IdExterno { get; set; }
            public string Ativar { get; set; }
            public string StatusAtivo { get; set; }
            public bool Ativo { get; set; }
            public int Id { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarSegmentos();
            }

            ResetarInformeDeErros();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (CamposValidados())
            {
                FabricaDeRepositorio.Segmentos().InserirERecuperarId(FormatarEntidade());

                CarregarSegmentos();
                ResetarInformeDeErros();
                ResetarInformacoesSegmentos();
            }
        }

        protected void btnAtivarDesativar_Click(object sender, EventArgs e)
        {
            int idSegmento = Int32.Parse(hdnIdSegmento.Value);
            EntidadeSituacao situacao = RecuperarStatusAtivo(idSegmento);

            FabricaDeRepositorio.Segmentos().AlterarSituacao(idSegmento, situacao);

            CarregarSegmentos();
        }

        private Segmento FormatarEntidade()
        {
            Segmento segmento = new Segmento();
            segmento.IdExterno = Int32.Parse(txtIdExterno.Text);
            segmento.ComporNomePorSetorCategoriaItem(txtSetor.Text, txtCategoria.Text, txtItem.Text);

            return segmento;
        }

        private bool CamposValidados()
        {
            bool validado = true;
            string mensagemErro = "Preencha os campos:<br />";

            if (String.IsNullOrEmpty(txtSetor.Text.Trim()))
            {
                validado = false;
                SetorValidacao.Visible = true;
                mensagemErro += "<b> - Setor</b><br />";
            }

            if (String.IsNullOrEmpty(txtCategoria.Text.Trim()))
            {
                validado = false;
                CategoriaValidacao.Visible = true;
                mensagemErro += "<b> - Categoria</b><br />";
            }

            if (String.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                validado = false;
                ItemValidacao.Visible = true;
                mensagemErro += "<b> - Item</b><br />";
            }

            if (String.IsNullOrEmpty(txtIdExterno.Text.Trim()))
            {
                validado = false;
                idExternoValidacao.Visible = true;
                mensagemErro += "<b> - ID Shopping Brasil</b><br />";
            }
            else
            {
                int idExterno = Int32.MinValue;

                if (!Int32.TryParse(txtIdExterno.Text.Trim(), out idExterno))
                {
                    validado = false;
                    idExternoValidacao.Visible = true;
                    mensagemErro = "O ID Shopping Brasil deve ser apenas numérico!";
                }
            }

            if (validado)
            {
                Segmento novoSegmento = FormatarEntidade();

                List<Segmento> segmentosSemelhantes = 
                    FabricaDeRepositorio.Segmentos().ListarPorNomeOuIdExterno(novoSegmento.Nome, novoSegmento.IdExterno);

                if (segmentosSemelhantes.FirstOrDefault(x => x.IdExterno == novoSegmento.IdExterno) != null)
                {
                    validado = false;
                    idExternoValidacao.Visible = true;
                    mensagemErro = "ID Shopping Brasil já cadastrado! <br/>Tente outro.";
                }

                if (segmentosSemelhantes.FirstOrDefault(x => x.Nome == novoSegmento.Nome) != null)
                {
                    validado = false;
                    SetorValidacao.Visible = true;
                    CategoriaValidacao.Visible = true;
                    ItemValidacao.Visible = true;
                    mensagemErro = "Segmento já cadastrado!";
                }
            }
            
            if (!validado)
                WebUtilitarios.Util.ExibirMensagem(mensagemErro, Page);

            return validado;
        }

        private void ResetarInformeDeErros()
        {
            SetorValidacao.Visible = false;
            CategoriaValidacao.Visible = false;
            ItemValidacao.Visible = false;
            idExternoValidacao.Visible = false;

            
        }

        private void ResetarInformacoesSegmentos()
        {
            txtSetor.Text = String.Empty;
            txtCategoria.Text = String.Empty;
            txtItem.Text = String.Empty;
            txtIdExterno.Text = String.Empty;
        }
        
        public void CarregarSegmentos()
        {
            rptSegmentos.DataSource = ConverterSegmentosParaHtmlTable(FabricaDeRepositorio.Segmentos().ListarTodos());
            rptSegmentos.DataBind();
        }

        private List<SegmentoTableData> ConverterSegmentosParaHtmlTable(List<Segmento> segmentosDoRepositorio)
        {
            List<SegmentoTableData> segmentosParaTela = new List<SegmentoTableData>();
            SegmentoTableData segmentoDeTela;

            foreach (var segmentoDoRepositorio in segmentosDoRepositorio)
            {
                segmentoDeTela = new SegmentoTableData();
                segmentoDeTela.Nome = segmentoDoRepositorio.Nome;
                segmentoDeTela.IdExterno = segmentoDoRepositorio.IdExterno;
                segmentoDeTela.Ativar = String.Format(CONST_ATIVAR, segmentoDoRepositorio.Id);
                segmentoDeTela.StatusAtivo = segmentoDoRepositorio.Situacao.ToString();
                segmentoDeTela.Ativo = segmentoDoRepositorio.Situacao.Equals(EntidadeSituacao.Ativo);
                segmentoDeTela.Id = segmentoDoRepositorio.Id;

                segmentosParaTela.Add(segmentoDeTela);
            }

            return segmentosParaTela;
        }

        private EntidadeSituacao RecuperarStatusAtivo(int idSegmento)
        {
            EntidadeSituacao retorno = EntidadeSituacao.Inativo;

            foreach (RepeaterItem item in rptSegmentos.Items)
            {
                if (Int32.Parse(((Label)item.FindControl("lblIdSegmento")).Text).Equals(idSegmento))
                {
                    retorno = 
                        ((Label)item.FindControl("lblAtivo")).Text.Equals("Ativo") ? EntidadeSituacao.Inativo : EntidadeSituacao.Ativo;
                    break;
                }
            }

            return retorno;
        }
    }
}
