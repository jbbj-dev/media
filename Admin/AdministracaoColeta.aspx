<%@ Page Title="Administração da Coleta" Language="C#" MasterPageFile="~/templates/Internas.Master" AutoEventWireup="true"
    CodeBehind="AdministracaoColeta.aspx.cs" Inherits="Ibope.MediaPricing.Web.Admin.AdministracaoColeta" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #ulAnunciantesVinculados {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 60%;
        }

            #ulAnunciantesVinculados li {
                margin: 0 3px 3px 3px;
                padding: 0.4em;
                padding-left: 1.5em;
                font-size: 1.4em;
                height: 18px;
            }

                #ulAnunciantesVinculados li span {
                    position: absolute;
                    margin-left: -1.3em;
                }

        #ulSegmentosVinculados {
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 60%;
        }

            #ulSegmentosVinculados li {
                margin: 0 3px 3px 3px;
                padding: 0.4em;
                padding-left: 1.5em;
                font-size: 1.4em;
                height: 18px;
            }

                #ulSegmentosVinculados li span {
                    position: absolute;
                    margin-left: -1.3em;
                }
    </style>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h1>Administração da Coleta</h1>
    <fieldset class="filtrosAdmUsuario canto_arredondado">
        <div>
            <label>Usuário - Login: </label>
            <asp:DropDownList ID="ddlUsuario" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUsuario_Changed" Width="500px" />
            &nbsp;
             <asp:CheckBox ID="cbxColetaTodosAnunciantes" runat="server" Text="Coleta todos os anunciantes"
                 AutoPostBack="True" OnCheckedChanged="cbxColetaTodosAnunciantes_CheckedChanged" />
            &nbsp;
             <asp:CheckBox ID="cbxColetaTodosSegmentos" runat="server" Text="Coleta todos os segmentos"
                 AutoPostBack="True" OnCheckedChanged="cbxColetaTodosSegmentos_CheckedChanged" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
                <ContentTemplate>
                    <div style="position: relative; float: right !important;">
                        <asp:LinkButton ID="btnReplicar" runat="server" OnClick="btnReplicar_Click" CssClass="botao_azul btnReplicar">
                    <span>Replicar</span>
                        </asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </fieldset>
    <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
        <ProgressTemplate>
            <div style="background-color: rgba(0,30,120,0.35); width: 100%; top: 0px; left: 0px; position: fixed; height: 100%; z-index: 9000;">
            </div>
            <div style="vertical-align: middle; top: 45%; position: fixed; right: 45%; background-color: #ffffff; border-radius: 10px; z-index: 9001; padding: 12px 20px; box-shadow: 0 8px 24px rgba(0,30,120,0.18);">
                <p style="margin-left: 5%;">Processando...</p>
                <p>
                    <img src="../Inc/images/loading.gif" />
                </p>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="painelPrincipal" runat="server">
        <fieldset class="filtrosAdmUsuario canto_arredondado">
            <br />
            <div style="width: 90%">
                <label>Anunciantes: </label>
                <asp:DropDownList ID="ddlAnunciante" runat="server" Width="500px">
                </asp:DropDownList>
                <asp:LinkButton ID="btnVincular" runat="server" OnClick="btnVincular_Click" CssClass="botao_azul">
                <span>Vincular</span>
                </asp:LinkButton>
            </div>
            <br />
            <br />
            <fieldset class="filtrosAdmUsuario canto_arredondado">
                <br />
                <div style="width: 100%;">
                    <h2>Anunciantes Vinculados</h2>
                </div>
                <div>
                    <asp:Repeater ID="rptAnunciantesVinculados" runat="server" OnItemCommand="rptAnunciantesVinculados_ItemCommand">
                        <HeaderTemplate>
                            <ul id="ulAnunciantesVinculados" style="width: 500px">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="ui-state-default">
                                <asp:HiddenField ID="hdnUsuarioAnuncianteId" runat="server" Value='<%#Eval("Id")%>' />
                                <span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                <%#Eval("Anunciante.Nome")%>
                                <asp:LinkButton ID="removerAnunciante" runat="server" Text="X" CssClass="vermelho" Style="float: right;"></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </fieldset>
        </fieldset>
        <fieldset class="filtrosAdmUsuario canto_arredondado">
            <br />
            <div style="width: 90%">
                <label>Segmentos: </label>
                <asp:DropDownList ID="dllSegmento" runat="server" Width="500px">
                </asp:DropDownList>
                <asp:LinkButton ID="btnVincluarSegmento" runat="server" OnClick="btnVincluarSegmento_Click" CssClass="botao_azul">
                <span>Vincular</span>
                </asp:LinkButton>
            </div>
            <br />
            <br />
            <fieldset class="filtrosAdmUsuario canto_arredondado">
                <br />
                <div style="width: 100%;">
                    <h2>Segmentos Vinculados</h2>
                </div>
                <div>
                    <asp:Repeater ID="rptSegmentosVinculados" runat="server" OnItemCommand="rptSegmentosVinculados_ItemCommand">
                        <HeaderTemplate>
                            <ul id="ulSegmentosVinculados" style="width: 500px">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="ui-state-default">
                                <asp:HiddenField ID="hdnUsuarioSegmentoId" runat="server" Value='<%#Eval("Id")%>' />
                                <span class="ui-icon ui-icon-arrowthick-2-n-s"></span>
                                <%#Eval("Segmento.Nome")%>
                                <asp:LinkButton ID="removerSegmento" runat="server" Text="X" CssClass="vermelho" Style="float: right;"></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </fieldset>
        </fieldset>
    </asp:Panel>
    <div id="divAplicandoOrdenacao" style="display: none;">
        Aplicando ordenação...
    </div>
