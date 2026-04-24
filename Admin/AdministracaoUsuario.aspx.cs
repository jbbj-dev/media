using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Enumeradores;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Repositorios.Interfaces;
using Ibope.MediaPricing.Dominio.Utilitarios;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class AdministracaoUsuario : System.Web.UI.Page
    {
        Usuarios repositorioUsuarios = FabricaDeRepositorio.Usuarios();
        private string MensagemErro { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnEditar.Visible = false;
                CarregarUsuarios();

                foreach (UsuarioPerfil usuarioPerfil in Enum.GetValues(typeof(UsuarioPerfil)))
                    ddlPerfis.Items.Add(new ListItem(usuarioPerfil.RetornaValorString(), ((int)usuarioPerfil).ToString()));

                ddlPerfis.DataBind();
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (CamposValidadosDeInsercao())
            {
                repositorioUsuarios.InserirERecuperarId(FormatarEntidade());

                LimparCampos();
                CarregarUsuarios();
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (CamposValidadosDeEdicao())
            {
                repositorioUsuarios.Atualizar(FormatarEntidade());

                LimparCampos();
                CarregarUsuarios();

                btnAdicionar.Visible = true;
                btnEditar.Visible = false;
            }
        }

        protected void rptOfertas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int usuarioId = int.Parse(((HiddenField)e.Item.FindControl("hdnUsuarioId")).Value);
            Usuario usuario = repositorioUsuarios.ConsultarPorId(usuarioId);

            if (e.CommandName == "Editar")
            {
                hdnId.Value = usuarioId.ToString().Trim();
                txtNome.Text = usuario.Nome.Trim();
                txtEmail.Text = usuario.Email.Trim();
                txtLogin.Text = usuario.Login.Trim();
                ddlPerfis.SelectedValue = ((int)usuario.Perfil).ToString();

                btnAdicionar.Visible = false;
                btnEditar.Visible = true;
            }
            else if (e.CommandName == "AtivarInativar")
            {
                usuario.Ativo = !usuario.Ativo;
                repositorioUsuarios.Atualizar(usuario);

                rptOfertas.DataSource = repositorioUsuarios.Listar();
                rptOfertas.DataBind();
            }
        }

        private void CarregarUsuarios()
        {
            rptOfertas.DataSource = repositorioUsuarios.Listar();
            rptOfertas.DataBind();
        }

        private bool CamposValidadosDeEdicao()
        {
            bool validado = CamposValidados();

            if (validado)
            {
                Usuario usuarioAlterado = FormatarEntidade();
                Usuario usuarioExistente = repositorioUsuarios.ConsultarPorLogin(FormatarEntidade().Login);

                if (usuarioExistente != null &&
                    usuarioAlterado.Id != usuarioExistente.Id)
                {
                    validado = false;
                    LoginValidacao.Visible = true;
                    MensagemErro = "Login já cadastrado! <br/>Tente outro.";
                }
            }

            if (!validado)
                WebUtilitarios.Util.ExibirMensagem(MensagemErro, Page);

            return validado;
        }

        private bool CamposValidadosDeInsercao()
        {
            bool validado = CamposValidados();

            if (validado)
                if (repositorioUsuarios.ConsultarPorLogin(FormatarEntidade().Login) != null)
                {
                    validado = false;
                    LoginValidacao.Visible = true;
                    MensagemErro = "Login já cadastrado! <br/>Tente outro.";
                }

            if (!validado)
                WebUtilitarios.Util.ExibirMensagem(MensagemErro, Page);

            return validado;
        }

        private bool CamposValidados()
        {
            string EmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" +
                                  @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
                                  @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
                                  @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


            bool validado = true;
            NomeValidacao.Visible = false;
            EmailValidacao.Visible = false;
            LoginValidacao.Visible = false;
            SenhaValidacao.Visible = false;
            ConfSenhaValidacao.Visible = false;
            PerfilValidacao.Visible = false;

            MensagemErro = "Preencha os campos:<br />";

            //Nome
            if (string.IsNullOrEmpty(txtNome.Text.Trim()))
            {
                NomeValidacao.Visible = true;
                validado = false;
                MensagemErro += "<b> - Nome</b><br />";
            }

            //Email
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                EmailValidacao.Visible = true;
                validado = false;
                MensagemErro += "<b> - Email</b><br />";
            }
            else
            {
                if (!Regex.IsMatch(txtEmail.Text.Trim(), EmailPattern))
                {
                    EmailValidacao.Visible = true;
                    validado = false;
                    MensagemErro = "Email inválido";
                }
            }

            //Login
            if (string.IsNullOrEmpty(txtLogin.Text.Trim()))
            {
                LoginValidacao.Visible = true;
                validado = false;
                MensagemErro += "<b> - Login</b><br />";
            }

            //Perfil
            if (ddlPerfis.SelectedValue == "0")
            {
                PerfilValidacao.Visible = true;
                validado = false;
                MensagemErro += "<b> - Perfil</b><br />";
            }

            //Conf. Senha
            if (txtSenha.Text.Trim() != txtConfirmacaoSenha.Text.Trim())
            {
                ConfSenhaValidacao.Visible = true;
                validado = false;
                MensagemErro = "A senha e confirmação de senha devem ser iguais";
            }

            return validado;
        }

        private void LimparCampos()
        {
            hdnId.Value = string.Empty;
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtConfirmacaoSenha.Text = string.Empty;
            ddlPerfis.ClearSelection();
        }

        private Usuario FormatarEntidade()
        {
            Usuario usuario = new Usuario();

            usuario.Id = Convert.ToInt32(string.IsNullOrEmpty(hdnId.Value.Trim()) ? "0" : hdnId.Value.Trim());
            usuario.Nome = txtNome.Text;
            usuario.Email = txtEmail.Text;
            usuario.Login = txtLogin.Text;
            usuario.Senha = txtSenha.Text;
            usuario.Perfil = (UsuarioPerfil)int.Parse(ddlPerfis.SelectedValue);
            usuario.Ativo = true;
            usuario.ColetaTodosAnunciantes = false;
            usuario.CalcularMd5();
            return usuario;
        }
    }
}