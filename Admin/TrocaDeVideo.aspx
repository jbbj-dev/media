<%@ Page Title="Troca de Vídeo Manual" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true" CodeBehind="TrocaDeVideo.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.TrocaDeVideo" %>
<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register Src="~/Inc/controls/PropagandaControl.ascx" TagName="PropagandaControl"
    TagPrefix="Ibope" %>
<%@ Register Src="~/Ajax/TextBoxAutoComplete.ascx" TagName="TextBoxAutoComplete"
    TagPrefix="Ibope" %>
<%@ Register Src="~/Ajax/MarcaProdutoAutoComplete.ascx" TagName="MarcaProdutoAutoComplete"
    TagPrefix="Ibope" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Inc/css/video-js.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h1>Troca de Videos Manual</h1>
    <div>
        <label>Id do Video da Propaganda:</label>
        <asp:TextBox ID="txtIdVideo" runat="server" onkeypress="return verifica( this , event ) ;"></asp:TextBox>

        <asp:LinkButton ID="btnPesquisar" runat="server" CssClass="botao_azul" OnClick="btnPesquisar_Click">
                    <span>Pesquisar</span>
        </asp:LinkButton>
    </div>
    <div class="clearfix">
    </div>
    <div class="area_video" runat="server" id="areaVideo" visible="false">
        <div class="topFixedDiv area_video">
            <asp:UpdateProgress ID="updProgress"
                AssociatedUpdatePanelID="UpdatePanel1"
                runat="server">
                <ProgressTemplate>
                    <div style="background-color: rgba(0,30,120,0.35); width: 100%; top: 0px; left: 0px; position: fixed; height: 100%; z-index: 9000;">
                    </div>
                    <div style="vertical-align: middle; top: 45%; position: fixed; right: 45%; background-color: #ffffff; border-radius: 10px; z-index: 9001; padding: 12px 20px; box-shadow: 0 8px 24px rgba(0,30,120,0.18);">

                        <p style="margin-left: 5%;">Processando...</p>
                        <p>
                            <img src="../Inc/images/loading.gif" /></p>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <Ibope:PropagandaControl ID="ucPropaganda" runat="server" />
                    <div id="divBotoesSuperior" class="divBotoesSuperior" style="clear: both; float: left">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="botao_azul trocar" OnClick="btnTrocarVideo_Click">
                            <span>Trocar video</span>
                        </asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="clearfix">
        </div>
    </div>



</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="FooterContent" runat="server">
    <script src="/Inc/js/waypoints.min.js" type="text/javascript"></script>
    <script src="/Inc/js/video.js" type="text/javascript"></script>
    <%: Scripts.Render("~/Scripts/Ajax/TextBoxAutoComplete") %>
    <%: Scripts.Render("~/Scripts/Ajax/MarcaProdutoAutoComplete") %>
    <%: Scripts.Render("~/Scripts/Ajax/VarejoAutoComplete") %>
    <script type="text/javascript">
        function verifica(obj, e) {
            var tecla = (window.event) ? e.keyCode : e.which;
            if (tecla == 8 || tecla == 0)
                return true;
            if (tecla != 44 && tecla < 48 || tecla > 57)
                return false;
        }

        $(document).ready(function () {
            VideoJS.setupAllWhenReady();
            AtribuiConfirmacao($(".trocar"), " Esta propaganda irá retornar a coleta, porem manterá as ofertas informadas. Deseja realmente trocar o video da propaganda?");
        });
    </script>
</asp:Content>