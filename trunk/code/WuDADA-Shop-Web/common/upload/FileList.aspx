<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileList.aspx.cs" Inherits="common_upload_FileList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新明眼鏡管理後台-檔案上傳</title>
    <link href="../../admin/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../admin/css/pager.css" rel="stylesheet" type="text/css" />
    <link href="../../admin/css/ajaxcss.css" rel="stylesheet" type="text/css" />
    <link href="../../admin/css/file.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">

        function DoShowModelDialog(path) {
            //window.open('FileModify_Fake.aspx', 'newwindow', 'height=100, width=100');
            var strTitle = window.showModalDialog(path, "MyDialog2", "dialogWidth=500px;dialogHeight=300px;dialogTop=100px");

            document.location.href = "FileList.aspx?" + document.getElementById("<%=hdnTargetClientId %>").value;
        }
    </script>
    <%--<script src="../../js/tbEvent.js" type="text/javascript"></script>--%>
    <%--<script type="text/javascript" src="../../js/jquery-1.4.1.min.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="btnAdd" runat="server" type="button" value="新增" onclick="DoShowModelDialog('FileModify.aspx?type=add')" />
        <br />
        <table width="100%" border="1" cellspacing="0" cellpadding="20" bordercolor="#f6f6f6"
            class="table-wide">
            <tr>
                <td align="center" bgcolor="#4F6E82" class="td-wide" width="20%" runat="server" id="tdH_Pic">
                    <span class="t09white">
                        <asp:Label ID="lblFileHead" runat="server" Text="檔名/圖片"></asp:Label></span>
                </td>
                <td align="center" bgcolor="#4F6E82" class="td-wide" width="60%">
                    <span class="t09white">說明</span>
                </td>
                <td align="center" bgcolor="#4F6E82" class="td-wide" width="10%">
                    
                </td>
                <td align="center" bgcolor="#4F6E82" class="td-wide" width="10%">
                    <%--<span class="t09white">刪除</span>--%>
                </td>
            </tr>
            <asp:Repeater ID="rpFile" runat="server" OnItemCommand="rpFile_ItemCommand"
                OnItemDataBound="rpFile_OnItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td align="left" class="td-wide" runat="server" id="td_Pic" >
                            <span class="t09white">
                                <asp:HyperLink ID="hlinkImage" runat="server" Visible="false" Target="_blank">
                                    <asp:Image ID="imgFile" runat="server" Width="70px" Height="70px" visible="false"/></asp:HyperLink>
                                <asp:HyperLink ID="hlinkFile" runat="server" Text='<%#Eval("Name") %>' NavigateUrl='<%#String.Format("../../common/download.ashx?mode=fjx&id={0}", DataBinder.Eval(Container,"DataItem.Id"))%>' visible="false"></asp:HyperLink>
                            </span>
                        </td>
                        <td align="left" class="td-wide">                            
                            <asp:Label ID="lblNote" runat="server" Text='<%#Eval("Note") %>'></asp:Label>                            
                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Id") %>' />
                        </td>                        
                        <td class="td-wide" align="center">
                            <input type="button" name="btnUpdate" id="btnUpdate" value="修改" onclick="DoShowModelDialog('FileModify.aspx?fid=<%#Eval("Id") %>')" />
                        </td>
                        <td class="td-wide" align="center">
                            <asp:Button ID="btnDel" runat="server" OnClientClick="return confirm('確定刪除?')" Text="刪除"
                                CommandName="MyDel" CommandArgument='<%#Eval("Id") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <asp:HiddenField ID="hdnId" runat="server" />
    <asp:HiddenField ID="hdnTarget" runat="server" />
    </form>
</body>
</html>
