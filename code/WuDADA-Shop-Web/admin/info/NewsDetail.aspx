<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="admin_info_NewsDetail" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajax:Loading ID="Loading" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
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
                                        <font color='#FF0000'>*</font>標題：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:TextBox ID="txtTitle" runat="server" Width="550px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                            Display="None" ErrorMessage="標題 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" height="30px">
                                        <font color="#FF0000">*</font>發布日期：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPublicDate" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                            Format="yyyy/MM/dd" PopupButtonID="butDay1" TargetControlID="txtPublicDate" />
                                        <asp:Image ID="butDay1" runat="server" AlternateText="開啟日曆，點選日期" ImageUrl="~/admin/images/calendar.gif" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPublicDate"
                                            Display="None" ErrorMessage="發布日期 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPublicDate"
                                            Display="Dynamic" ErrorMessage="起日期格式輸入不正確" ValidationGroup="Save" ValidationExpression="^((?:19|20)\d\d)[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>圖片：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                                        <asp:Label ID="Label11" runat="server" CssClass="labelAlert" Text="最佳大小(寬：125)"></asp:Label>
                                        <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                            <asp:Image ID="imgFile" runat="server" AlternateText="無圖片或圖片失效"/>
                                            <asp:Button ID="btnFileDel" runat="server" Text="刪除" OnClick="btnFileDel_Click" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" height="30px" valign="top">
                                        <font color="#FF0000">*</font>內容：
                                    </td>
                                    <td>
                                        <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Width="550px" Height="500px">
                                        </FCKeditorV2:FCKeditor>
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

