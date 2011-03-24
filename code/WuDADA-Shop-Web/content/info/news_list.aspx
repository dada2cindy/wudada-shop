<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage_White.master" AutoEventWireup="true" CodeFile="news_list.aspx.cs" Inherits="content_info_news_list" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
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
      <div id="latestnews">
          <asp:Repeater ID="Repeater1" runat="server" 
              onitemdatabound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td valign="middle" class="title-01" width="100%">
                            <asp:Label ID="lblPublicDate" runat="server" Text='<%# Eval("PublicDate","{0:yyyy/MM/dd}") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
                <ul>
                    <li>
                        <div class="imgholder">
                            <img src="../../common/PictureShow.ashx?fileName=<%#Eval("ImgPath") %>&type=fjx&auto=w&size=125" alt="" /></div>
                        <div class="latestnews">
                            <h2>
                                <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label></h2>
                            <p>
                                <asp:Label ID="lblContent" runat="server" Text='<%#Eval("Content") %>'></asp:Label></p>
                            <p class="readmore">
                                <a href="news_detail.aspx?id=<%#Eval("Id") %>">More &raquo;</a></p>
                        </div>
                        <br class="clear" />
                    </li>
                </ul>
            </ItemTemplate>
          </asp:Repeater>
          <div style="text-align:center;">
          <Webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageIndexOutOfRangeErrorMessage="頁索引超出範圍！"
              Width="100%" OnPageChanged="AspNetPager1_PageChanged" PageSize="5" CssClass="gvPage">
          </Webdiyer:AspNetPager>
          </div>
      </div>
    </div>
    <ucAd:adInfo1 id="ucAdInfo1" runat="server" />
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
</asp:Content>

