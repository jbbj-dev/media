using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Repositorios.Interfaces;
using Ibope.MediaPricing.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class AdministracaoColeta : System.Web.UI.Page
    {
        Anunciantes repositorioAnunciantes = FabricaDeRepositorio.Anunciantes();
        Segmentos repositorioSegmentos = FabricaDeRepositorio.Segmentos();
        Usuarios repositorioUsuarios = FabricaDeRepositorio.Usuarios();
        UsuarioAnunciantes repositorioUsuarioAnunciantes = FabricaDeRepositorio.UsuarioAnunciantes();
        UsuarioSegmentos repositorioUsuarioSegmentos = FabricaDeRepositorio.UsuarioSegmentos();
        private static ServicoUsuarioAnunciante servicoAnunciante = new ServicoUsuarioAnunciante();
        private static ServicoUsuarioSegmento servicoSegmento = new ServicoUsuarioSegmento();
        private static ServicoUsuario servicoUsuario = new ServicoUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpaTela();

                List<Usuario> usuarios = repositorioUsuarios.Listar().OrderBy(x => x.Login).ToList();
                foreach (Usuario usuario in usuarios) usuario.Nome = string.Format("{0} - {1}", usuario.Nome, usuario.Login);
                usuarios.Insert(0, new Usuario() { Nome = string.Empty, Id = default(int) });
                ddlUsuario.DataTextField = "Nome";
                ddlUsuario.DataValueField = "Id";
                ddlUsuario.DataSource = usuarios;
                ddlUsuario.DataBind();
            }
        }

        protected void btnVincular_Click(object sender, EventArgs e)
        {
            try
            {
                int usuarioId = UsuarioSelecionado();
                int anuncianteId = AnuncianteSelecionado();

                servicoAnunciante.Vincular(usuarioId, anuncianteId);

                CarregaTela();
            }
            catch (Exception ex)
            {
                WebUtilitarios.Util.ExibirMensagem(ex.Message, this);
            }
        }

        protected void ddlUsuario_Changed(object sender, EventArgs e)
        {
            LimpaTela();

            int usuarioId = int.Parse(ddlUsuario.SelectedValue);

            if (usuarioId != default(int))
                CarregaTela();
        }

        protected void cbxColetaTodosAnunciantes_CheckedChanged(Object sender, EventArgs e)
        {
            int usuarioId = UsuarioSelecionado();

            Usuario usuario = repositorioUsuarios.ConsultarPorId(usuarioId);
            usuario.ColetaTodosAnunciantes = cbxColetaTodosAnunciantes.Checked;

            repositorioUsuarios.Atualizar(usuario);
        }

        protected void rptAnunciantesVinculados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int usuarioAnuncianteId = int.Parse(((HiddenField)e.Item.FindControl("hdnUsuarioAnuncianteId")).Value);
            repositorioUsuarioAnunciantes.ExcluirPorId(usuarioAnuncianteId);
            CarregaTela();
        }

        private void CarregaTela()
        {
            int usuarioId = UsuarioSelecionado();

            cbxColetaTodosAnunciantes.Visible = true;
            cbxColetaTodosSegmentos.Visible = true;
            painelPrincipal.Visible = true;
            btnReplicar.Visible = true;

            CarregaUsuarioAnuncianteVinculados();
            CarregaUsuarioSegmentoVinculados();
            CarregaAnunciantes();
            CarregaSegmentos();

            var usuario = repositorioUsuarios.ConsultarPorId(usuarioId);
            cbxColetaTodosAnunciantes.Checked = usuario.ColetaTodosAnunciantes;
            cbxColetaTodosSegmentos.Checked = usuario.ColetaTodosSegmentos;

        }

        private void LimpaTela()
        {
            cbxColetaTodosAnunciantes.Visible = false;
            cbxColetaTodosSegmentos.Visible = false;
            painelPrincipal.Visible = false;
            btnReplicar.Visible = false;
        }

        private void CarregaUsuarioAnuncianteVinculados()
        {
            int usuarioId = UsuarioSelecionado();

            if (usuarioId == default(int))
                throw new Exception("Usuário inválido.");

            rptAnunciantesVinculados.DataSource = repositorioUsuarioAnunciantes.ListarPorUsuario(usuarioId).OrderBy(x => x.Id);
            rptAnunciantesVinculados.DataBind();
        }


        private void CarregaUsuarioSegmentoVinculados()
        {
            int usuarioId = UsuarioSelecionado();

            if (usuarioId == default(int))
                throw new Exception("Usuário inválido.");

            rptSegmentosVinculados.DataSource = repositorioUsuarioSegmentos.ListarPorUsuario(usuarioId).OrderBy(x => x.Id);
            rptSegmentosVinculados.DataBind();
        }

        private void CarregaAnunciantes()
        {
            ddlAnunciante.DataTextField = "Nome";
            ddlAnunciante.DataValueField = "Id";
            ddlAnunciante.DataSource = repositorioAnunciantes.ListarTodos().OrderBy(x => x.Nome).ToList();
            ddlAnunciante.DataBind();
        }

        private void CarregaSegmentos()
        {
            dllSegmento.DataTextField = "Nome";
            dllSegmento.DataValueField = "Id";
            dllSegmento.DataSource = repositorioSegmentos.ListarTodos().OrderBy(x => x.Nome).ToList();
            dllSegmento.DataBind();
        }

        private int UsuarioSelecionado()
        {
            int usuarioId = int.Parse(ddlUsuario.SelectedValue);

            if (usuarioId == default(int))
                throw new Exception("Usuário inválido.");

            return usuarioId;
        }

        private int AnuncianteSelecionado()
        {
            int anuncianteId = int.Parse(ddlAnunciante.SelectedValue);

            if (anuncianteId == default(int))
                throw new Exception("Anunciante inválido.");

            return anuncianteId;
        }

        private int SegmentoSelecionado()
        {
            int segmentoId = int.Parse(dllSegmento.SelectedValue);

            if (segmentoId == default(int))
                throw new Exception("Segmento inválido.");

            return segmentoId;
        }

        [WebMethod]
        public static List<int> AplicarOrdenacaoAnunciante(List<int> ids)
        {
            return servicoAnunciante.Ordenar(ids);
        }

        [WebMethod]
        public static List<int> AplicarOrdenacaoSegmento(List<int> ids)
        {
            return servicoSegmento.Ordenar(ids);
        }

        protected void btnVincluarSegmento_Click(object sender, EventArgs e)
        {
            try
            {
                int usuarioId = UsuarioSelecionado();
                int segmentoId = SegmentoSelecionado();

                servicoSegmento.Vincular(usuarioId, segmentoId);

                CarregaTela();
            }
            catch (Exception ex)
            {
                WebUtilitarios.Util.ExibirMensagem(ex.Message, this);
            }
        }

        protected void rptSegmentosVinculados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int usuarioSegmentoId = int.Parse(((HiddenField)e.Item.FindControl("hdnUsuarioSegmentoId")).Value);
            repositorioUsuarioSegmentos.ExcluirPorId(usuarioSegmentoId);
            CarregaTela();
        }

        protected void cbxColetaTodosSegmentos_CheckedChanged(object sender, EventArgs e)
        {
            int usuarioId = UsuarioSelecionado();

            Usuario usuario = repositorioUsuarios.ConsultarPorId(usuarioId);
            usuario.ColetaTodosSegmentos = cbxColetaTodosAnunciantes.Checked;

            repositorioUsuarios.Atualizar(usuario);
        }

        protected void btnReplicar_Click(object sender, EventArgs e)
        {
            try
            {
                int usuarioId = UsuarioSelecionado();

                servicoUsuario.ReplicarConfiguracaoColeta(usuarioId);
            }
            catch (Exception ex)
            {
                WebUtilitarios.Util.ExibirMensagem(ex.Message, this);
            }
        }
    }
}