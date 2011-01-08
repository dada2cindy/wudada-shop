<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master"
    AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="admin_auth_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAdd">
                <table style="width: 100%" class="admTable">
                    <tr>
                        <td height="30px" colspan="4" class="labelHeader">
                            <div id="title" align="center">
                                後台帳號管理</div>
                            <hr border="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30px" width="150px">
                            使用者帳號：
                        </td>
                        <td height="30px">
                            <asp:TextBox ID="txtId" runat="server" Width="160px"></asp:TextBox><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtId" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td height="30px" width="200px">
                            姓名：
                        </td>
                        <td height="30px" width="715">
                            <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" height="30px">
                            使用者密碼：
                        </td>
                        <td height="30px" class="style2">
                            <asp:TextBox ID="txtPw" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPw"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td height="30px">
                            再次輸入密碼：
                        </td>
                        <td height="30px" width="715">
                            <asp:TextBox ID="txtPw2" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPw2"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" height="30px" align="center">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/admin/auth/images/btn_add.jpg" OnClick="btnAdd_Click"
                                Height="20px" />
                            <asp:ImageButton ID="btnUpdate" runat="server" Height="20px" ImageUrl="~/admin/auth/images/btn_update.jpg"
                                OnClick="btnUpdate_Click" />
                            &nbsp;
                            <asp:ImageButton ID="btnReset" runat="server" ImageUrl="~/admin/auth/images/btn_re.jpg" OnClick="btnReset_Click"
                                Height="20px" CausesValidation="false" />
                            <asp:HiddenField ID="hdnVersion" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" height="30px" align="center">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtId"
                                ErrorMessage="帳號長度至少三碼" ValidationExpression=".{3,}"></asp:RegularExpressionValidator>
                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPw"
                                ControlToValidate="txtPw2" ErrorMessage="密碼不一致"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    DataKeyNames="UserId" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand"
                    AllowSorting="True">
                    <RowStyle CssClass="gvRow" />
                    <Columns>
                        <asp:BoundField DataField="UserId" HeaderText="帳號" ReadOnly="True">
                            <HeaderStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="姓名"></asp:BoundField>
                        <asp:TemplateField HeaderText="密碼">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='********'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="編輯" ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" CausesValidation="False" CommandName="MyEdit"
                                    ImageUrl="images/btn_edit.jpg" Text="編輯" CommandArgument='<%# Eval("UserId") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="MyDelete"
                                    ImageUrl="images/btn_delete.jpg" OnClientClick="return confirm('確定刪除?')" Text="刪除" CommandArgument='<%#Eval("UserId") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="設定權限" ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="images/btn_set.jpg"
                                    CausesValidation="false" CommandName="Select" CommandArgument='<%# Eval("UserId") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="gvPage" />
                    <HeaderStyle CssClass="gvHeader" />
                </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
