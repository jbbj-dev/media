<%@ Page Title="Administração de usuários" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true"
    CodeBehind="AdministracaoUsuario.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.AdministracaoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administração de usuários</h1>
    <fieldset class="filtrosAdmUsuario canto_arredondado">
        <asp:HiddenField ID="hdnId" runat="server" />
        <div>
            <label>Nome:</label>
            <span id="NomeValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Email:</label>
            <span id="EmailValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Login:</label>
            <span id="LoginValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Senha:</label>
            <span id="SenhaValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <label>Conf. Senha:</label>
            <span id="ConfSenhaValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox ID="txtConfirmacaoSenha" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <label>Perfil:</label>
            <span id="PerfilValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:DropDownList ID="ddlPerfis" runat="server" />
        </div>
        <asp:LinkButton ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" CssClass="botao_azul">
            <span>Adicionar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click" CssClass="botao_azul">
            <span>Editar</span>
        </asp:LinkButton>
    </fieldset>
    <div>
        <asp:Repeater ID="rptOfertas" runat="server" OnItemCommand="rptOfertas_ItemCommand">
            <HeaderTemplate>
                <table width="100%" class="Generica">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Email</th>
                            <th>Login</th>
                            <th>Perfil</th>
                            <th>Ativo</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width: 35%;"><%#Eval("Nome")%></td>
                    <td><%#Eval("Email")%></td>
                    <td><%#Eval("Login")%></td>
                    <td><%#Eval("Perfil")%></td>
                    <td>
                        <asp:LinkButton ID="Inativar" runat="server" CommandName="AtivarInativar" CssClass="botao_azul"><span>
                            <%#Eval("Ativo").ToString().Equals("True") ? "Inativar" : "Ativar"%></span>
                        </asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="Editar" runat="server" CommandName="Editar" CssClass="botao_azul">
                            <span>Editar</span>
                        </asp:LinkButton>
                        <asp:HiddenField ID="hdnUsuarioId" runat="server" Value='<%#Eval("Id") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
