using System;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Repositorios.Interfaces;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class Principal : System.Web.UI.Page
    {
        private MonitoracaoCarregadorPropagandas monitoracaoCarregadorPropagandas = FabricaDeRepositorio.MonitoracaoCarregadorPropagandas();
        private MonitoracaoCarregadorVideosHq monitoracaoCarregadorVideosHq = FabricaDeRepositorio.MonitoracaoCarregadorVideosHq();
        private MonitoracaoSincronizadoresSb monitoracaoSincronizadoresSb = FabricaDeRepositorio.MonitoracaoSincronizadoresSb();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}
