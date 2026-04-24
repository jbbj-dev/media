<%@ Page Title="Coleta" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true"
    CodeBehind="Coleta.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.Coleta" %>
<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register Src="~/Inc/controls/PropagandaControl.ascx" TagName="PropagandaControl"
    TagPrefix="Ibope" %>
<%@ Register Src="~/Ajax/TextBoxAutoComplete.ascx" TagName="TextBoxAutoComplete"
    TagPrefix="Ibope" %>
<%@ Register Src="~/Ajax/MarcaProdutoAutoComplete.ascx" TagName="MarcaProdutoAutoComplete"
    TagPrefix="Ibope" %>

<asp:Content ID="styles" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Inc/css/video-js.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h1>Coleta</h1>    
    <div class="area_video">
        <div class="topFixedDiv area_video">
            <Ibope:PropagandaControl ID="ucPropaganda" runat="server" />
            <div id="divBotoesSuperior" class="divBotoesSuperior" style="clear: both; float: left">
                <asp:LinkButton ID="btnAdicionarOferta" runat="server" OnClick="btnAdicionarOferta_Click" CssClass="botao_azul">
                    <span>Adicionar oferta</span>
                </asp:LinkButton>
                <asp:LinkButton ID="btnSalvarParcialmente" runat="server" OnClick="btnSalvarParcialmente_Click" CssClass="botao_azul salvar">
                    <span>Salvar Parcialmente</span>
                </asp:LinkButton>
                <asp:LinkButton ID="btnSalvarFalha" runat="server" OnClick="btnSalvarFalha_Click" CssClass="botao_azul salvar">
                    <span>Salvar Baixa Qualidade</span>
                </asp:LinkButton>
                <asp:LinkButton ID="btnSalvarInvalida" runat="server" OnClick="btnSalvarInvalida_Click" CssClass="botao_azul salvar">
                    <span>Salvar Inv�lida</span>
                </asp:LinkButton>
                <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" CssClass="botao_azul salvar">
                    <span>Salvar</span>
                </asp:LinkButton>
            </div>
        </div>
        <div class="clearfix">
        </div>
    </div>
    <br />
    <asp:UpdatePanel ID="upnOfertas" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdicionarOferta" />
        </Triggers>
        <ContentTemplate>
            <div class="coleta_ofertas">
                <asp:Repeater runat="server" ID="rptOfertasAdicionadas">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnOfertaId" runat="server" Value='<%#Eval("Id")%>' />
                        <div class="ofertaItem">
                            <Ibope:MarcaProdutoAutoComplete ID="autoCompleteMarcaProduto" runat="server" MarcaId='<%#Eval("Produto.Marca.Id")%>'
                                MarcaNome='<%#Eval("Produto.Marca.Nome")%>' ProdutoId='<%#Eval("Produto.Id")%>'
                                ProdutoNome='<%#Eval("Produto.Nome")%>' ProdutoModelo='<%#Eval("Produto.Modelo")%>'
                                ProdutoValorMax='<%#Eval("Produto.ValorMax")%>' ProdutoValorMin='<%#Eval("Produto.ValorMin")%>' />
                            <div class="precoAVista">
                                <label>
                                    Pre�o � vista</label>
                                <br />
                                <asp:TextBox ID="txtOfertaPrecoAVista" runat="server" class="preco" Text='<%#Eval("PrecoVista")%>'></asp:TextBox>
                                <asp:Label ID="lblPrecoComentario" runat="server" class="precoComentario" Style="display: none;"></asp:Label>
                            </div>
                            <div>
                                <label>
                                    1� Cond. pag.</label>
                                <br />
                                <asp:TextBox runat="server" ID="txtOfertaCondicaoPagamento1" Text='<%#Eval("CondicaoPagamento1")%>'
                                    class="condicaoPagamento1"></asp:TextBox>
                            </div>
                            <div>
                                <label>
                                    2� Cond. pag.</label>
                                <br />
                                <asp:TextBox runat="server" ID="txtOfertaCondicaoPagamento2" Text='<%#Eval("CondicaoPagamento2")%>'
                                    class="condicaoPagamento2"></asp:TextBox>
                            </div>
                            <div>
                                <label>
                                    Cart�o 1</label>
                                <br />
                                <Ibope:TextBoxAutoComplete ID="autoCompleteCartao1" runat="server" repositorio="Cartoes"
                                    CampoId='<%#Eval("Cartao1.Id") %>' CampoNome='<%#Eval("Cartao1.Nome") %>' limparCamposCasoNaoExista="true" />
                            </div>
                            <div>
                                <label>
                                    Cart�o 2</label>
                                <br />
                                <Ibope:TextBoxAutoComplete ID="autoCompleteCartao2" runat="server" repositorio="Cartoes"
                                    CampoId='<%#Eval("Cartao2.Id") %>' CampoNome='<%#Eval("Cartao2.Nome") %>' limparCamposCasoNaoExista="true" />
                            </div>
                            <div class="produtoModelo">
                                <label>
                                    Observa��o</label>
                                <br />
                                <asp:TextBox ID="txtObservacao" runat="server" Text='<%#Eval("Observacao")%>'
                                    class="observacao"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="lblOfertaComentarioReprovacao" runat="server" Text='<%#Eval("ComentarioReprovacao")%>'
                                    Width="315px" CssClass="vermelho bold"></asp:Label>
                            </div>
                            <asp:LinkButton runat="server" ID="btnExcluirOferta" OnClick="btnExcluirOferta_Click"
                                class="excluirOferta" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="FooterContent" runat="server">
    <script src="../Inc/js/jquery.price_format.2.0.js" type="text/javascript"></script>
    <script src="../Inc/js/waypoints.min.js" type="text/javascript"></script>
    <script src="../Inc/js/zoom.js" type="text/javascript"></script>
    <script src="../Inc/js/video.js" type="text/javascript"></script>
    <%: Scripts.Render("~/Scripts/Ajax/TextBoxAutoComplete") %>
    <%: Scripts.Render("~/Scripts/Ajax/MarcaProdutoAutoComplete") %>
    <%: Scripts.Render("~/Scripts/Ajax/VarejoAutoComplete") %>
    <%: Scripts.Render("~/Scripts/Page/coleta") %>
</asp:Content>
