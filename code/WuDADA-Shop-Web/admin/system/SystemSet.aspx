<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="SystemSet.aspx.cs" Inherits="admin_system_SystemSet" %>

<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajax:Loading ID="Loading" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelUI" runat="server" DefaultButton="btnSave">
                <br />
                <table border="0" cellpadding="0" cellspacing="0" width="96%">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="5" cellspacing="0">
                                <tbody>
                                    <tr>
                                        <th colspan="2">
                                            (<span class="labelMsg">*</span>為必填欄位)
                                        </th>
                                    </tr>
                                    <tr>
                                        <td align="right" height="40px">
                                            <span class="labelMsg">*</span>EMail帳號：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAccount" runat="server" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAccount"
                                                Display="None" ErrorMessage="EMail帳號 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" height="40px">
                                            <span class="labelMsg">*</span>Email密碼：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" runat="server" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                Display="None" ErrorMessage="Email密碼 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" height="40px">
                                            <span class="labelMsg">*</span>MailSmtp：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMailSmtp" runat="server" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMailSmtp"
                                                Display="None" ErrorMessage="MailSmtp 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" height="40px">
                                            <span class="labelMsg">*</span>MailPort：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMailPort" runat="server" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMailPort"
                                                Display="None" ErrorMessage="MailPort 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" height="40px">
                                            <span class="labelMsg">*</span>寄件人Mail：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSendEmail" runat="server" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSendEmail"
                                                Display="None" ErrorMessage="寄件人Mail 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" height="40px">
                                            <span class="labelMsg">*</span>啟用SSL：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlIsEnableSSL" runat="server">
                                                <asp:ListItem Text="是" Value="True"></asp:ListItem>
                                                <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" ValidationGroup="Save"
                                                CssClass="btn" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Save" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnId" runat="server" />
</asp:Content>

