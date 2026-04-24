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
    public partial class AdministracaoPraca : System.Web.UI.Page
    {
        private const string CONST_EXCLUIR =
            "<a id='btnExcluir' pracaId='{0}' class='botao_azul jqueryPraca' " +
                "href=javascript:__doPostBack('ctl00$MainContent$btnExcluir','')> <span>Excluir</span> <a>";

        private class PracaTableData
        {
            public string Nome { get; set; }
            public string Uf { get; set; }
            public string Excluir { get; set; }
            public int Id { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPracas();
            }

            ResetarInformeDeErros();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (CamposValidados())
            {
                FabricaDeRepositorio.Pracas().InserirERecuperarId(FormatarEntidade());

                LimparCampos();
                CarregarPracas();
                ResetarInformeDeErros();
                ResetarInformacoesPracas();
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (CamposValidados())
            {
                FabricaDeRepositorio.Pracas().Atualizar(FormatarEntidade());

                LimparCampos();
                CarregarPracas();
                btnAdicionar.Visible = true;
                btnEditar.Visible = false;
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            int idPraca = Int32.Parse(hdnIdPraca.Value);

            if (FabricaDeRepositorio.Emissoras().RetornaNumeroDeEmissorasQuePossuemIdDaPraca(idPraca) <= 0)
            {
                FabricaDeRepositorio.Pracas().ExcluirPorId(idPraca);
                CarregarPracas();
            }
            else
                WebUtilitarios.Util.ExibirMensagem("Não é permitida a exclusão, pois a Praça está em uso!", Page);
        }

        protected void rptPracas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int pracaId = int.Parse(((HiddenField)e.Item.FindControl("hdnPracaId")).Value);
            Praca praca = FabricaDeRepositorio.Pracas().ConsultarPorId(pracaId);

            if (e.CommandName == "Editar")
            {
                hdnIdPraca.Value = pracaId.ToString();
                txtNomePraca.Text = praca.Nome;
                txtUfPraca.Text = praca.Uf;

                btnAdicionar.Visible = false;
                btnEditar.Visible = true;
            }
        }

        private Praca FormatarEntidade()
        {
            Praca praca = new Praca();

            praca.Nome = txtNomePraca.Text.Trim();
            praca.Uf = txtUfPraca.Text.Trim().ToUpper();
            praca.Id = string.IsNullOrEmpty(hdnIdPraca.Value) ? 0 : Convert.ToInt32(hdnIdPraca.Value);

            return praca;
        }

        private bool CamposValidados()
        {
            bool validado = true;
            string mensagemErro = "Preencha os campos:<br />";

            if (String.IsNullOrEmpty(txtNomePraca.Text.Trim()))
            {
                validado = false;
                NomeValidacao.Visible = true;
                mensagemErro += "<b> - Nome</b><br />";
            }

            if (String.IsNullOrEmpty(txtUfPraca.Text.Trim()))
            {
                validado = false;
                UfValidacao.Visible = true;
                mensagemErro += "<b> - UF</b><br />";
            }

            if (validado)
            {
                Praca novaPraca = FormatarEntidade();

                List<Praca> pracasSemelhantes = FabricaDeRepositorio.Pracas().ListarPorNomeEUf(novaPraca.Nome, novaPraca.Uf);

                if (pracasSemelhantes.FirstOrDefault(x => x.Nome.Trim().ToUpper() == novaPraca.Nome.Trim().ToUpper()
                                                       && x.Uf.Trim().ToUpper() == novaPraca.Uf.Trim().ToUpper()
                                                       && x.Id != novaPraca.Id) != null)
                {
                    validado = false;
                    NomeValidacao.Visible = true;
                    UfValidacao.Visible = true;
                    mensagemErro = "Praça já cadastrada!";
                }
            }

            if (!validado)
                WebUtilitarios.Util.ExibirMensagem(mensagemErro, Page);

            return validado;
        }

        private void ResetarInformeDeErros()
        {
            NomeValidacao.Visible = false;
            UfValidacao.Visible = false;
        }

        private void ResetarInformacoesPracas()
        {
            txtNomePraca.Text = String.Empty;
            txtUfPraca.Text = String.Empty;
        }

        public void CarregarPracas()
        {
            rptPracas.DataSource = ConverterPracasParaHtmlTable(FabricaDeRepositorio.Pracas().ListarTodos());
            rptPracas.DataBind();
        }

        private List<PracaTableData> ConverterPracasParaHtmlTable(List<Praca> pracasDoRepositorio)
        {
            List<PracaTableData> pracasParaTela = new List<PracaTableData>();
            PracaTableData pracaDeTela;

            foreach (var pracaDoRepositorio in pracasDoRepositorio)
            {
                pracaDeTela = new PracaTableData();
                pracaDeTela.Nome = pracaDoRepositorio.Nome;
                pracaDeTela.Uf = pracaDoRepositorio.Uf;
                pracaDeTela.Excluir = String.Format(CONST_EXCLUIR, pracaDoRepositorio.Id);
                pracaDeTela.Id = pracaDoRepositorio.Id;

                pracasParaTela.Add(pracaDeTela);
            }

            return pracasParaTela;
        }

        private void LimparCampos()
        {
            txtNomePraca.Text = string.Empty;
            txtUfPraca.Text = string.Empty;
            hdnIdPraca.Value = string.Empty;
        }
    }
}
