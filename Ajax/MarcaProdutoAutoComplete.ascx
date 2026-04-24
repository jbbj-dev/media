<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarcaProdutoAutoComplete.ascx.cs"
    Inherits="Ibope.MediaPricing.Web.Ajax.MarcaProdutoAutoComplete" %>
<%--Caso modificar a estrutura de divs, rever o script(locais que utilizam a função .parent()) --%>
<div>
    <label style="padding-right: 0;">
        Marca</label>
    <asp:Label ID="lblMarcaNova" runat="server" Text="(Nova Marca)" class="novaMarca"
        Style="display: none"></asp:Label>
    <asp:Label ID="lblValidarMarca" runat="server" Text="*" Visible="false" class="msgErro ValidarMarcaProduto"></asp:Label>
    <br />
    <asp:TextBox ID="txtMarca" runat="server" class="marca" Text='<%#MarcaNome%>' />
    <asp:HiddenField ID="hdnMarcaNome" runat="server" Value='<%# MarcaNome%>' />
    <asp:HiddenField ID="hdnMarcaId" runat="server" Value='<%# MarcaId%>' />
</div>
<div class="produtoModelo">
    <label style="padding-right: 0;">
        Produto/Modelo</label>
    <asp:Label ID="lblProdutoNovo" runat="server" Text="(Novo Produto)" class="novoProduto"
        Style="display: none"></asp:Label>
    <asp:Label ID="lblValidarProduto" runat="server" Text="*" Visible="false" class="msgErro ValidarMarcaProduto"></asp:Label>
    <br />
    <asp:TextBox ID="txtProduto" runat="server" class="produto" Text='<%#ProdutoNome%> ' />
    <asp:HiddenField ID="hdnProdutoNome" runat="server" Value='<%# ProdutoNome%>' />
    <asp:HiddenField ID="hdnProdutoId" runat="server" Value='<%# ProdutoId%>' />
    <asp:HiddenField ID="hdnProdutoValorMax" runat="server" Value='<%# ProdutoValorMax%>' />
    <asp:HiddenField ID="hdnProdutoValorMin" runat="server" Value='<%# ProdutoValorMin%>' />
</div>

