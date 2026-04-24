using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class AdministracaoEmissoraReplicadora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregarTela();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                EmissoraReplicadora novaEmissoraReplicadora = new EmissoraReplicadora()
                {
                    Praca = new Praca() { Id = int.Parse(ddlPraca.SelectedValue) },
                    Veiculo = new Veiculo() { Id = int.Parse(ddlVeiculo.SelectedValue) }
                };

                FabricaDeRepositorio.EmissorasReplicadoras().InserirERecuperarId(novaEmissoraReplicadora);
                CarregarTela();
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdnReplicadoraId.Value))
                throw new Exception("Id da Emissora Replicadora não definido");

            if (ValidarCampos())
            {
                EmissoraReplicadora replicadora = new EmissoraReplicadora()
                {
                    Id = int.Parse(hdnReplicadoraId.Value),
                    Praca = new Praca() { Id = int.Parse(ddlPraca.SelectedValue) },
                    Veiculo = new Veiculo() { Id = int.Parse(ddlVeiculo.SelectedValue) }
                };

                FabricaDeRepositorio.EmissorasReplicadoras().Atualizar(replicadora);
                CarregarTela();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CarregarTela();
        }

        protected void rptEmissorasReplicadoras_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int replicadoraId = int.Parse(((HiddenField)e.Item.FindControl("hdnRptReplicadoraId")).Value);

            switch (e.CommandName)
            {
                case "Editar":
                    EmissoraReplicadora replicadora = FabricaDeRepositorio.EmissorasReplicadoras().ConsultarPorId(replicadoraId);
                    hdnReplicadoraId.Value = replicadora.Id.ToString();
                    ddlPraca.SelectedValue = replicadora.Praca.Id.ToString();
                    ddlVeiculo.SelectedValue = replicadora.Veiculo.Id.ToString();
                    btnAdicionar.Visible = false;
                    btnEditar.Visible = true;
                    btnCancelar.Visible = true;
                    break;
                case "Excluir":
                    if (FabricaDeRepositorio.EmissorasReplicadoras().VerificaSeFoiVinculada(replicadoraId))
                        WebUtilitarios.Util.ExibirMensagem("A emissora replicadora está vinculada.", Page);
                    else
                        FabricaDeRepositorio.EmissorasReplicadoras().ExcluirPorId(replicadoraId);
                    CarregarTela();
                    break;
                default:
                    throw new Exception("Comando desconhecido.");
            }
        }

        private bool ValidarCampos()
        {
            string mensagemErro = string.Empty;
            int pracaId = int.Parse(ddlPraca.SelectedValue);
            int veiculoId = int.Parse(ddlVeiculo.SelectedValue);

            if (pracaId == default(int) || veiculoId == default(int))
            {
                mensagemErro += "Preencha os campos:<br />";

                if (pracaId == default(int))
                    mensagemErro += "<b> - Praça</b><br />";

                if (veiculoId == default(int))
                    mensagemErro += "<b> - Veículo</b><br />";
            }

            if (string.IsNullOrEmpty(mensagemErro))
                if (FabricaDeRepositorio.EmissorasReplicadoras().ListarTodas().Exists(x => x.Praca.Id == pracaId && x.Veiculo.Id == veiculoId))
                    mensagemErro += "Emissora Replicadora já cadastrada";

            if (string.IsNullOrEmpty(mensagemErro))
                return true;
            else
            {
                WebUtilitarios.Util.ExibirMensagem(mensagemErro, Page);
                return false;
            }
        }

        private void CarregarTela()
        {
            CarregarEmissorasReplicadoras();
            CarregarPracas();
            CarregarVeiculos();
            btnAdicionar.Visible = true;
            btnEditar.Visible = false;
            btnCancelar.Visible = false;
        }

        private void CarregarPracas()
        {
            List<Praca> pracas = FabricaDeRepositorio.Pracas().ListarTodos().OrderBy(x => x.Nome).ToList();
            pracas.Insert(0, new Praca() { Id = 0, Nome = string.Empty });

            ddlPraca.DataSource = pracas;
            ddlPraca.DataTextField = "Nome";
            ddlPraca.DataValueField = "Id";

            ddlPraca.DataBind();
        }

        private void CarregarVeiculos()
        {
            List<Veiculo> veiculos = FabricaDeRepositorio.Veiculos().ListarTodosAtivos().OrderBy(x => x.Nome).ToList();

            foreach (Veiculo veiculo in veiculos) 
                veiculo.Nome = string.Format("{0} - {1}", veiculo.Nome, veiculo.IdExterno);

            veiculos.Insert(0, new Veiculo() { Id = 0, Nome = string.Empty });

            ddlVeiculo.DataSource = veiculos;
            ddlVeiculo.DataTextField = "Nome";
            ddlVeiculo.DataValueField = "Id";

            ddlVeiculo.DataBind();
        }

        private void CarregarEmissorasReplicadoras()
        {
            rptEmissorasReplicadoras.DataSource = FabricaDeRepositorio.EmissorasReplicadoras().ListarTodas().OrderBy(x => x.Nome);
            rptEmissorasReplicadoras.DataBind();
        }
    }
}
