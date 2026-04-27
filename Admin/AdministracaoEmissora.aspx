<%@ Page Title="Administração de Emissoras" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true"
    CodeBehind="AdministracaoEmissora.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.AdministracaoEmissora" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Inc/css/datatable_jui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administração de emissoras</h1>
    <div id="dlgCriarEmissora">
        <div style="width: 300px; float: left;">
            <label>
                Praça</label>
            <span id="PracaValidacao" runat="server" class="msgErro pracaErr">*</span>
            <br />
            <asp:DropDownList ID="ddlPraca" runat="server" CssClass="pracaAcc" Width="200" />
        </div>
        <div style="width: 300px; float: left;">
            <label>
                Veículo - ID Shopping Brasil</label>
            <span id="VeiculoValidacao" runat="server" class="msgErro veiculoErr">*</span>
            <br />
            <asp:DropDownList ID="ddlVeiculo" runat="server" CssClass="veiculoAcc" Width="250" />
        </div>
        <div style="width: 300px; float: left;">
            <label>
                ID Media DNA</label>
            <span id="IdMediaDnaValidacao" runat="server" class="msgErro mediaDnaErr">*</span>
            <br />
            <asp:TextBox ID="txtIdMediaDna" CssClass="mediaDnaAcc" runat="server" />
        </div>
        <asp:HiddenField ID="hndIdEmissoraSelecionada" runat="server" />
        <asp:HiddenField ID="hndIdPracaSelecionada" runat="server" />
        <asp:HiddenField ID="hndIdVeiculoSelecionado" runat="server" />
        <asp:HiddenField ID="hndIdMediaSelecionado" runat="server" />
    </div>
    <fieldset class="filtrosAdmUsuario canto_arredondado">
        <div>
            <label>
                Emissora:</label>
            <asp:DropDownList ID="ddlEmissora" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmissora_Changed" />
        </div>
        <asp:LinkButton ID="btnAplicarAssociacao" runat="server" CssClass="botao_azul processarEdicao"
            OnClick="btnAplicarAssociacao_Click" Visible="false">
            <span>Aplicar Associações</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnExcluir" runat="server" CssClass="botao_azul" OnClick="btnExcluir_Click"
            Visible="false">
            <span>Ativar/Inativar Emissora</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnEditar" runat="server" CssClass="botao_azul processarAlteracao"
            Visible="false">
            <span>Editar Emissora</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnAdicionar" runat="server" CssClass="botao_azul processarInclusao">
            <span>Adicionar Emissora</span>
        </asp:LinkButton>
    </fieldset>
    <div id="tabs">
        <asp:HiddenField ID="hdnTabSelecionada" runat="server" />
        <ul>
            <li><a href="#emissoras">Emissora - Segmentos</a></li>
            <li><a href="#emissorasReplicadoras">Emissoras Replicadoras</a></li>
        </ul>
        <div id="emissoras">
            <asp:Panel ID="pnlEmissoraSegmentos" runat="server" Visible="false">
                <asp:Repeater ID="rptEmissoras" runat="server">
                    <HeaderTemplate>
                        <table id="tblEmissoras" style="width: 100% !important">
                            <thead>
                                <tr>
                                    <th align="center">EMISSORA
                                    </th>
                                    <th align="center">SEGMENTO
                                    </th>
                                    <th align="center">
                                        <input type="checkbox" id="selectall" class="check_all"/>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("EmissoraSegmento.Emissora.Nome")%>
                            </td>
                            <td>
                                <%#Eval("EmissoraSegmento.Segmento.Nome")%>
                            </td>
                            <td align="center">
                                &nbsp;&nbsp;<asp:CheckBox ID="cbxAssociado" runat="server" Checked='<%#Eval("EstaAssociada")%>' class="check"/>
                                <asp:HiddenField ID="hdnSegmentoId" runat="server" Value='<%#Eval("EmissoraSegmento.Segmento.Id")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                <tfoot>
                    <tr>
                        <th align="center">EMISSORA
                        </th>
                        <th align="center">SEGMENTO
                        </th>
                        <th align="center">ASSOCIAR
                        </th>
                    </tr>
                </tfoot>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
        <div id="emissorasReplicadoras">
            <asp:Panel ID="pnlEmissorasReplicadoras" runat="server" Visible="false">
                <fieldset class="filtrosAdmUsuario canto_arredondado">
                    <div>
                        <label>Emissoras Replicadoras: </label>
                        <asp:DropDownList ID="ddlEmissorasReplicadoras" runat="server" Width="400px">
                        </asp:DropDownList>
                    </div>
                    <asp:LinkButton ID="btnVincular" runat="server" OnClick="btnVincular_Click" CssClass="botao_azul">
                    <span>Vincular</span>
                    </asp:LinkButton>
                </fieldset>
                <asp:Repeater ID="rptEmissorasReplicadoras" runat="server" OnItemCommand="rptEmissorasReplicadoras_ItemCommand">
                    <HeaderTemplate>
                        <table id="tblEmissorasReplicadoras" style="width: 100% !important">
                            <thead>
                                <tr>
                                    <th>EMISSORA REPLICADORA VINCULADA
                                    </th>
                                    <th></th>
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
                                <asp:HiddenField ID="hdnRptReplicadoraId" runat="server" Value='<%#Eval("Id") %>' />
                                <asp:LinkButton ID="lnkDesvincular" runat="server" CommandName="Desvincular" CssClass="botao_azul">
                                    <span>Desvincular</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                            <tfoot>
                                <tr>
                                    <th>EMISSORA REPLICADORA VINCULADA
                                    </th>
                                    <th></th>
                                </tr>
                            </tfoot>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="FooterContent" runat="server">
    <script src="../Inc/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        /* Variáveis do processo */
        var oTable;
        var oEmissoraDialog;
        var iPracaId;
        var iVeiculoId;
        var iMediaDnaId;
        var iEmissoraId;

        /* Função que inclui ou altera emissora no sistema */
        function AdicionarEmissora(pracaId, veiculoId, mediaDnaId, modo) {
            $.ajax({
                "type": "POST",
                "url": "AdministracaoEmissora.aspx/ProcessarEmissora",
                "contentType": "application/json; charset=utf-8",
                "data": "{pracaId:" + pracaId + ", veiculoId:" + veiculoId + ", mediaDnaId:" + mediaDnaId + ", emissoraId:" + iEmissoraId + "}",
                "dataType": "json",
                "async": false,
                "success": function (data) {
                    var pracaValida = data.d[0];
                    var veiculoValidado = data.d[1];
                    var mediaDnaValido = data.d[2];

                    if (pracaValida && veiculoValidado && mediaDnaValido) {
                        window.location.reload();
                    } else {
                        if (!pracaValida) {
                            $('.pracaErr').show();
                        }

                        if (!veiculoValidado) {
                            $('.veiculoErr').show();
                        }

                        if (!mediaDnaValido) {
                            $('.mediaDnaErr').show();
                        }
                    }
                }
            });
        }

        /* Função que valida valor informado nos campos(sempre esperado IDs maior que zero) */
        function ValidarCampoInformado(valor) {
            var retorno = true;

            if (valor != "") {
                var value = valor.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
                var intRegex = /^\d+$/;

                if (!intRegex.test(value)) {
                    retorno = false;
                } else {
                    if (parseInt(value) <= 0) {
                        retorno = false;
                    }
                }
            }
            else {
                retorno = false;
            }

            return retorno;
        }

        /* Função que ativa e desativa indicador de erros */
        function ExibirErros(exibir) {
            if (exibir) {
                $('.pracaErr').show();
                $('.veiculoErr').show();
                $('.mediaDnaErr').show();
            } else {
                $('.pracaErr').hide();
                $('.veiculoErr').hide();
                $('.mediaDnaErr').hide();
            }
        }

        $(document).ready(function () {
            //Define as abas
            $('#tabs').tabs({
                show: function () {
                    var newIdx = $('#tabs').tabs('option', 'selected');
                    $("#<%=hdnTabSelecionada.ClientID %>").val(newIdx);
                },
                selected: 0,
                autoHeight: false
            });

            /* Define Datatable Emissoras Replicadoras*/
            $('#tblEmissorasReplicadoras').dataTable({
                "sPaginationType": "full_numbers",
                "aLengthMenu": [10, 15, 25, 50],
                "bProcessing": true,
                "aoColumnDefs": [
                    { "sWidth": "90%", "aTargets": [0] },
                    { "sWidth": "10%", "bSearchable": false, "bSortable": false, "aTargets": [1] }]
            });

            /* Define Datatable Emissoras*/
            oTable = $('#tblEmissoras').dataTable({
                "sPaginationType": "full_numbers",
                "aLengthMenu": [10, 15, 25, 50],
                "bProcessing": true,
                "aoColumnDefs": [
                    { "sWidth": "40%", "bSearchable": false, "bSortable": false, "aTargets": [0] },
                    { "sWidth": "50%", "aTargets": [1] },
                    { "sWidth": "10%", "bSearchable": false, "bSortable": false, "aTargets": [2] }]
            });

            /* Renderiza todo o datatables para aplicar associações corretamente */
            $(document).on('click', '.processarEdicao', function () {
                var oSettings = oTable.fnSettings();
                oSettings._iDisplayLength = oSettings.fnRecordsTotal();
                oTable.fnFilter('');

                oTable.fnDraw();
            });

            /* Define Diálogo para criação e alteração de emissora */
            oEmissoraDialog = $('#dlgCriarEmissora').dialog({
                "autoOpen": false,
                "height": 150,
                "width": 950,
                "resizable": false,
                "title": 'Configurar Emissora',
                "draggable": false,
                "modal": true,
                buttons: {
                    "Salvar": function () {
                        iPracaId = $('.pracaAcc').val();
                        iVeiculoId = $('.veiculoAcc').val();
                        iMediaDnaId = $('.mediaDnaAcc').val();

                        var validaPraca = ValidarCampoInformado(iPracaId);
                        var validaVeiculo = ValidarCampoInformado(iVeiculoId);
                        var validaMediaDna = ValidarCampoInformado(iMediaDnaId);

                        if (!validaPraca) {
                            $('.pracaErr').show();
                        } else {
                            $('.pracaErr').hide();
                        }

                        if (!validaVeiculo) {
                            $('.veiculoErr').show();
                        } else {
                            $('.veiculoErr').hide();
                        }

                        if (!validaMediaDna) {
                            $('.mediaDnaErr').show();
                        } else {
                            $('.mediaDnaErr').hide();
                        }

                        if (validaPraca && validaVeiculo && validaMediaDna) {
                            AdicionarEmissora(iPracaId, iVeiculoId, iMediaDnaId, iEmissoraId);
                        }
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                }
            });

            /* Define lógica executado no click do botão que inclui emissora */
            $(document).on('click', '.processarInclusao', function () {
                iEmissoraId = 0;
                oEmissoraDialog.dialog('open');

                return false;
            });

            /* Define lógica executado no click do botão que altera emissora */
            $(document).on('click', '.processarAlteracao', function () {
                iEmissoraId = $('#<%=hndIdEmissoraSelecionada.ClientID%>').val();
                $('.pracaAcc').val($('#<%=hndIdPracaSelecionada.ClientID%>').val())
                $('.veiculoAcc').val($('#<%=hndIdVeiculoSelecionado.ClientID%>').val());
                $('.mediaDnaAcc').val($('#<%=hndIdMediaSelecionado.ClientID%>').val());

                oEmissoraDialog.dialog('open');

                return false;
            });

            /* Define lógica executado no evento de teclado para campo IdMediaDna */
            $(document).on('keydown', '.mediaDnaAcc', function (e) {
                if (!(event.keyCode == 8 ||
                    event.keyCode == 46 ||
                    (event.keyCode >= 35 && event.keyCode <= 40) ||
                    (event.keyCode >= 48 && event.keyCode <= 57) ||
                    (event.keyCode >= 96 && event.keyCode <= 105))) {

                    return false;
                }
            });

            /* Inicializa indicador de erros de inclusão e alteração de emissora */
            ExibirErros(false);
        });
    </script>
</asp:Content>