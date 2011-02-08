<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage_White.master" AutoEventWireup="true" CodeFile="news_detail.aspx.cs" Inherits="content_info_news_detail" %>

<%@ Register Src="../userControls/AdInfo1.ascx" TagName="adInfo1" TagPrefix="ucAd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!-- ####################################################################################################### -->
<div class="wrapper col0">
  <div id="topline" style="background-color:#333333;">
    <ul>
      <li class="first">您的位置</li>
      <li>&#187;</li>
      <li><a href="../masterpage/index.aspx">首頁</a></li>
      <li>&#187;</li>
      <li class="current"><a href="../info/news_list.aspx">最新消息</a></li>
    </ul>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
<div class="wrapper col3">
  <div class="container">
    <div class="content">
        <asp:Panel ID="PanelUI" runat="server">
            <div id="latestnews" style="background-color: #FFFFFF;">
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td valign="middle" class="title-01" width="100%">
                            <asp:Label ID="lblPublicDate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <ul>
                    <li>
                        <div class="imgholder">
                            <asp:Image ID="imgImgPath" runat="server" Width="125" Height="125" /></div>
                        <div class="latestnews">
                            <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
                            <p class="readmore">
                                <a href="../info/news_list.aspx">Back &raquo;</a></p>
                        </div>
                        <br class="clear" />
                    </li>
                </ul>
            </div>
        </asp:Panel>
    </div>
    <ucAd:adInfo1 id="ucAdInfo1" runat="server" />
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
    <asp:HiddenField ID="hdnId" runat="server" />
</asp:Content>

