<%@ Page Title="Administração de Emissoras Replicadoras" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true"
    CodeBehind="AdministracaoEmissoraReplicadora.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.AdministracaoEmissoraReplicadora" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Inc/css/datatable_jui.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/css/ibope.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-button {
            padding: 0px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administração de Emissoras Replicadoras</h1>
    <fieldset class="filtrosAdmUsuario canto_arredondado">
        <asp:HiddenField ID="hdnReplicadoraId" runat="server" />
        <div>
            <label>Praça</label>
            <asp:DropDownList ID="ddlPraca" runat="server" Width="200px">
            </asp:DropDownList>
        </div>
        <div>
            <label>Veículo - ID Shopping Brasil (Ativos)</label>
            <asp:DropDownList ID="ddlVeiculo" runat="server" Width="200px">
            </asp:DropDownList>
        </div>
        <asp:LinkButton ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" CssClass="botao_azul">
            <span>Adicionar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click" CssClass="botao_azul">
            <span>Editar</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" CssClass="botao_azul">
            <span>Cancelar</span>
        </asp:LinkButton>
    </fieldset>
    <div>
        <asp:Repeater ID="rptEmissorasReplicadoras" runat="server" OnItemCommand="rptEmissorasReplicadoras_ItemCommand">
            <HeaderTemplate>
                <table id="tblEmissorasReplicadoras">
                    <thead>
                        <tr>
                            <th>PRACA
                            </th>
                            <th>VEICULO
                            </th>
                            <th>EDITAR
                            </th>
                            <th>EXCLUIR
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("Praca.Nome")%>
                    </td>
                    <td align="center">
                        <%#Eval("Veiculo.Nome")%>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdnRptReplicadoraId" runat="server" Value='<%#Eval("Id") %>' />
                        <asp:LinkButton ID="lnkEditar" runat="server" CommandName="Editar" CssClass="botao_azul"><span>Editar</span></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkExcluir" runat="server" CommandName="Excluir" CssClass="botao_azul"><span>Excluir</span></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                <tfoot>
                    <tr>
                        <th>NOME
                        </th>
                        <th>UF
                        </th>
                        <th>EDITAR
                        </th>
                        <th>EXCLUIR
                        </th>
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
            $('#tblEmissorasReplicadoras').dataTable({
                "sPaginationType": "full_numbers",
                "bProcessing": true,
                "aLengthMenu": [10, 20, 30],
                "aoColumnDefs": [
                    { "sWidth": "45%", "sType": "string-case", "aTargets": [0] },
                    { "sWidth": "45%", "sType": "string-case", "aTargets": [1] },
                    { "sWidth": "5%", "bSearchable": false, "bSortable": false, "aTargets": [2] },
                    { "sWidth": "5%", "bSearchable": false, "bSortable": false, "aTargets": [3] }]
            });
        });
    </script>
</asp:Content>