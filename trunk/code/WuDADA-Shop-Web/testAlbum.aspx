<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testAlbum.aspx.cs" Inherits="testAlbum" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        用户名：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        密码：<asp:TextBox ID="TextBox2"
            runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />  
        <asp:Image ID="Image1" runat="server" />
        <asp:Image ID="Image2" runat="server" />
    </div>
    </form>
</body>
</html>
