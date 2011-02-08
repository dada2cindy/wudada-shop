<%@ Page Title="" Language="C#" MasterPageFile="~/admin/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="Ad1_Detail.aspx.cs" Inherits="admin_adInfo_Ad1_Detail" %>

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
                                        簡短說明：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:TextBox ID="txtDetail" runat="server" Width="550px"></asp:TextBox>                                        
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
                                    <td class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>圖片：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                                        <asp:Label ID="Label11" runat="server" CssClass="labelAlert" Text="最佳大小105x113"></asp:Label>
                                        <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                            <asp:Image ID="imgFile" runat="server" Width="105px" Height="113px" AlternateText="無圖片或圖片失效"/>
                                            <asp:Button ID="btnFileDel" runat="server" Text="刪除" OnClick="btnFileDel_Click" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="updateDateContent" align="right" height="30px">
                                        連結方式：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:RadioButtonList ID="rdoTarget" runat="server" RepeatDirection="Horizontal" Width="180px">
                                            <asp:ListItem Text="另開新視窗" Value="_blank"></asp:ListItem>
                                            <asp:ListItem Text="直接連結" Value="_self" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="updateDateContent" align="right" height="30px">
                                        <font color='#FF0000'>*</font>網址：
                                    </td>
                                    <td class="updateDateContent">
                                        <asp:TextBox ID="txtURL" runat="server" Width="550px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtURL"
                                            Display="None" ErrorMessage="網址 不可空白" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtURL"
                                            WatermarkText="ex：http://www.yahoo.com.tw" WatermarkCssClass="labelWatermark" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="網址格式不正確"
                                            Display="None" ControlToValidate="txtURL" ValidationGroup="Save" ValidationExpression="^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"></asp:RegularExpressionValidator>
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

