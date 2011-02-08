<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="content_masterpage_about" %>

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
      <li class="current"><a href="../masterpage/about.aspx">關於新明</a></li>
    </ul>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
<div class="wrapper col4">
  <div id="footer" style="background-color: #000000; ">
    <div class="box1">
      <h2>關於新明</h2>
        <asp:Literal ID="ltlComInfo1" runat="server"></asp:Literal>
    </div>
    <div class="box contactdetails">
      <h2>聯絡資訊</h2>
        <asp:Literal ID="ltlComInfo2" runat="server"></asp:Literal>
    </div>
    <div class="box flickrbox">
      <h2>地理位置</h2>
      <div class="wrap">          
        <iframe width="260" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com.tw/maps?f=q&amp;source=s_q&amp;hl=zh-TW&amp;geocode=&amp;q=%E5%8F%B0%E5%8C%97%E5%B8%82%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F1%E6%A8%93&amp;aq=&amp;sll=25.043265,121.518188&amp;sspn=0.014503,0.032444&amp;brcurrent=3,0x3442a93f510c7ba1:0x731637f5caca2004,0,0x3442ac6b61dbbd9d:0xc0c243da98cba64b&amp;ie=UTF8&amp;hq=&amp;hnear=108%E5%8F%B0%E5%8C%97%E5%B8%82%E8%90%AC%E8%8F%AF%E5%8D%80%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F&amp;z=14&amp;ll=25.045922,121.508085&amp;output=embed"></iframe><br /><small><a target="_blank" href="http://maps.google.com.tw/maps?f=q&amp;source=embed&amp;hl=zh-TW&amp;geocode=&amp;q=%E5%8F%B0%E5%8C%97%E5%B8%82%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F1%E6%A8%93&amp;aq=&amp;sll=25.043265,121.518188&amp;sspn=0.014503,0.032444&amp;brcurrent=3,0x3442a93f510c7ba1:0x731637f5caca2004,0,0x3442ac6b61dbbd9d:0xc0c243da98cba64b&amp;ie=UTF8&amp;hq=&amp;hnear=108%E5%8F%B0%E5%8C%97%E5%B8%82%E8%90%AC%E8%8F%AF%E5%8D%80%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F&amp;z=14&amp;ll=25.045922,121.508085" style="color:#0000FF;text-align:left">檢視較大的地圖</a></small>
      </div>
    </div>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
</asp:Content>

