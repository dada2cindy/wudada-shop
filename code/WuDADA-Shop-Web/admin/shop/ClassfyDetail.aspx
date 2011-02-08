<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="ClassfyDetail.aspx.cs" Inherits="admin_shop_ClassfyDetail" %>

<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajax:Loading ID="Loading" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelUI" runat="server">
                <br />
                <table border="0" cellpadding="0" cellspacing="0" width="96%">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="updateDataTable">
                                <tr>
                                    <th colspan="2" class="updateDateHeader">
                                        (<font color="#FF0000">*</font>為必填欄位)
                                    </th>
                                </tr>
                                <tr>
                                    <td class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>名稱：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:TextBox ID="txtName" runat="server" Width="550px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                            Display="None" ErrorMessage="名稱 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="updateDateContent" align="right" height="30px">
                                        啟用：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:CheckBox ID="ckbIsEnable" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" height="30px" valign="top">
                                        最後更新：
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUpdateTime" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="updateDateBtnTR" align="center" colspan="2" height="50px">
                                        <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" ValidationGroup="Save"
                                            CssClass="btn" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="修改" ValidationGroup="Save" CssClass="btn"
                                            OnClick="btnUpdate_Click" />
                                        &nbsp;<asp:Button ID="btnBack" runat="server" CausesValidation="False" OnClick="btnBack_Click"
                                            CssClass="btn" Text="回清單" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Save" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hdnId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

