using System;
using System.IO;
using System.Web;

namespace Ibope.MediaPricing.Web.templates
{
    public partial class Externas : System.Web.UI.MasterPage
    {
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
    }
}