</asp:Content>
<asp:Content ID="ContentFooter" ContentPlaceHolderID="FooterContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            AtribuiConfirmacao($(".btnReplicar"), "Deseja realmente replicar a configuração atual? As configurações dos outros usuários serão sobrescritas.");
        });

        $(function () {
            $('#ulAnunciantesVinculados').sortable({
                update: function (event, ui) {

                    $("#divAplicandoOrdenacao").dialog({
                        open: function (event, ui) {
                            $(".ui-dialog-titlebar").hide();
                        },
                        modal: true,
                        width: 180
                    });

                    var idsOrdenados = [];
                    $(this).find('li').each(function () {
                        idsOrdenados.push($(this).find("input[id*='hdnUsuarioAnuncianteId']").val());
                    });

                    $.ajax({
                        type: 'POST',
                        url: 'AdministracaoColeta.aspx/AplicarOrdenacaoAnunciante',
                        data: JSON.stringify({ ids: idsOrdenados }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            $('#ulAnunciantesVinculados').find('li').each(function (index) {
                                $(this).find("input[id*='hdnUsuarioAnuncianteId']").val(result.d[index]);
                            })

                            setTimeout(function () {
                                $("#divAplicandoOrdenacao").dialog("close");
                            }, 500);
                        }
                    });
                }
            });


            $('#ulSegmentosVinculados').sortable({
                update: function (event, ui) {

                    $("#divAplicandoOrdenacao").dialog({
                        open: function (event, ui) {
                            $(".ui-dialog-titlebar").hide();
                        },
                        modal: true,
                        width: 180
                    });

                    var idsOrdenados = [];
                    $(this).find('li').each(function () {
                        idsOrdenados.push($(this).find("input[id*='hdnUsuarioSegmentoId']").val());
                    });

                    $.ajax({
                        type: 'POST',
                        url: 'AdministracaoColeta.aspx/AplicarOrdenacaoSegmento',
                        data: JSON.stringify({ ids: idsOrdenados }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            $('#ulSegmentosVinculados').find('li').each(function (index) {
                                $(this).find("input[id*='hdnUsuarioSegmentoId']").val(result.d[index]);
                            })

                            setTimeout(function () {
                                $("#divAplicandoOrdenacao").dialog("close");
                            }, 500);
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>
