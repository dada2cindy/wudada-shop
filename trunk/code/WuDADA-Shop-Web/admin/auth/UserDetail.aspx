<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master"
    AutoEventWireup="true" CodeFile="UserDetail.aspx.cs" Inherits="admin_auth_RoleDetail"
    ValidateRequest="false" %>

<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajax:Loading ID="Loading1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
            <br />
                <table border="0" cellpadding="0" cellspacing="0" width="96%">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="updateDataTable">
                                <tr>
                                   <th colspan="2" class="updateDateHeader"> <span id="ctl00_ContentPlaceHolder1_lblMsg" class="labelMsg"></span>
                                   (<font color='#FF0000'>*</font>為必填欄位)<asp:Label ID="lblMsg" runat="server" CssClass="labelMsg"
                                            EnableViewState="False"></asp:Label>
                                    </th>

                                </tr>
                                <tr>
                                    <td   class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>帳號：
                                    </td>
                                    <td  class="updateDateContent">
                                        <asp:TextBox ID="txtUserId" Width="100px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserId"
                                            Display="None" ErrorMessage="帳號不可空白" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td   class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>姓名：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:TextBox ID="txtName" Width="100px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName"
                                            Display="None" ErrorMessage="姓名不可空白" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td   class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>信箱：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:TextBox ID="txtEmail" Width="200px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                                            Display="None" ErrorMessage="信箱不可空白" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td   class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>啟用：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:RadioButtonList ID="rdoIsEnable" runat="server" 
                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                            <asp:ListItem Text="是" Value="True" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="updateDateContent" align="right" height="30px">
                                        權限：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:CheckBoxList ID="ckblRole" runat="server" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="updateDateContent" align="right" height="30px">
                                        更新密碼：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:CheckBox ID="ckbUpdatePass" runat="server" 
                                            oncheckedchanged="ckbUpdatePass_CheckedChanged" AutoPostBack="True" />
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlUpdatePass" runat="server" Visible="false">
                                    <tr>
                                        <td  class="updateDateContent" align="right" height="30px">
                                            <font color='#FF0000'>*</font>密碼：
                                        </td>
                                        <td class="updateDateContent">
                                            <asp:TextBox ID="txtPassword" Width="100px" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                                                Display="None" ErrorMessage="密碼不可空白" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="updateDateContent" align="right" height="30px">
                                            <font color='#FF0000'>*</font>再次確認密碼：
                                        </td>
                                        <td class="updateDateContent">
                                            <asp:TextBox ID="txtPassword2" Width="100px" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword2"
                                                Display="None" ErrorMessage="再次確認密碼 不可空白" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                                ControlToValidate="txtPassword2" ErrorMessage="密碼不一致" ValidationGroup="Insert"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td class="updateDateBtnTR" align="center" colspan="2" height="50px">
                                        <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" ValidationGroup="Insert" CssClass="btn"
                                           />
                                        <asp:Button ID="btnUpdate" runat="server" Text="修改" OnClick="btnUpdate_Click" ValidationGroup="Insert" CssClass="btn"
                                           />
                                        &nbsp;<asp:Button ID="btnBack" runat="server" CausesValidation="False" OnClick="btnBack_Click"
                                           Text="回清單" CssClass="btn" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Insert" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnId" runat="server" />
</asp:Content>

