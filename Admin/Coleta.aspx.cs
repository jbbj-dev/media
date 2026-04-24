using Ibope.MediaPricing.Dominio.Entidades;
using Ibope.MediaPricing.Dominio.Enumeradores;
using Ibope.MediaPricing.Dominio.Repositorios;
using Ibope.MediaPricing.Dominio.Servicos.Coleta;
using Ibope.MediaPricing.Dominio.Utilitarios;
using Ibope.MediaPricing.Web.Ajax;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ibope.MediaPricing.Web.Admin
{
    public partial class Coleta : System.Web.UI.Page
    {
        private int UsuarioId
        {
            get
            {
                return Convert.ToInt32(Session["UsuarioId"]);
            }
        }

        private string IdPagina
        {
            get { return ViewState["IdPagina"].ToString(); }
            set { ViewState["IdPagina"] = value; }
        }

        private Propaganda PropagandaEmColeta
        {
            get { return (Propaganda)Session[$"{IdPagina}_PROPAGANDA_EM_COLETA"]; }
            set { Session[$"{IdPagina}_PROPAGANDA_EM_COLETA"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            IdPagina = Guid.NewGuid().ToString();

            this.PropagandaEmColeta = null;

            Propaganda propaganda = new ServicoColeta().ConsultarPropagandaParaSerColetada(UsuarioId);

            if (propaganda.Id != default(int))
            {
                PropagandaEmColeta = propaganda;
                ucPropaganda.AtribuirPropagandaColeta(propaganda);
                CarregarOfertasEmTela(propaganda.Ofertas, false);
            }

            ApresentarStatusDaPropaganda();
        }

        protected void btnAdicionarOferta_Click(object sender, EventArgs e)
        {
            List<Oferta> ofertas = new List<Oferta>();

            ofertas.Add(new Oferta());

            //Ordenação na inclusão das ofertas em tela, a oferta adicionada deve ficar sobre as demaiss
            foreach (Oferta oferta in ColetarOfertasEmTela())
                ofertas.Add(oferta);

            CarregarOfertasEmTela(ofertas, false);
        }

        protected void btnExcluirOferta_Click(object sender, EventArgs e)
        {
            List<Oferta> ofertas = ColetarOfertasEmTela();

            ofertas.RemoveAt(((RepeaterItem)((Control)sender).Parent).ItemIndex);

            CarregarOfertasEmTela(ofertas, true);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            bool propagandaValidada = ValidarPropaganda();
            bool ofertaValidada = ValidarOfertas();

            if (propagandaValidada && ofertaValidada)
            {
                PropagandaEmColeta.ComentarioColeta = ucPropaganda.ObterComentarioColeta();
                PropagandaEmColeta.DataValidadeInicio = ucPropaganda.ObterDataValidadeInicio();
                PropagandaEmColeta.DataValidadeFim = ucPropaganda.ObterDataValidadeFim();
                PropagandaEmColeta.Varejos = ucPropaganda.ObterVarejos();

                PropagandaEmColeta.Ofertas.Clear();
                PropagandaEmColeta.AdicionarOfertas(ColetarOfertasEmTela());

                new ServicoColeta().ColetarPropaganda(PropagandaEmColeta, PropagandaFluxo.Conferencia, PropagandaSituacao.Valida, UsuarioId);

                Response.Redirect("Coleta.aspx");
            }
        }

        protected void btnSalvarParcialmente_Click(object sender, EventArgs e)
        {
            bool propagandaValidada = ValidarPropagandaSalvaParcialmente();
            bool ofertaValidada = ValidarOfertas();

            if (propagandaValidada && ofertaValidada)
            {
                PropagandaEmColeta.DataValidadeInicio = ucPropaganda.ObterDataValidadeInicio();
                PropagandaEmColeta.DataValidadeFim = ucPropaganda.ObterDataValidadeFim();
                PropagandaEmColeta.ComentarioColeta = ucPropaganda.ObterComentarioColeta();
                PropagandaEmColeta.Varejos = ucPropaganda.ObterVarejos();
                PropagandaEmColeta.Ofertas.Clear();
                PropagandaEmColeta.AdicionarOfertas(ColetarOfertasEmTela());

                new ServicoColeta().ColetarPropaganda(PropagandaEmColeta, PropagandaFluxo.ColetaSalvoParcialmente, PropagandaSituacao.Valida, UsuarioId);

                Response.Redirect("Coleta.aspx");
            }
        }

        protected void btnSalvarInvalida_Click(object sender, EventArgs e)
        {
            if (ValidarPropagandaInvalida())
            {
                PropagandaEmColeta.ComentarioColeta = ucPropaganda.ObterComentarioColeta();
                PropagandaEmColeta.Ofertas.Clear();

                new ServicoColeta().ColetarPropaganda(PropagandaEmColeta, PropagandaFluxo.Conferencia, PropagandaSituacao.Invalida, UsuarioId);

                Response.Redirect("Coleta.aspx");
            }
        }

        protected void btnSalvarFalha_Click(object sender, EventArgs e)
        {
            if (ValidarPropagandaInvalida())
            {
                PropagandaEmColeta.ComentarioColeta = ucPropaganda.ObterComentarioColeta();
                PropagandaEmColeta.Ofertas.Clear();

                new ServicoColeta().ColetarPropaganda(PropagandaEmColeta, PropagandaFluxo.Conferencia, PropagandaSituacao.BaixaQualidade, UsuarioId);

                Response.Redirect("Coleta.aspx");
            }
        }

        private void CarregarOfertasEmTela(List<Oferta> ofertas, bool exclusao)
        {
            if (ofertas.Count == 0)
                if (!exclusao)
                    ofertas.Add(new Oferta());

            rptOfertasAdicionadas.DataSource = ofertas;
            rptOfertasAdicionadas.DataBind();

            foreach (RepeaterItem item in rptOfertasAdicionadas.Items)
            {
                var autoCompleteMarcaProduto = ((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto"));
                decimal preco = decimal.Parse(((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text);

                //Verifica se é uma MARCA nova
                if (autoCompleteMarcaProduto.EMarcaNova())
                    autoCompleteMarcaProduto.MarcaNova(true);

                //Verifica se é um PRODUTO novo
                if (autoCompleteMarcaProduto.EProdutoNovo())
                    autoCompleteMarcaProduto.ProdutoNovo(true);

                //Corrige o formato do preço
                if (preco != default(decimal))
                    ((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text = string.Format("{0:N2}", preco);
                else
                    ((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text = string.Empty;

                //Verifica se o preco informado esta fora do preco max. ou preco min.
                if (!autoCompleteMarcaProduto.EProdutoNovo() && preco != default(decimal))
                {
                    Produto produto = FabricaDeRepositorio.Produtos().ConsultarPorId(autoCompleteMarcaProduto.ObterProdutoId());
                    if (preco < produto.ValorMin || preco > produto.ValorMax)
                    {
                        ((Label)item.FindControl("lblPrecoComentario")).Attributes.Add("style", "display:block;");
                        ((Label)item.FindControl("lblPrecoComentario")).Text =
                            "Min: " + string.Format("{0:0.00}", produto.ValorMin) + " Max: " + string.Format("{0:0.00}", produto.ValorMax);
                    }
                }
            }
        }

        private List<Oferta> ColetarOfertasEmTela()
        {
            List<Oferta> ofertas = new List<Oferta>();

            foreach (RepeaterItem item in rptOfertasAdicionadas.Items)
            {
                Oferta oferta = new Oferta();

                int id = Convert.ToInt32(((HiddenField)item.FindControl("hdnOfertaId")).Value);
                if (id != default(int))
                {
                    Oferta ofertaExistente = FabricaDeRepositorio.Ofertas().ConsultarPorId(id);
                    oferta = ofertaExistente;
                }

                oferta.Produto = new Produto
                {
                    Id = ((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).ObterProdutoId(),
                    Nome = ((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).ObterProdutoNome(),
                    Marca = new Marca
                    {
                        Id = ((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).ObterMarcaId(),
                        Nome = ((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).ObterMarcaNome(),
                    }
                };

                decimal precoVista;
                decimal.TryParse(((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text.Trim(), out precoVista);

                //Se foi digitado valor negativo assume valor 0
                oferta.PrecoVista = precoVista < 0 ? 0 : precoVista;

                int cartao1 = ((TextBoxAutoComplete)item.FindControl("autoCompleteCartao1")).ObterId();
                if (cartao1 != default(int))
                    oferta.Cartao1 = new Cartao
                    {
                        Id = ((TextBoxAutoComplete)item.FindControl("autoCompleteCartao1")).ObterId(),
                        Nome = ((TextBoxAutoComplete)item.FindControl("autoCompleteCartao1")).ObterNome()
                    };

                int cartao2 = ((TextBoxAutoComplete)item.FindControl("autoCompleteCartao2")).ObterId();
                if (cartao2 != default(int))
                    oferta.Cartao2 = new Cartao
                    {
                        Id = ((TextBoxAutoComplete)item.FindControl("autoCompleteCartao2")).ObterId(),
                        Nome = ((TextBoxAutoComplete)item.FindControl("autoCompleteCartao2")).ObterNome()
                    };

                oferta.CondicaoPagamento1 = ((TextBox)item.FindControl("txtOfertaCondicaoPagamento1")).Text;

                oferta.CondicaoPagamento2 = ((TextBox)item.FindControl("txtOfertaCondicaoPagamento2")).Text;

                oferta.Observacao = ((TextBox)item.FindControl("txtObservacao")).Text;

                ofertas.Add(oferta);
            }

            return ofertas;
        }

        private void ApresentarStatusDaPropaganda()
        {
            int totalDePropagandasEmColeta = FabricaDeRepositorio.Propagandas().ConsultarTotalDePropagandasEmColeta();

            string status = string.Empty;

            if (totalDePropagandasEmColeta >= 0 && PropagandaEmColeta != null)
            {
                status = string.Format("{0} propaganda(s) restante(s).", totalDePropagandasEmColeta);

                status += " Status: Propaganda ";

                switch (PropagandaEmColeta.Fluxo)
                {
                    case PropagandaFluxo.Coleta:
                        status += "Nova.";
                        break;
                    case PropagandaFluxo.ColetaReprovado:
                        status += "Reprovada.";
                        break;
                    case PropagandaFluxo.ColetaSalvoParcialmente:
                        status += "Salva parcialmente.";
                        break;
                    case PropagandaFluxo.ColetaRecuperadoBaixaQualidade:
                        status += "Vídeo de melhor qualidade encontrado.";
                        break;
                }

                status += " Qualidade do Vídeo: ";

                switch (PropagandaEmColeta.Qualidade)
                {
                    case QualidadeVideo.Baixa:
                        status += "Baixa.";
                        break;
                    case QualidadeVideo.Normal:
                        status += "Normal.";
                        break;
                    case QualidadeVideo.Hq:
                        status += "Alta.";
                        break;
                    case QualidadeVideo.Mp3:
                        status += "Somente áudio.";
                        break;
                }
            }
            else
                status = "Não existem propagandas para coleta.";

            this.ucPropaganda.DescricaoStatusPropaganda = status;
        }

        private bool ValidarPropaganda()
        {
            List<string> erros = new List<string>();

            Propaganda propaganda = FabricaDeRepositorio.Propagandas().ConsultarPorId(PropagandaEmColeta.Id);

            if (propaganda.Fluxo == PropagandaFluxo.Conferencia || propaganda.Fluxo == PropagandaFluxo.Aprovado)
                erros.Add("Esta propaganda já foi coletada.");

            if (PropagandaEmColeta.Qualidade != propaganda.Qualidade)
                erros.Add("Esta propaganda teve o vídeo substituído enquanto a coleta estava sendo feita. Salve parcialmente a propaganda para que o vídeo seja recarregado, confira novamente as ofertas e então poderá salvar normalmente.");

            if (!ucPropaganda.VarejoSelecionado())
                erros.Add("Um ou mais varejos devem ser selecionados.");

            if (ucPropaganda.ObterDataValidadeInicio() == default(DateTime) ||
                ucPropaganda.ObterDataValidadeFim() == default(DateTime) ||
                ucPropaganda.ObterDataVeiculacao() == default(DateTime))
                erros.Add("Datas de inválidas");

            if (ucPropaganda.ObterDataValidadeInicio() > ucPropaganda.ObterDataValidadeFim())
                erros.Add("Data de validade inicial maior que a data final.");

            if (VerificadorPais.Brasil &&
                ucPropaganda.ObterDataVeiculacao().Date > ucPropaganda.ObterDataValidadeInicio())
                erros.Add("Data de veiculacao maior que a data de validade inicial.");

            if (erros.Count > 0)
            {
                string mensagemErro = string.Empty;

                foreach (string erro in erros)
                    mensagemErro += "-" + erro + "</br>";

                WebUtilitarios.Util.ExibirMensagem(mensagemErro, this);
                return false;
            }
            else
                return true;
        }

        private bool ValidarPropagandaInvalida()
        {
            List<string> erros = new List<string>();

            Propaganda propaganda = FabricaDeRepositorio.Propagandas().ConsultarPorId(PropagandaEmColeta.Id);

            if (propaganda.Fluxo == PropagandaFluxo.Conferencia || propaganda.Fluxo == PropagandaFluxo.Aprovado)
                erros.Add("Esta propaganda já foi coletada.");

            if (ucPropaganda.VarejoSelecionado())
                erros.Add("Nenhum varejo deve ser selecionado");

            if (rptOfertasAdicionadas.Items.Count != 0)
                erros.Add("Ainda existem ofertas atreladas à propaganda.");

            if (erros.Count > 0)
            {
                string mensagemErro = string.Empty;

                foreach (string erro in erros)
                    mensagemErro += "-" + erro + "</br>";

                WebUtilitarios.Util.ExibirMensagem(mensagemErro, this);
                return false;
            }
            else
                return true;
        }

        private bool ValidarPropagandaSalvaParcialmente()
        {
            List<string> erros = new List<string>();

            Propaganda propaganda = FabricaDeRepositorio.Propagandas().ConsultarPorId(PropagandaEmColeta.Id);

            if (propaganda.Fluxo == PropagandaFluxo.Conferencia || propaganda.Fluxo == PropagandaFluxo.Aprovado)
                erros.Add("Esta propaganda já foi coletada.");

            if (erros.Count > 0)
            {
                string mensagemErro = string.Empty;

                foreach (string erro in erros)
                    mensagemErro += "-" + erro + "</br>";

                WebUtilitarios.Util.ExibirMensagem(mensagemErro, this);
                return false;
            }
            else
                return true;
        }

        private bool ValidarOfertas()
        {
            const decimal MAIOR_VALOR_VALIDO = 999999.99M;

            //Validação se ao menos uma oferta foi preenchida
            if (rptOfertasAdicionadas.Items.Count == 0)
            {
                WebUtilitarios.Util.ExibirMensagem("Insira ao menos uma oferta.", this);
                return false;
            }

            foreach (RepeaterItem item in rptOfertasAdicionadas.Items)
            {
                if (((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).EProdutoNovo() &&
                    ((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).ObterMarcaNome().Length > 120)
                    return false;

                if (((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).EMarcaNova() &&
                    ((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).ObterProdutoNome().Length > 20)
                    return false;

                if (((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text.Length > 0 &&
                    Convert.ToDecimal(((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text) > MAIOR_VALOR_VALIDO)
                    return false;

                if (((TextBox)item.FindControl("txtOfertaCondicaoPagamento1")).Text.Length > 40)
                    return false;

                if (((TextBox)item.FindControl("txtOfertaCondicaoPagamento2")).Text.Length > 40)
                    return false;

                if (((TextBox)item.FindControl("txtObservacao")).Text.Length > 255)
                    return false;

                //Se a condicao de pagamento for informada valida se foi informada corretamente
                if (!String.IsNullOrEmpty(((TextBox)item.FindControl("txtOfertaCondicaoPagamento1")).Text) &&
                    !ValidaCondicaoPagamento(((TextBox)item.FindControl("txtOfertaCondicaoPagamento1")).Text))
                    return false;

                //Se a condicao de pagamento for informada valida se foi informada corretamente
                if (!String.IsNullOrEmpty(((TextBox)item.FindControl("txtOfertaCondicaoPagamento2")).Text) &&
                    !ValidaCondicaoPagamento(((TextBox)item.FindControl("txtOfertaCondicaoPagamento2")).Text))
                    return false;

                //Se o preco a vista e condicação forem informadas valida se o valor parcelado não é menor que
                //o valor a vista
                if (!String.IsNullOrEmpty(((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text) &&
                    !String.IsNullOrEmpty(((TextBox)item.FindControl("txtOfertaCondicaoPagamento1")).Text) &&
                    !ValidaValorParcelado(((TextBox)item.FindControl("txtOfertaCondicaoPagamento1")).Text, ((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text))
                {
                    WebUtilitarios.Util.ExibirMensagem("Valor parcelado não pode ser menor que o valor a vista.", this);
                    return false;
                }

                if (!String.IsNullOrEmpty(((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text) &&
                    !String.IsNullOrEmpty(((TextBox)item.FindControl("txtOfertaCondicaoPagamento2")).Text) &&
                    !ValidaValorParcelado(((TextBox)item.FindControl("txtOfertaCondicaoPagamento2")).Text, ((TextBox)item.FindControl("txtOfertaPrecoAVista")).Text))
                {
                    WebUtilitarios.Util.ExibirMensagem("Valor parcelado não pode ser menor que o valor a vista.", this);
                    return false;
                }

                //Validação Marca e Produto
                if (!((MarcaProdutoAutoComplete)item.FindControl("autoCompleteMarcaProduto")).ValidarMarcaProduto())
                    return false;
            }

            return true;
        }

        private bool ValidaCondicaoPagamento(string condicao)
        {
            string pattern = @"[^a-z](\+)[^a-z]+(\*)\s*(?:[1-9]\d{0,2}(?:\d{3})*|0),\d{2}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(condicao);
        }

        private bool ValidaValorParcelado(string condicaoPagamento, string preco)
        {
            double aVista = default(double),
                   entrada = default(double),
                   quantidadeDeVezes = default(double),
                   valorParcela = default(double);

            if (!double.TryParse(preco, out aVista))
            {
                WebUtilitarios.Util.ExibirMensagem("O valor: " + preco + " informado para o preço a vista está no formato incorreto", this);
                return false;
            }

            if (!double.TryParse(condicaoPagamento.Split('+')[0], out entrada))
            {
                WebUtilitarios.Util.ExibirMensagem("O valor: " + condicaoPagamento.Split('+')[0] + " informado para a entrada está no formato incorreto", this);
                return false;
            }

            if (!double.TryParse(condicaoPagamento.Split('*')[0].Split('+')[1], out quantidadeDeVezes))
            {
                WebUtilitarios.Util.ExibirMensagem("O valor: " + condicaoPagamento.Split('*')[0].Split('+')[1] + " informado para quantidade de parcelas está no formato incorreto", this);
                return false;
            }

            if (!double.TryParse(condicaoPagamento.Split('*')[1], out valorParcela))
            {
                WebUtilitarios.Util.ExibirMensagem("O valor: " + condicaoPagamento.Split('*')[1] + " informado para valor da parcela está no formato incorreto", this);
                return false;
            }

            double totalParcelas = entrada + quantidadeDeVezes;

            double valorAvistaParcelado = Truncate((aVista / totalParcelas), 2);


            return (valorParcela >= valorAvistaParcelado);
        }

        private double Truncate(double valor, int digitos)
        {
            double mult = System.Math.Pow(10, digitos);
            return System.Math.Truncate(valor * mult) / mult;
        }
    }
}