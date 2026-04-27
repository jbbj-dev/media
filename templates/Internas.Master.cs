using Ibope.MediaPricing.Dominio.Servicos.Monitoracao;
using System;
using System.IO;
using System.Web;

namespace Ibope.MediaPricing.Web.templates
{
    public partial class Internas : System.Web.UI.MasterPage
    {
        private ServicoMonitoracao servicoMonitoracao = new ServicoMonitoracao();

        public static string GetVersaoSistema()
        {
            string versaoSistema = string.Empty;
            string pathArquivoVersao = HttpContext.Current.Server.MapPath("~/versao.txt");

            if (File.Exists(pathArquivoVersao))
                versaoSistema = File.ReadAllText(pathArquivoVersao);
            else
                versaoSistema = "DEV";

            return versaoSistema;
        }

        public static string GetAnoCorrente()
        {
            return DateTime.Now.Year.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string mensagemAlerta = string.Empty;

            bool primeiraInteracao = true;

            foreach (string item in servicoMonitoracao.MonitoracaoAlerta(DateTime.Now))
            {
                if (!primeiraInteracao)
                    mensagemAlerta += "</br></br>";

                primeiraInteracao = false;
                mensagemAlerta += item;
            }

            imgAlerta.Visible = false;

            if (!string.IsNullOrEmpty(mensagemAlerta))
            {
                imgAlerta.Alt = mensagemAlerta + "</br></br> " + Dominio.Utilitarios.Config.SiteMensagemMonitoracaoErro;
                imgAlerta.Visible = true;
            }
        }
    }
}
