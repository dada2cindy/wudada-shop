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
    <div class="box2">
      <h2>關於新明</h2>
        <asp:Literal ID="ltlComInfo1" runat="server"></asp:Literal>
    </div>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
</asp:Content>

