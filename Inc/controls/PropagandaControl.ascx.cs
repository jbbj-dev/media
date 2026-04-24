using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Utilitarios;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ibope.MediaPricing.Web.Admin.controls
{
    public partial class PropagandaControl : System.Web.UI.UserControl
    {
        public Propaganda propaganda;

        public string DescricaoStatusPropaganda { get; set; }

        public void AtribuirPropagandaConferencia(Propaganda propaganda)
        {
            AtribuirBasePropaganda(propaganda);
            ucVarejoAutoComplete.Inicializar(propaganda.Varejos, false);
            txtComentarioColeta.Enabled = false;
            txtDataValidadeInicio.Enabled = false;
            txtDataValidadeFim.Enabled = false;
        }

        public void AtribuirPropagandaColeta(Propaganda propaganda)
        {
            AtribuirBasePropaganda(propaganda);
            ucVarejoAutoComplete.Inicializar(propaganda.Varejos, true);
            txtComentarioConferencia.Enabled = false;
        }

        public void AtribuirPropagandaTrocaDeVideoManual(Propaganda propaganda)
        {
            AtribuirBasePropaganda(propaganda);

            ucVarejoAutoComplete.Inicializar(propaganda.Varejos, false);
            txtComentarioColeta.Enabled = false;
            txtComentarioConferencia.Enabled = false;
            txtDataValidadeInicio.Enabled = false;
            txtDataValidadeFim.Enabled = false;
        }

        public string MontarCaminhoVideo()
        {
            return this.video.Value;
        }

        public bool VarejoSelecionado()
        {
            return ucVarejoAutoComplete.VarejoSelecionado();
        }

        public List<Varejo> ObterVarejos()
        {
            return ucVarejoAutoComplete.ObterVarejos();
        }

        public DateTime ObterDataValidadeInicio()
        {
            DateTime dataValidade = default(DateTime);
            DateTime.TryParseExact(txtDataValidadeInicio.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dataValidade);

            return dataValidade;
        }

        public DateTime ObterDataValidadeFim()
        {
            DateTime dataValidade = default(DateTime);
            DateTime.TryParseExact(txtDataValidadeFim.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out dataValidade);

            return dataValidade;
        }

        public DateTime ObterDataVeiculacao()
        {
            DateTime dataValidade = default(DateTime);
            DateTime.TryParseExact(txtDataVeiculacao.Text, "dd/MM/yyyy - HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.None, out dataValidade);

            return dataValidade;
        }

        public string ObterComentarioColeta()
        {
            return UtilitariosString.LimpaCaracteres(txtComentarioColeta.Text.Trim());
        }

        public string ObterComentarioConferencia()
        {
            return UtilitariosString.LimpaCaracteres(txtComentarioConferencia.Text.Trim());
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.lblStatusPropaganda.Text = this.DescricaoStatusPropaganda;
        }

        private void AtribuirBasePropaganda(Propaganda propaganda)
        {
            if (propaganda == null || propaganda.Id == default(int))
                return;

            this.propaganda = propaganda;
            this.video.Value = $"{propaganda.UrlVideo()}?t={DateTime.Now.Ticks}";
            txtId.Text = propaganda.VideoId.ToString();
            txtDataVeiculacao.Text = propaganda.DataVeiculacao.ToString("dd/MM/yyyy - HH:mm");
            txtDataCriacaoExecucao.Text = propaganda.DataCriacao.ToString("dd/MM/yyyy - HH:mm");
            txtDataValidadeInicio.Text = propaganda.DataValidadeInicio.ToString("dd/MM/yyyy");
            txtDataValidadeFim.Text = propaganda.DataValidadeFim.ToString("dd/MM/yyyy");
            txtPracaVeiculo.Text = propaganda.EmissoraSegmento.Emissora.FormatarInformacaoPracaVeiculo();
            txtSetor.Text = propaganda.EmissoraSegmento.Segmento.Setor;
            txtCategoria.Text = propaganda.EmissoraSegmento.Segmento.Categoria;
            txtItem.Text = propaganda.EmissoraSegmento.Segmento.Item;
            txtAnunciante.Text = propaganda.Anunciante.Nome;
            txtComentarioColeta.Text = propaganda.ComentarioColeta;
            txtComentarioConferencia.Text = propaganda.ComentarioConferencia;
        }
    }
}