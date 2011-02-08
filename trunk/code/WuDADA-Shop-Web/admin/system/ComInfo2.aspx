<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="ComInfo2.aspx.cs" Inherits="admin_system_ComInfo2" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="PanelUI" runat="server" DefaultButton="btnSave">
        <br />
        <table width="100%" border="0" cellpadding="5" cellspacing="0" class="updateDataTable">
            <tbody>
                <tr>
                    <th class="updateDateHeader" colspan="2">
                        (<font color="#FF0000">*</font>為必填欄位)
                    </th>
                </tr>                
                <tr>
                    <td align="right" height="40px" valign="top">
                        <font color="#FF0000">*</font>內容：
                    </td>
                    <td>
                        <FCKeditorV2:FCKeditor ID="fckValue" runat="server" Width="550px" Height="500px">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" Text="更新"
                            ValidationGroup="Save" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="Save" />
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
</asp:Content>

