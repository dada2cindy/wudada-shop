<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="UserRoleSet.aspx.cs" Inherits="admin_auth_UserRoleSet" %>
<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <ajax:Loading ID="Loading" runat="server" />
     
   <asp:Panel ID="Panel1" runat="server" DefaultButton="Button3">
   <br />
                <table  width="100%" border="0" cellpadding="5" cellspacing="0">
                    <tr>
                        <td height="30px" class="labelHeader"  bgcolor="#F0F0F0" colspan="3">
                            <div id="title" align="center">
                                帳號群組設定</div>
                        </td>
                    </tr>
                    <tr>
                        <td height="30px" colspan="3" class="labelHeader">
                            <div align="center">
                                使用者名稱：
                                <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                    <td height="30px" colspan="3" class="labelHeader">
                    <table class="labelHeader" align="center"  width="90%" >
                       
                        <th width="30%" height="30px" align="center">
                            可供設定群組
                        </th>
                        <th width="10%" align="center" class="style1">
                            設定操作
                        </th>
                        <th width="30%" align="center">
                            目前群組
                        </th>
                   
                     <tr class="labelHeader">
                        <td width="40%" height="50px" align="center">
                        <br />
                            <asp:ListBox ID="lblxToBeRole" runat="server" Height="98px" Width="246px" OnSelectedIndexChanged="lblxToBeRole_SelectedIndexChanged"
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                        <td align="center">
                        <br />
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="../images/btn_rightArrow.jpg"
                                OnClick="btnAdd_Click" />
                            <br />
                            <br />
                            <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="../images/btn_leftArrow.jpg"
                                OnClick="btnRemove_Click" />
                        </td>
                        <td width="40%" align="center">
                        <br />
                            <asp:ListBox ID="lbxHadRole" runat="server" Height="98px" Width="246px" OnSelectedIndexChanged="lbxHadRole_SelectedIndexChanged"
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                   
                    <tr class="labelHeader">
                        <td height="30px" colspan="3" align="center">
                      <%--  <asp:ImageButton ID="Button3" runat="server" ImageUrl="~/admin/auth/images/btn_save.jpg"
                                OnClick="Button3_Click" />--%>
                            <asp:Button ID="Button3" runat="server" Text="儲存" OnClick="Button3_Click" CssClass="btn" />
                               <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel> 
</asp:Content>

