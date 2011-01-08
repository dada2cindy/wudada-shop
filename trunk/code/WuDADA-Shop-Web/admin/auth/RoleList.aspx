﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master"
    AutoEventWireup="true" CodeFile="RoleList.aspx.cs" Inherits="admin_auth_RoleList" %>

<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajax:Loading ID="Loading" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <br />
            <table border="0" cellpadding="0" cellspacing="0" width="96%">
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="5" cellspacing="0" >
                            <tr>
                                <td colspan="4" bgcolor="#F0F0F0" class="searchConHeader" >
                                    查詢相關連結 
                                    <div class="add">
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" height="23" 
                                            ImageUrl="../images/add_btn.jpg" OnClick="btnAdd_Click" width="62" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td  >
                                    群組名稱 <asp:TextBox ID="txtSearchTitle" runat="server" Width="190px"></asp:TextBox>                                                     
                                </td>
                            </tr>
                            <tr>
                                <td  height="30px">
                                 
                                    <asp:ImageButton ID="imgbtnSearch" runat="server" 
                                        ImageUrl="../images/search_btn02.jpg" OnClick="btnSearch_Click" 
                                        ToolTip="查詢" />
                                </td>
                            </tr>
                        </table>
                     
                    </td>
                </tr>
            </table>
            <br />
               <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td bgcolor="#F0F0F0" >
                                    查詢結果<asp:Label ID="lblMsg" runat="server" CssClass="labelMsg" EnableViewState="False"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView runat="server" AutoGenerateColumns="False" Width="100%" ID="GridView1"
                                        EmptyDataText="無符合資料" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                        <RowStyle CssClass="gvRow" />
                                        <EmptyDataRowStyle CssClass="labelMsg" />
                                        <Columns>
                                            <asp:BoundField DataField="RoleName" HeaderText="群組">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="../images/brower_btn.jpg" ToolTip="修改" CausesValidation="false"  CommandName="MyEdit"
                                                        CommandArgument='<%# Eval("RoleId") %>' />
                                                    </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                             <asp:ImageButton ID="imgbtnDel" runat="server" ImageUrl="../images/delete_btn.jpg" ToolTip="刪除" CausesValidation="false"  CommandName="MyDel" OnClientClick="return confirm('確定刪除?')"
                                                        CommandArgument='<%# Eval("RoleId") %>' />
                                                </itemtemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td height="30px">
                                    <Webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageIndexOutOfRangeErrorMessage="頁索引超出範圍！"
                                        Width="100%" OnPageChanged="AspNetPager1_PageChanged" PageSize="15" CssClass="gvPage">
                                    </Webdiyer:AspNetPager>
                                </td>
                            </tr>
                        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
