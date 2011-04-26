<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="shop_info.aspx.cs" Inherits="content_masterpage_shop_info" %>

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
      <li class="current"><a href="../masterpage/shop_info.aspx">服務據點</a></li>
    </ul>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
<div class="wrapper col4">
  <div id="footer" style="background-color: #000000; ">
    <div class="box2">
      <h2>聯絡資訊</h2>
      <div class="contact_info">
          <asp:Literal ID="ltlSopInfo1" runat="server"></asp:Literal>
      <%--<ul>
        <li>中華店：台北市中華路一段76巷29號</li>
        <li>Tel: (02) 2371-4010</li>
      </ul>--%>
      <div class="wrap">     
        <iframe width="425" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com.tw/maps?f=q&amp;source=s_q&amp;hl=zh-TW&amp;geocode=&amp;q=%E5%8F%B0%E5%8C%97%E5%B8%82%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F1%E6%A8%93&amp;aq=&amp;sll=25.043265,121.518188&amp;sspn=0.014503,0.032444&amp;brcurrent=3,0x3442a93f510c7ba1:0x731637f5caca2004,0,0x3442ac6b61dbbd9d:0xc0c243da98cba64b&amp;ie=UTF8&amp;hq=&amp;hnear=108%E5%8F%B0%E5%8C%97%E5%B8%82%E8%90%AC%E8%8F%AF%E5%8D%80%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F&amp;z=14&amp;ll=25.045922,121.508085&amp;output=embed"></iframe><br /><small><a target="_blank" href="http://maps.google.com.tw/maps?f=q&amp;source=embed&amp;hl=zh-TW&amp;geocode=&amp;q=%E5%8F%B0%E5%8C%97%E5%B8%82%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F1%E6%A8%93&amp;aq=&amp;sll=25.043265,121.518188&amp;sspn=0.014503,0.032444&amp;brcurrent=3,0x3442a93f510c7ba1:0x731637f5caca2004,0,0x3442ac6b61dbbd9d:0xc0c243da98cba64b&amp;ie=UTF8&amp;hq=&amp;hnear=108%E5%8F%B0%E5%8C%97%E5%B8%82%E8%90%AC%E8%8F%AF%E5%8D%80%E4%B8%AD%E8%8F%AF%E8%B7%AF%E4%B8%80%E6%AE%B576%E5%B7%B729%E8%99%9F&amp;z=14&amp;ll=25.045922,121.508085" style="color:#0000FF;text-align:left">檢視較大的地圖</a></small>     
      </div>
      </div>      
      <div class="contact_info">
        <asp:Literal ID="ltlSopInfo2" runat="server"></asp:Literal>
      <%--<ul>
        <li>敦北店：台北市松山區敦化北路155巷9號</li>
        <li>Tel: (02) 2713-1113</li>
      </ul>--%>
      <div class="wrap">     
        <iframe width="425" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com.tw/maps?f=q&amp;source=s_q&amp;hl=zh-tw&amp;geocode=&amp;q=%E5%8F%B0%E5%8C%97%E5%B8%82%E6%9D%BE%E5%B1%B1%E5%8D%80%E6%95%A6%E5%8C%96%E5%8C%97%E8%B7%AF155%E5%B7%B79%E8%99%9F&amp;aq=&amp;sll=25.051974,121.556382&amp;sspn=0.016873,0.042272&amp;brcurrent=3,0x3442abf2b1d366ed:0x5d62b36fc2a1bb86,0,0x3442ac6b61dbbd9d:0xc0c243da98cba64b&amp;ie=UTF8&amp;hq=&amp;hnear=105%E5%8F%B0%E5%8C%97%E5%B8%82%E6%9D%BE%E5%B1%B1%E5%8D%80%E6%95%A6%E5%8C%96%E5%8C%97%E8%B7%AF155%E5%B7%B79%E8%99%9F&amp;z=14&amp;ll=25.054166,121.550412&amp;output=embed"></iframe><br /><small><a href="http://maps.google.com.tw/maps?f=q&amp;source=embed&amp;hl=zh-tw&amp;geocode=&amp;q=%E5%8F%B0%E5%8C%97%E5%B8%82%E6%9D%BE%E5%B1%B1%E5%8D%80%E6%95%A6%E5%8C%96%E5%8C%97%E8%B7%AF155%E5%B7%B79%E8%99%9F&amp;aq=&amp;sll=25.051974,121.556382&amp;sspn=0.016873,0.042272&amp;brcurrent=3,0x3442abf2b1d366ed:0x5d62b36fc2a1bb86,0,0x3442ac6b61dbbd9d:0xc0c243da98cba64b&amp;ie=UTF8&amp;hq=&amp;hnear=105%E5%8F%B0%E5%8C%97%E5%B8%82%E6%9D%BE%E5%B1%B1%E5%8D%80%E6%95%A6%E5%8C%96%E5%8C%97%E8%B7%AF155%E5%B7%B79%E8%99%9F&amp;z=14&amp;ll=25.054166,121.550412" style="color:#0000FF;text-align:left">檢視較大的地圖</a></small>
      </div>
      </div>
    </div>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
</asp:Content>
