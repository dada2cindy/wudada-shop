<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileModify.aspx.cs" Inherits="common_upload_FileModify" %>

<%@ Register TagName="Loading" TagPrefix="ajax" Src="~/admin/common/AjaxLoading.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新明眼鏡管理後台-檔案上傳</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <base target="_self" />
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/upload.css" type="text/css" />
</head>
<body>
    <form id="formUpload" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />
    <ajax:Loading ID="Loading1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload"/>
        </Triggers>
        <ContentTemplate>
            <div class="upload">
                <h3>
                    File Upload</h3>
                <div>
                    <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;<asp:Button ID="btnUpload"
                        runat="server" Text="upload" OnClick="btnUpload_Click" />
                    <br />
                    &nbsp;
                    <div>
                        <div id="status" class="info">
                            請選擇上傳檔案</div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelUI" runat="server">
                <table style="font-size: 12px;" width="400px">
                    <tr>
                        <td>
                            <font color="#FF0000">*</font>檔案名稱:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="comment-form " Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            說明:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" CssClass="comment-form " Width="300px" Height="100px"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSure" runat="server" Text="確定" OnClick="btnSure_Click" ValidationGroup="Save" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hdnId" runat="server" />
            <asp:HiddenField ID="hdnTargetId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
