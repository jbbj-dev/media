using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Ibope.MediaPricing.Web.Ajax
{
    public partial class VarejoAutoComplete : System.Web.UI.UserControl
    {
        public const string LiInicio = "<li class=\"ui-state-default\">";
        public const string LiMeio = "<a href=\"#\" varejoId=\"";
        public const string LiFim = "\" class=\"removerVarejo\">X</a></li>";

        private bool Editar
        {
            get { return (bool)Session["VarejoAutoCompleteEditar"]; }
            set { Session["VarejoAutoCompleteEditar"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtVarejo.Attributes["data-hdn-varejos-id"] = hdnVarejos.ClientID;
            txtVarejo.Attributes["data-ul-varejos-id"] = ulVarejos.ClientID;
            ulVarejos.Attributes["data-hdn-varejos-id"] = hdnVarejos.ClientID;

            if (!IsPostBack)
                txtVarejo.Focus();
            else
                CarregarTela(ObterVarejos());
        }

        public void Inicializar(List<Varejo> varejos, bool editar)
        {
            this.Editar = editar;

            CarregarTela(varejos);

            if (!editar)
                txtVarejo.Enabled = false;
        }

        public bool VarejoSelecionado()
        {
            return !string.IsNullOrEmpty(hdnVarejos.Value);
        }

        public List<Varejo> ObterVarejos()
        {
            List<Varejo> varejos = new List<Varejo>();

            Varejos acessorVarejos = FabricaDeRepositorio.Varejos();

            if (!string.IsNullOrEmpty(hdnVarejos.Value))
                foreach (string varejoId in hdnVarejos.Value.Split(';'))
                    varejos.Add(acessorVarejos.ConsultarPorId(int.Parse(varejoId)));

            return varejos;
        }

        private void CarregarTela(List<Varejo> varejos)
        {
            string hidden = string.Empty;

            bool primeiro = true;
            foreach (Varejo varejo in varejos)
            {
                //Adicionando <li>
                HtmlGenericControl control = new HtmlGenericControl();

                if (Editar)
                    control.InnerHtml = string.Concat(LiInicio, varejo.Nome, LiMeio, varejo.Id, LiFim);
                else
                    control.InnerHtml = string.Concat(LiInicio, varejo.Nome, " </li>");

                ulVarejos.Controls.Add(control);

                //Adicionando valor no hidden
                if (!primeiro) hidden += ";";
                hidden += varejo.Id;

                primeiro = false;
            }

            hdnVarejos.Value = hidden;
        }
    }
}