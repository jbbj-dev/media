<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxAutoComplete.ascx.cs"
    Inherits="Ibope.MediaPricing.Web.Ajax.TextBoxAutoComplete" %>
<div>
    <asp:Label ID="lblAutoComplete" runat="server"><%= labelAutoComplete%>: </asp:Label>
    <asp:TextBox ID="txtNome" runat="server" CssClass="campo txtNome" Text='<%# CampoNome%>' />
    <asp:HiddenField ID="hdnNome" runat="server" />
    <asp:HiddenField ID="hdnId" runat="server" Value='<%# CampoId%>' />
    <asp:Label ID="lblRepositorio" runat="server" Style="display: none;" CssClass="AutoCompleteRepositorio lblRepositorio"><%= repositorio%></asp:Label>
</div>
<style type="text/css">
    .AutoCompleteRepositorio
    {
        display: none;
    }
</style>

