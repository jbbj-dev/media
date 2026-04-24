<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VarejoAutoComplete.ascx.cs"
    Inherits="Ibope.MediaPricing.Web.Ajax.VarejoAutoComplete" %>
<%@ Register Src="~/Ajax/TextBoxAutoComplete.ascx" TagName="AutoComplete" TagPrefix="Ibope" %>
<div>
    <label>Varejos:</label>
    <div>
        <ul id="ulVarejos" runat="server" class="ulVarejos"></ul>
        <asp:HiddenField ID="hdnVarejos" runat="server" />
    </div>
    <asp:TextBox ID="txtVarejo" runat="server" class="txtVarejo" />
</div>

<style>
    .ulVarejos {
        list-style-type: none;
        margin: 0;
        padding: 0;
        width: 450px;
        float: left;
    }

        .ulVarejos li {
            margin: 3px 3px 3px 0;
            padding: 1px;
            float: left;
            height: 20px;
            text-align: left;
        }

            .ulVarejos li a {
                color: red !important;
                margin-left: 5px;
                float: right;
            }
</style>
