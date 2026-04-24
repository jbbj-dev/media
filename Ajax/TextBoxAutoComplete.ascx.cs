using System;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Ibope.MediaPricing.Web.Ajax
{
    public partial class TextBoxAutoComplete : System.Web.UI.UserControl
    {
        public bool limparCamposCasoNaoExista { get; set; }
        public string labelAutoComplete { get; set; }
        public string repositorio { get; set; }
        public string NomeCampoExibicao { get; set; }
        public string CampoId { get; set; }
        public string CampoNome { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CampoId))
                AtribuirId(int.Parse(CampoId));

            if (!string.IsNullOrEmpty(CampoNome))
                AtribuirNome(CampoNome);

            if (string.IsNullOrEmpty(labelAutoComplete))
                this.lblAutoComplete.Visible = false;
        }

        public TextBoxAutoComplete()
        {
            if (string.IsNullOrEmpty(NomeCampoExibicao))
                NomeCampoExibicao = "Nome";
        }

        public void PreencherTexto(string texto)
        {
            txtNome.Text = texto;
        }

        public void HabilitarDesabilitarTextBox(bool habilitado)
        {
            txtNome.Enabled = habilitado;
        }

        public int ObterId()
        {
            int id = default(int);
            if (!String.IsNullOrEmpty(hdnId.Value))
                id = Convert.ToInt32(hdnId.Value, CultureInfo.CurrentCulture);

            return id;
        }

        public string ObterNome()
        {
            string nome = string.Empty;
            if (!String.IsNullOrEmpty(txtNome.Text))
                nome = txtNome.Text;

            return nome;
        }

        public void Limpar()
        {
            txtNome.Text = hdnId.Value = hdnNome.Value = "";
        }

        public void AtribuirNome(string nome)
        {
            txtNome.Text = hdnNome.Value = nome;
        }

        public void AtribuirId(int id)
        {
            hdnId.Value = id.ToString();
        }

        public void FocoTextBox()
        {
            txtNome.Focus();
        }
    }
}