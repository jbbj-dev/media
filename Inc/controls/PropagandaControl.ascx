<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PropagandaControl.ascx.cs"
    Inherits="Ibope.MediaPricing.Web.Admin.controls.PropagandaControl" %>
<%@ Register Src="~/Ajax/VarejoAutoComplete.ascx" TagName="VarejoAutoComplete" TagPrefix="Ibope" %>
<table class="areaVideo">
    <tr>
        <td width="1">
            <div class="video-js-box">
                <video id="videotag" class="video-js" width="380" height="288" controls="controls" preload="auto">
                    <source src="<%=MontarCaminhoVideo()%>" type='video/ogg; codecs="vorbis"' />
                </video>
            </div>
        </td>
        <td class="dadosPropaganda">
            <asp:HiddenField ID="video" runat="server" />
            <div class="propaganda">
                <div class="statusContainer">
                    <asp:Label ID="lblStatusPropaganda" runat="server" />
                </div>
                <p>
                    <asp:Label class="labelPropagandaControl" runat="server" Text="ID:"></asp:Label>
                    <asp:TextBox ID="txtId" runat="server" Enabled="false"></asp:TextBox>
                    
                </p>
                <p>
                    <Ibope:VarejoAutoComplete ID="ucVarejoAutoComplete" runat="server" />
                </p>
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Veiculação:"></asp:Label>
                            <asp:TextBox ID="txtDataVeiculacao" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Criação:"></asp:Label>
                            <asp:TextBox ID="txtDataCriacaoExecucao" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Validade Início:"></asp:Label>
                            <asp:TextBox ID="txtDataValidadeInicio" runat="server" CssClass="jqueryCampoData"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label runat="server" Text="Validade Fim:"></asp:Label>
                            <asp:TextBox ID="txtDataValidadeFim" runat="server" CssClass="jqueryCampoData"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Label runat="server" Text="(Id)Praça - Veículo:"></asp:Label>
                    <asp:TextBox ID="txtPracaVeiculo" runat="server" Enabled="false"></asp:TextBox>
                </p>
                <p>
                    <asp:Label runat="server" Text="Setor:"></asp:Label>
                    <asp:TextBox ID="txtSetor" runat="server" Enabled="false"></asp:TextBox>
                </p>
                <p>
                    <asp:Label runat="server" Text="Categoria:"></asp:Label>
                    <asp:TextBox ID="txtCategoria" runat="server" Enabled="false"></asp:TextBox>
                </p>
                <p>
                    <asp:Label runat="server" Text="Item:"></asp:Label>
                    <asp:TextBox ID="txtItem" runat="server" Enabled="false"></asp:TextBox>
                </p>
                <p>
                    <asp:Label runat="server" Text="Anunciante:"></asp:Label>
                    <asp:TextBox ID="txtAnunciante" runat="server" Enabled="false"></asp:TextBox>
                </p>
            </div>
        </td>
        <td>
            <div id="divComentario" runat="server" class="propagandaComentarios" style="margin-top: 27px;">
                <div>
                    <asp:Label runat="server" Text="Comentário da coleta"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtComentarioColeta" runat="server" TextMode="MultiLine" onkeyup="LimitarCaracter(this, '500');"
                        Style="font-size: 16px;"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label runat="server" Text="Comentário da conferência"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtComentarioConferencia" runat="server" TextMode="MultiLine" onkeyup="LimitarCaracter(this, '500');"
                        Style="font-size: 16px;"></asp:TextBox>
                </div>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">
    function LimitarCaracter(campo, TamMax) {
        var caracteres = TamMax - campo.value.length;
        if (campo.value.length >= TamMax)
            campo.value = campo.value.substring(0, TamMax);
    }
</script>

