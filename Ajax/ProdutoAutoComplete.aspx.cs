using System;
using System.Collections.Generic;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios.Interfaces;
using Ibope.MediaPricing.Dominio.Excecoes;
using System.Text;

namespace Ibope.MediaPricing.Web.Ajax
{
    public partial class ProdutoAutoComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string produtoNome = Request.QueryString["term"];

            int marcaId = default(int);

            if (!string.IsNullOrEmpty(Request.QueryString["marcaId"]))
                marcaId = int.Parse(Request.QueryString["marcaId"]);
            
            List<Produto> produtos = FabricaDeRepositorio.Produtos().ListarPorNomeEMarcaId(produtoNome, marcaId);

            Response.Clear();
            Response.Write("[");
            bool first = true;
            foreach (Produto produto in produtos)
            {
                if (!first)
                {
                    Response.Write(",");
                }
                first = false;

                Response.Write(produto.ParaApresentacaoJson());
            }
            Response.Write("]");
            Response.End();
        }
    }
}
