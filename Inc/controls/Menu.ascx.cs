using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Enumeradores;
using System;
using System.Web.Security;

namespace Ibope.MediaPricing.Web.Inc.controls
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExconderTodosOsMenus();
            ExibirMenurPorPerfil();
        }

        private void ExibirMenurPorPerfil()
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            switch (usuario.Perfil)
            {
                case UsuarioPerfil.Coletor:
                    menuColeta.Visible = true;
                    menuTrocaVideo.Visible = true;
                    break;
                case UsuarioPerfil.Conferidor:
                    menuConferencia.Visible = true;
                    menuTrocaVideo.Visible = true;
                    break;
                case UsuarioPerfil.Administrador:
                    menuColeta.Visible = true;
                    menuConferencia.Visible = true;
                    menuRelatDesemp.Visible = true;
                    menuRelatProdOferta.Visible = true;
                    menuRelarOferta.Visible = true;
                    menuRelatAlteracao.Visible = true;
                    menuRelatPropagandaColeta.Visible = true;
                    menuAdminUsuario.Visible = true;
                    menuAdminColeta.Visible = true;
                    menuAdminEmissora.Visible = true;
                    menuAdminEmissoraReplicadora.Visible = true;
                    menuAdminSegmento.Visible = true;
                    menuAdminPraca.Visible = true;
                    menuExibicaoLog.Visible = true;
                    menuExportacaoManualDePropagandas.Visible = true;
                    menuExportacaoManualDeReplicacoes.Visible = true;
                    menuExportacaoManualDePropagandasBaixaQualidade.Visible = true;
                    menuTrocaVideo.Visible = true;
                    menuRelatorioEmissoraSegmento.Visible = true;
                    break;
                case UsuarioPerfil.ShoppingBrasil:
                    menuColeta.Visible = true;
                    menuRelatDesemp.Visible = true;
                    menuRelatProdOferta.Visible = true;
                    menuRelarOferta.Visible = true;
                    menuAdminSegmento.Visible = true;
                    menuExportacaoManualDePropagandas.Visible = true;
                    menuExportacaoManualDeReplicacoes.Visible = true;
                    menuExportacaoManualDePropagandasBaixaQualidade.Visible = true;
                    break;
            }
        }

        private void ExconderTodosOsMenus()
        {
            menuColeta.Visible = false;
            menuConferencia.Visible = false;
            menuRelatDesemp.Visible = false;
            menuRelatProdOferta.Visible = false;
            menuRelarOferta.Visible = false;
            menuRelatAlteracao.Visible = false;
            menuRelatPropagandaColeta.Visible = false;
            menuAdminUsuario.Visible = false;
            menuAdminColeta.Visible = false;
            menuAdminEmissora.Visible = false;
            menuAdminEmissoraReplicadora.Visible = false;
            menuAdminSegmento.Visible = false;
            menuAdminPraca.Visible = false;
            menuExibicaoLog.Visible = false;
            menuExportacaoManualDePropagandas.Visible = false;
            menuExportacaoManualDeReplicacoes.Visible = false;
            menuExportacaoManualDePropagandasBaixaQualidade.Visible = false;
            menuTrocaVideo.Visible = false;
            menuRelatorioEmissoraSegmento.Visible = false;
        }

        protected void BtnSairClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }
    }
}