using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ibope.MediaPricing.Web.Ajax
{
    public partial class AutoComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nome = Request.QueryString["term"].Replace(",", string.Empty);
            string repositorio = Request.QueryString["repositorio"];

            PesquisavelPorNome prop = null;
            try
            {
                Repositorio<Entidade> rep = FabricaDeRepositorio.CriarPorNome(repositorio);
                prop = (PesquisavelPorNome)rep;
            }
            catch
            {
                throw new Exception("O repositório que está tentando ser acessado não existes ou não implementa o Pesquisável Por Nome.");
            }

            List<Entidade> entidades = prop.ListarPorNome(nome).OrderBy(entidade => entidade.Nome).ToList();

            Response.Clear();
            Response.Write("[");
            bool first = true;
            foreach (Entidade entidade in entidades)
            {
                if (!first)
                    Response.Write(",");

                first = false;

                Response.Write(entidade.ParaApresentacaoJson());
            }

            Response.Write("]");
            Response.End();
        }
    }
}
