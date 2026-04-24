using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Utilitarios;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            logPrincipal.Focus();
            logPrincipal.UserNameLabelText = "Login";
            logPrincipal.PasswordLabelText = "Senha";
            logPrincipal.LoginButtonText = "Fazer Logon";
        }

        protected void LogPrincipal_OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
            Usuario usuarioInformado = new Usuario();
            usuarioInformado.Login = this.logPrincipal.UserName.Trim();
            usuarioInformado.Senha = this.logPrincipal.Password.Trim();
            usuarioInformado.CalcularMd5();

            Usuario usuarioExistente = FabricaDeRepositorio.Usuarios().ConsultarPorLogin(usuarioInformado.Login);

            if (usuarioExistente == null)
            {
                logPrincipal.FailureText = "Login incorreto";
                return;
            }

            if (usuarioInformado.Senha != usuarioExistente.Senha)
            {
                logPrincipal.FailureText = "Senha incorreta";
                return;
            }
            
            if (!usuarioExistente.Ativo)
            {
                logPrincipal.FailureText = "Usuário inativo";
                return;
            }

            Session["UsuarioId"] = usuarioExistente.Id;
            Session["Usuario"] = usuarioExistente;

            string[] perfil = usuarioExistente.Perfil.ToString().Split(',');

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                version: 1,
                name: usuarioExistente.Login,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddMinutes(30),
                isPersistent: true,
                userData: perfil[0],
                cookiePath: FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            if (Config.FlAmbienteQA)
            {
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                {
                    HttpOnly = true
                });
            }
            else
            {
                Response.Cookies.Add(new HttpCookie(
                    name: FormsAuthentication.FormsCookieName,
                    value: encryptedTicket)
                {
                    HttpOnly = true,
                    Secure = true
                });
            }

            Response.Redirect(FormsAuthentication.GetRedirectUrl(
                usuarioExistente.Login,
                createPersistentCookie: true));
        }
    }
}
