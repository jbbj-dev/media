<%@ Page Title="Login" Language="C#" MasterPageFile="~/templates/Externas.Master"  AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="Ibope.MediaPricing.Web.Admin.Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <div class="formulario_login">
        <asp:Login ID="logPrincipal" runat="server" UserNameLabelText="Login:" OnAuthenticate="LogPrincipal_OnAuthenticate" TitleText="" DisplayRememberMe="false">
        </asp:Login>
    </div>
</asp:Content>
