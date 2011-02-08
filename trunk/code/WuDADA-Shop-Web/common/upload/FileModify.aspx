<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileModify.aspx.cs" Inherits="common_upload_FileModify" %>

<%@ Register TagPrefix="fup" Namespace="OboutInc.FileUpload" Assembly="obout_FileUpload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新明眼鏡管理後台-檔案上傳</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <%--<meta http-equiv="expires" content="0" />--%>
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/upload.css" type="text/css" />
    
    <script type="text/JavaScript">
        function ProgressStarted() {
            document.getElementById("<%= uploadedFiles.ClientID %>").innerHTML = "";
            document.getElementById("upload").disabled = 'disabled';
            document.getElementById("<%=btnSureClientId %>").disabled = 'disabled';
        }
        function ProgressStopped() {
            document.getElementById("<%=btnSureClientId %>").disabled = '';
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />
    <div class="upload">
        <h3>
            File Upload</h3>
        <div>
            <input type="file" name="myFile1" style="width:400px;"/><br />&nbsp;
            <div>
                <div id="status" class="info" >
                    請選擇上傳檔案</div>
                <div class="commands">
                    <input type="submit" value="upload" name="mySubmit" id="upload"/><br />    
                </div>
            </div>
        </div>
    </div>  
    <%--<fup:FileUploadProgress
       runat                     = "server" 
       ID                        = "uploadProgress"
       OnClientProgressStopped   = "ProgressStopped"
       OnClientProgressStarted   = "ProgressStarted"
       OnClientSubmitting        = ""
       LocalizationFile          = "fup_localization/tw.xml"
       ShowUploadedFiles         = "true"                     
       InnerFiles                = "true"
       StyleFile                 = "css/myStyle.css"
     /> --%>    
    <fup:StatusPanel ID="StatusPanel1" runat="server">
        <fup:FileUploadProgress
       runat                     = "server" 
       ID                        = "uploadProgress"
       OnClientProgressStopped   = "ProgressStopped"
       OnClientProgressStarted   = "ProgressStarted"
       OnClientSubmitting        = ""
       LocalizationFile          = "fup_localization/tw.xml"
       ShowUploadedFiles         = "true"                     
       InnerFiles                = "true"
       StyleFile                 = "css/myStyle.css"
     /> 
        <asp:Label runat="server" ID="uploadedFiles" Text="" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelUI" runat="server">
                    <table style="font-size: 12px;" width="400px">
                        <tr>
                            <td>
                                檔案名稱:
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" CssClass="comment-form " Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                    ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:Button ID="btnSure" runat="server" Text="確定" OnClick="btnSure_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:HiddenField ID="hdnId" runat="server" />
                <asp:HiddenField ID="hdnTargetId" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </fup:StatusPanel>
    
    </form>
</body>
</html>
