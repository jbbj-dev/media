<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Ibope.MediaPricing.Web.Inc.controls.Menu" %>

<ul class="menu">
    <li class="titMenu">MENU</li>
    <li><a href="../../Admin/Principal.aspx">Principal</a></li>
    <li runat="server" id="menuColeta"><a href="../../Admin/Coleta.aspx">Coleta</a></li>
    <li runat="server" id="menuConferencia"><a href="../../Admin/ConferenciaListagem.aspx">Conferência</a></li>
    <li runat="server" id="menuTrocaVideo"><a href="../../Admin/TrocaDeVideo.aspx">Troca de Vídeo</a></li>    
    <li runat="server" id="menuAdminColeta"><a href="../../Admin/AdministracaoColeta.aspx">Adm. Coleta</a></li>
    <li runat="server" id="menuAdminEmissora"><a href="../../Admin/AdministracaoEmissora.aspx">Adm. Emissoras</a></li>
    <li runat="server" id="menuAdminEmissoraReplicadora"><a href="../../Admin/AdministracaoEmissoraReplicadora.aspx">Adm. Emissoras Replicadoras</a></li>
    <li runat="server" id="menuAdminPraca"><a href="../../Admin/AdministracaoPraca.aspx">Adm. Praça</a></li>
    <li runat="server" id="menuAdminSegmento"><a href="../../Admin/AdministracaoSegmento.aspx">Adm. Segmentos</a></li>
    <li runat="server" id="menuAdminUsuario"><a href="../../Admin/AdministracaoUsuario.aspx">Adm. Usuários</a></li>
    <li runat="server" id="menuRelatAlteracao"><a href="../../Admin/RelatorioAlteracaoPropaganda.aspx">Relatório de Alterações</a></li>
    <li runat="server" id="menuRelatDesemp"><a href="../../Admin/RelatorioDesempenhoColetagem.aspx">Relatório de Produtividade de Propaganda</a></li>
    <li runat="server" id="menuRelatProdOferta"><a href="../../Admin/RelatorioProdutividadeOferta.aspx">Relatório de Produtividade de Oferta</a></li>
    <li runat="server" id="menuRelarOferta"><a href="../../Admin/RelatorioOferta.aspx">Relatório de Ofertas</a></li>
    <li runat="server" id="menuRelatPropagandaColeta"><a href="../../Admin/RelatorioPropagandaColetar.aspx">Relatório de Coleta</a></li>
    <li runat="server" id="menuRelatDispVideos"><a href="../../Admin/RelatorioDisponibilizacaoVideos.aspx">Relatório de Disponibilização de Vídeos</a></li>
    <li runat="server" id="menuRelatorioEmissoraSegmento"><a href="../../Admin/RelatorioEmissoraSegmento.aspx">Relatório de Emissoras Segmentos</a></li>
    <li runat="server" id="menuExportacaoManualDePropagandas"><a href="../../Admin/ExportacaoManualDePropagandas.aspx">Exportação de Propagandas</a></li>
    <li runat="server" id="menuExportacaoManualDePropagandasBaixaQualidade"><a href="../../Admin/ExportacaoManualDePropagandasBaixaQualidade.aspx">Exportação de Propagandas Baixa Qualidade </a></li>
    <li runat="server" id="menuExportacaoManualDeReplicacoes"><a href="../../Admin/ExportacaoManualDeReplicacoes.aspx">Exportação de Replicações</a></li>
    <li runat="server" id="menuExibicaoLog"><a href="../../Admin/ExibicaoLog.aspx">Logs</a></li>
    <li>
        <asp:LinkButton OnClick="BtnSairClick" runat="server">Sair</asp:LinkButton></li>
</ul>
