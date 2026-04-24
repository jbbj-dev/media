using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Servicos.TrocaDeVideoManual;
using System;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class TrocaDeVideo : System.Web.UI.Page
    {
        private int UsuarioId
        {
            get
            {
                return Convert.ToInt32(Session["UsuarioId"]);
            }
        }

        private Propaganda PropagandaSelecionada
        {
            get { return (Propaganda)Session["PROPAGANDA_EM_COLETA"]; }
            set { Session["PROPAGANDA_EM_COLETA"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            int videoId = default(int);

            int.TryParse(txtIdVideo.Text, out videoId);

            if (videoId == default(int))
            {
                WebUtilitarios.Util.ExibirMensagem("A propaganda informada inválida.", Page);
                return;
            }

            PropagandaSelecionada = FabricaDeRepositorio.Propagandas().ConsultarPorVideoId(videoId);

            if (PropagandaSelecionada != null)
            {
                ucPropaganda.AtribuirPropagandaTrocaDeVideoManual(PropagandaSelecionada);
                areaVideo.Visible = true;
            }
            else
            {
                WebUtilitarios.Util.ExibirMensagem("A busca não retornou nenhum valor.", this);
            }
        }

        protected void btnTrocarVideo_Click(object sender, EventArgs e)
        {
            if (PropagandaSelecionada == null)
            {
                WebUtilitarios.Util.ExibirMensagem("A propaganda não foi informada.", this);
                return;
            }

            try
            {
                ServicoTrocaDeVideoManual servico = new ServicoTrocaDeVideoManual();
                servico.EfetuaTrocaDeVideoManual(PropagandaSelecionada, UsuarioId);
                ucPropaganda.AtribuirPropagandaTrocaDeVideoManual(PropagandaSelecionada);
            }
            catch
            {
                WebUtilitarios.Util.ExibirMensagem("Não foi possivel efetuar a troca do video.", this);
            }
        }
    }
}