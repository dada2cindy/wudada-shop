<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="FunctionPathSet.aspx.cs" Inherits="admin_auth_FunctionPathSet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdate">
                <table style="border: 1px solid #58a960; width: 100%">
                    <tr>
                        <td height="30px" class="labelHeader">
                            <div align="center" id="title">
                                程式路徑設定</div>
                            <hr border="1" />
                        </td>
                    </tr>
                    <tr>
                        <td height="30px" class="labelHeader">
                            <div align="center">
                                功能群組：
                                <asp:DropDownList ID="ddlTopMenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dllTopMenu_SelectedIndexChanged"
                                    Width="150">
                                </asp:DropDownList>
                                &nbsp;&nbsp; 功能名稱：
                                <asp:DropDownList ID="ddlSubMenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubMenu_SelectedIndexChanged"
                                    Width="150">
                                </asp:DropDownList>
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td align="center" height="30px">
                            功能主要路徑：<asp:TextBox ID="txtMainPath" runat="server" Width="300"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMainPath"
                                ErrorMessage="*" ValidationGroup="gpMainPath"></asp:RequiredFieldValidator>
                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/admin/auth/images/btn_save.jpg"
                                OnClick="btnUpdate_Click" ValidationGroup="gpMainPath" />
                            <hr border="1" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnAdd">
                <table style="border: 1px solid #58a960; width: 100%">
                    <tr class="labelHeader">
                        <td align="center">
                            新增相關路徑：<asp:TextBox ID="txtPath" runat="server" Width="300"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPath"
                                ErrorMessage="*" ValidationGroup="gpInsert"></asp:RequiredFieldValidator>
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="images/btn_add.jpg" OnClick="btnAdd_Click"
                                Style="height: 20px" ValidationGroup="gpInsert" />
                        </td>
                        <tr>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvPath" runat="server" AutoGenerateColumns="False" Width="500px"
                                OnRowCommand="gvAuth_RowCommand">
                                <RowStyle CssClass="gvRow" />
                                <Columns>
                                    <asp:TemplateField HeaderText="編號">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="路徑">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("Id") %>' />
                                            <asp:TextBox ID="txtFPath" runat="server" Width="300" Text='<%# Eval("Path") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="images/btn_update.jpg"
                                                CommandName="MyUpdate" CommandArgument='<%# Bind("Id") %>' />
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="images/btn_delete.jpg"
                                                CommandName="MyDel" CommandArgument='<%# Bind("Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gvHeader" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="labelHeader">
                        <td align="center" width="34%">
                            &nbsp;
                            <asp:Label ID="lblMsg" runat="server" EnableViewState="false" CssClass="labelMsg"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
</asp:Content>

