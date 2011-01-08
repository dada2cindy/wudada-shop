﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="admin_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>WuDADA-EIP管理後台系統</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server" id="form1" defaultbutton="btnLogin">
    <div id="wrapper">
        <div id="container">
            <div id="top01">
                <div id="top01_inside">
                    <div id="top01_title">
                        <img src="../images/top_title.jpg" width="103" height="23" />
                    </div>
                    <div id="top01_login">
                        <a href="login.aspx">
                            <img src="../images/login_top.jpg" width="55" height="23" /></a>
                    </div>
                </div>
                <!-- top01_inside -->
            </div>
            <!-- End top01 -->
            <div id="top02">
                <div id="top02_inside">
                    <div id="top02_title">
                        <img src="../images/logo.jpg" width="250" height="63" />
                        <br />
                        WuDADA-EIP管理後台系統
                    </div>
                    <div id="top02_right">
                        <div id="login01">
                            <img src="../images/login01.jpg" width="76" height="27" />
                        </div>
                        <div id="pwd01">
                            <a href="forgetPass.aspx">忘記密碼</a></div>
                    </div>
                </div>
                <!-- top02_inside -->
            </div>
            <!-- End top02 -->
            <div id="content">
                <div id="content_inside">
                    <div id="login">
                        <div id="login_inside">
                            <table width="100%" border="0" cellpadding="1" cellspacing="1" class="no">
                                <tr>
                                    <td colspan="3">
                                        <img src="../images/login.jpg" width="56" height="30" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="41" colspan="3">
                                        <asp:TextBox ID="txtID" runat="server" Width="277px" MaxLength="30" CssClass="colum01"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="11" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <img src="../images/password.jpg" width="56" height="29" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtPW" runat="server" Width="277px" TextMode="Password" MaxLength="30"
                                            CssClass="colum01"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="2" colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="131" height="61">
                                        <%--<input name="" type="checkbox" value="" />--%>
                                        <asp:CheckBox ID="ckbSaveLogin" runat="server" />
                                        記住帳號<br />
                                        <img src="../images/line.jpg" width="124" height="11" /><br />
                                        <div id="pwd">
                                            <a href="forgetPass.aspx">忘記密碼</a></div>
                                    </td>
                                    <td width="143">
                                        <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="../images/login_btn.jpg"
                                            OnClick="btnLogin_Click" AlternateText="登入" Width="143" Height="61" />
                                    </td>
                                    <td width="38">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="直接以admin登入" />
                        </div>
                    </div>
                    <!-- End login -->
                </div>
            </div>
            <!-- End content -->
            <div id="footer">
                <div id="footer_inside">
                    © 2010 All rights Reserved. BestView：IE 7 & 1024 x 768<img src="../images/power.jpg"
                        width="204" height="39" />
                </div>
            </div>
            <!-- End footer -->
        </div>
        <!-- End container -->
    </div>
    <!-- End wrapper -->
    </form>
</body>
</html>

