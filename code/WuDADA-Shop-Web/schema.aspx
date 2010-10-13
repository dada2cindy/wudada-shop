<%@ Page Language="C#" AutoEventWireup="true" CodeFile="schema.aspx.cs" Inherits="schema" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlLogin" runat="server" Visible="true">
            <table border="0" cellpadding="3" cellspacing="0" width="300px" 
                bgcolor="#FFFFFF" align="center">
                <tr align="left" valign="top">
                    <td colspan="2" style="text-align: center">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#0099FF" colspan="2" class="style1">
                    &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" height="21" valign="top" >
                        ‧密碼:
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#0099FF" colspan="2" style="text-align: center">
                        <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
                        <asp:HiddenField ID="hdnIsOK" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContent" runat="server" Visible="false">
         ===================================<br />
            覆蓋原程式：<br />
            <br />
            上傳zip，第一層不要有資料夾<br />
            <br />
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="更新" />
            <br />
            <asp:Label ID="lblMsgUpload" runat="server"></asp:Label>
            <br />
            ===================================<br />
            <asp:Button ID="btnGo" runat="server" Text="到登入頁" OnClick="btnGo_Click" Style="height: 21px" /><br />
            <br />
            &nbsp;<asp:Button ID="btnAddSchema" runat="server" Text="Add Schema" OnClick="btnAddSchema_Click" />
            <asp:Button ID="btnDropSchema" runat="server" Text="Drop Schema" OnClick="btnDropSchema_Click" />&nbsp;
            <asp:Button ID="btnGenDDL" runat="server" Text="Gen DDL" OnClick="btnGenDDL_Click"   />
            <br />
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
            <br />
            &nbsp; &nbsp;<br />
            <asp:Button ID="btnInitData" runat="server" Text="initData" 
                OnClick="btnInitData_Click" style="height: 21px" />
            &nbsp;<span lang="zh-tw">　　</span><br />
            
            <br />
            <br />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
