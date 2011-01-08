<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="RoleDetail.aspx.cs" Inherits="admin_auth_RoleDetail" ValidateRequest="false" %>
<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajax:Loading ID="Loading1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <br />
           <table border="0" cellpadding="0" cellspacing="0" width="96%">
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="updateDataTable">
                            <tr>
                                 <th colspan="2" class="updateDateHeader"> (<font color="#FF0000">*</font>為必填欄位)<span id="ctl00_ContentPlaceHolder1_lblMsg" class="labelMsg"></span>
                                 <asp:Label ID="lblMsg" runat="server" CssClass="labelMsg" EnableViewState="False"></asp:Label>
                                  <asp:Label ID="Label1" runat="server" Text="序號：" Visible="false"></asp:Label>
                                       <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>
                                 </th>
                            </tr>
                          
                            <tr>
                                <td class="updateDateContent" align="right" height="30px"  >
                                    <font color='#FF0000'>*</font>群組名稱：
                                </td>
                                <td class="updateDateContent" >
                                    <asp:TextBox ID="txtRoleName" Width="123px" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoleName"
                                        Display="None" ErrorMessage="群組不可空白" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="updateDateBtnTR" align="center" colspan="2" height="50px">
                                    <asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" ValidationGroup="Insert"  CssClass="btn"/>
                                    <asp:Button ID="btnUpdate" runat="server" Text="修改" OnClick="btnUpdate_Click" ValidationGroup="Insert" CssClass="btn"/>
                                    &nbsp;<asp:Button ID="btnBack" runat="server" CausesValidation="False" OnClick="btnBack_Click" CssClass="btn"
                                        Text="回清單" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Insert" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

