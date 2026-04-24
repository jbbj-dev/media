<%@ Page Title="Administração de praças" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true"
    CodeBehind="AdministracaoPraca.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.AdministracaoPraca" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Inc/css/datatable_jui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administração de praças</h1>
    <fieldset class="filtrosAdmUsuario canto_arredondado">
        <asp:HiddenField ID="hdnIdPraca" runat="server" />
        <div>
            <label>Nome</label>
            <span id="NomeValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox Id="txtNomePraca" runat="server" Width="200px" />
        </div>
        <div>
            <label>UF</label>
            <span id="UfValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox Id="txtUfPraca" runat="server" Width="250px" MaxLength="2" />
        </div>
        <asp:LinkButton ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" CssClass="botao_azul">
            <span>Adicionar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click" CssClass="botao_azul" Visible=false>
            <span>Editar</span>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="btnExcluir" CssClass="botao_azul" OnClick="btnExcluir_Click" >
            <span>Excluir</span>
        </asp:LinkButton>
    </fieldset>
    <div>
        <asp:Repeater ID="rptPracas" runat="server" OnItemCommand="rptPracas_ItemCommand">
            <HeaderTemplate>
                <table id="tblPracas">
                    <thead>
                        <tr>
                            <th>NOME</th>
                            <th>UF</th>
                            <th>EDITAR</th>
                            <th>EXCLUIR</th>
                            <th>ID</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("Nome")%>
                        </td>
                        <td align="center">
                            <%#Eval("Uf")%>
                        </td>
                        <td align="center">
                            <asp:LinkButton ID="Editar" runat="server" CommandName="Editar" CssClass="botao_azul"><span>Editar</span></asp:LinkButton>
                            <asp:HiddenField ID="hdnPracaId" runat="server" Value='<%#Eval("Id") %>' />
                        </td>
                        <td align="center">
                            <%#Eval("Excluir")%>
                        </td>
                        <td>
                            <asp:Label id="lblIdPraca" runat="server" Text='<%#Eval("Id")%>' ></asp:Label>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th>NOME</th>
                            <th>UF</th>
                            <th>EDITAR</th>
                            <th>EXCLUIR</th>
                            <th>ID</th>
                        </tr>
                    </tfoot>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="FooterContent" runat="server">
    <script src="../Inc/js/jquery.dataTables.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            /* Define two custom functions (asc and desc) for string sorting */
            jQuery.fn.dataTableExt.oSort['string-case-asc'] = function (x, y) { return ((x < y) ? -1 : ((x > y) ? 1 : 0)); };
            jQuery.fn.dataTableExt.oSort['string-case-desc'] = function (x, y) { return ((x < y) ? 1 : ((x > y) ? -1 : 0)); };

            /* Define Datatable */
            $('#tblPracas').dataTable({
                "sPaginationType": "full_numbers",
                "bProcessing": true,
                "aLengthMenu": [10, 15],
                "aoColumnDefs": [
                    { "sWidth": "60%", "sType": "string-case", "aTargets": [0] },
                    { "sWidth": "10%", "sType": "string-case", "aTargets": [1] },
                    { "sWidth": "10%", "bSearchable": false, "bSortable": false, "aTargets": [2] },
                    { "sWidth": "10%", "bSearchable": false, "bSortable": false, "aTargets": [3] },
                    { "bVisible": false, "aTargets": [4] }
                ]
            });

            /* Esconde Botões de Excluir */
            $('#<%=btnExcluir.ClientID%>').attr('style', 'display:none;');
            
            /* Copia id do praça selecionado  */
            $(document).on('click', '.jqueryPraca', function() {
                $('#<%=hdnIdPraca.ClientID%>').val($(this).attr('pracaId'));
            });
        });
    </script>
</asp:Content>