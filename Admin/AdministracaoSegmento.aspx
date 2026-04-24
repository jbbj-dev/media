<%@ Page Title="Administração de segmentos" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true"
    CodeBehind="AdministracaoSegmento.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.AdministracaoSegmento" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Inc/css/datatable_jui.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/css/ibope.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administração de segmentos</h1>
    <fieldset class="filtrosAdmUsuario canto_arredondado">
        <asp:HiddenField ID="hdnIdSegmento" runat="server" />
        <div>
            <label>Setor</label>
            <span id="SetorValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox Id="txtSetor" runat="server" Width="200px" />
        </div>
        <div>
            <label>Categoria</label>
            <span id="CategoriaValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox Id="txtCategoria" runat="server" Width="250px" />
        </div>
        <div>
            <label>Item</label>
            <span id="ItemValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox Id="txtItem" runat="server" Width="200px" />
        </div>
        <div>
            <label>ID Externo</label>
            <span id="idExternoValidacao" runat="server" visible="false" class="msgErro">*</span>
            <br />
            <asp:TextBox Id="txtIdExterno" runat="server" />
        </div>
        <asp:LinkButton ID="btnAdicionar" runat="server" CssClass="botao_azul" OnClick="btnAdicionar_Click" style="margin: 10px 0 0 10px; float: left;">
            <span>Adicionar</span>
        </asp:LinkButton>
        <asp:LinkButton runat="server" ID="btnAtivarDesativar" CssClass="botao_azul" OnClick="btnAtivarDesativar_Click">
            <span>AtivarDesativar</span>
        </asp:LinkButton>
    </fieldset>
    <div>
        <asp:Repeater ID="rptSegmentos" runat="server">
            <HeaderTemplate>
                <table id="tblSegmentos">
                    <thead>
                        <tr>
                            <th>NOME</th>
                            <th>ID EXTERNO</th>
                            <th>STATUS ATIVO</th>
                            <th>ATIVO / INATIVO</th>
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
                        <td align="right">
                            <%#Eval("IdExterno")%>
                        </td>
                        <td align="center">
                            <asp:Label id="lblAtivo" runat="server" Text='<%#Eval("StatusAtivo")%>' ></asp:Label>
                        </td>
                        <td align="center">
                            <%#Eval("Ativar")%>
                        </td>
                        <td>
                            <asp:Label id="lblIdSegmento" runat="server" Text='<%#Eval("Id")%>' ></asp:Label>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th>NOME</th>
                            <th>ID EXTERNO</th>
                            <th>STATUS ATIVO</th>
                            <th>ATIVO INATIVO</th>
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
            $('#tblSegmentos').dataTable({
                "sPaginationType": "full_numbers",
                "bProcessing": true,
                "aLengthMenu": [10, 15, 30, 50, 100],
                "aoColumnDefs": [
                    { "sWidth": "60%", "sType": "string-case", "aTargets": [0] },
                    { "sWidth": "15%", "sType": "numeric", "aTargets": [1] },
                    { "sWidth": "15%", "aTargets": [2] },
                    { "sWidth": "10%", "bSearchable": false, "bSortable": false, "aTargets": [3] },
                    { "bVisible": false, "aTargets": [4] }]
            });

            /* Esconde botão de ativar e desativar */
            $('#<%=btnAtivarDesativar.ClientID%>').attr('style', 'display:none;');

            /* Copia id do segmento selecionado  */
            $(document).on('click', '.jquerySegmento', function () {
                $('#<%=hdnIdSegmento.ClientID%>').val($(this).attr('segmentoId'));
            });
        });
    </script>
</asp:Content>
