<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Product_Search.ascx.cs"
    Inherits="content_userControls_Product_Search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Panel ID="PanelUI" runat="server" DefaultButton="btnSearch">
    <div id="search">
        <fieldset>
            <legend>Site Search</legend>
            <img src="../../images/search.png" style="float: left;" alt="" />
            <asp:TextBox ID="txtName" runat="server" CssClass="inputTxt"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtName"
                WatermarkText="請輸入關鍵字" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchBtn" OnClick="btnSearch_Click" />
        </fieldset>
    </div>
    <asp:HiddenField ID="hdnCId" runat="server" />
    <asp:HiddenField ID="hdnBId" runat="server" />
    <asp:HiddenField ID="hdnBType" runat="server" />
</asp:Panel>
