using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ibope.MediaPricing.Web.Ajax
{
    public partial class MarcaProdutoAutoComplete : System.Web.UI.UserControl
    {
        public string MarcaId { get; set; }
        public string MarcaNome { get; set; }
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public string ProdutoModelo { get; set; }
        public string ProdutoValorMax { get; set; }
        public string ProdutoValorMin { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Marca
            if (!string.IsNullOrEmpty(MarcaNome))
                txtMarca.Text = MarcaNome;

            if (!string.IsNullOrEmpty(MarcaNome))
                hdnMarcaNome.Value = MarcaNome;

            if (!string.IsNullOrEmpty(MarcaId))
                hdnMarcaId.Value = MarcaId;

            //Produto
            if (!string.IsNullOrEmpty(ProdutoNome))
                txtProduto.Text = "[" + MarcaNome + "] " + "[" + ProdutoNome + "] " + "[" + ProdutoModelo + "]";

            if (!string.IsNullOrEmpty(ProdutoNome))
                hdnProdutoNome.Value = "[" + MarcaNome + "] " + "[" + ProdutoNome + "] " + "[" + ProdutoModelo + "]";

            if (!string.IsNullOrEmpty(ProdutoId))
                hdnProdutoId.Value = ProdutoId;

            if (!string.IsNullOrEmpty(ProdutoValorMax))
                hdnProdutoValorMax.Value = ProdutoValorMax;

            if (!string.IsNullOrEmpty(ProdutoValorMin))
                hdnProdutoValorMin.Value = ProdutoValorMin;
        }

        public int ObterMarcaId()
        {
            int id = default(int);
            if (!String.IsNullOrEmpty(hdnMarcaId.Value))
                id = Convert.ToInt32(hdnMarcaId.Value, CultureInfo.CurrentCulture);

            return id;
        }

        public string ObterMarcaNome()
        {
            string nome = string.Empty;
            if (!String.IsNullOrEmpty(txtMarca.Text))
                nome = txtMarca.Text;

            return nome;
        }

        public int ObterProdutoId()
        {
            int id = default(int);
            if (!String.IsNullOrEmpty(hdnProdutoId.Value))
                id = Convert.ToInt32(hdnProdutoId.Value, CultureInfo.CurrentCulture);

            return id;
        }

        public string ObterProdutoNome()
        {
            string nome = string.Empty;
            if (!String.IsNullOrEmpty(txtProduto.Text))
                nome = txtProduto.Text;

            return nome;
        }

        public bool ValidarMarcaProduto()
        {
            lblValidarMarca.Visible = false;
            lblValidarMarca.Visible = false;
            bool validacao = true;

            if (string.IsNullOrEmpty(txtMarca.Text))
            {
                lblValidarMarca.Visible = true;
                validacao = false;
            }

            if (string.IsNullOrEmpty(txtProduto.Text))
            {
                lblValidarProduto.Visible = true;
                validacao = false;
            }

            return validacao;
        }

        public bool EMarcaNova()
        {
            if (!string.IsNullOrEmpty(txtMarca.Text) && ObterMarcaId() == default(int))
                return true;
            else
                return false;
        }

        public bool EProdutoNovo()
        {
            if (!string.IsNullOrEmpty(txtProduto.Text) && ObterProdutoId() == default(int))
                return true;
            else
                return false;
        }

        public void MarcaNova(bool eMarcaNova)
        {
            if (eMarcaNova)
                lblMarcaNova.Attributes.Add("style", "display:inline;");
            else
                lblMarcaNova.Attributes.Add("style", "display:none;");
        }

        public void ProdutoNovo(bool eProdutoNovo)
        {
            if (eProdutoNovo)
                lblProdutoNovo.Attributes.Add("style", "display:inline;");
            else
                lblProdutoNovo.Attributes.Add("style", "display:none;");
        }
    }
}