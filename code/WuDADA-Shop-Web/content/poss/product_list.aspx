<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage_White.master" AutoEventWireup="true" CodeFile="product_list.aspx.cs" Inherits="content_poss_product_list" %>

<%@ Register TagPrefix="Webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register Src="../userControls/Product_Brand_Classify.ascx" TagName="product_BC1" TagPrefix="ucProduct" %>
<%@ Register Src="../userControls/Product_Search.ascx" TagName="product_Search" TagPrefix="ucProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!-- ####################################################################################################### -->
<div class="wrapper col0" style="background-color:#333333;">
  <div id="topline" style="background-color:#333333;">
    <ul>
      <li class="first">您的位置</li>
      <li>&#187;</li>
      <li><a href="../masterpage/index.aspx">首頁</a></li>
      <li>&#187;</li>
      <li class="current"><a href="../poss/product_list.aspx">產品櫥窗</a></li>
    </ul>
    <ucProduct:product_Search id="ucProduct_Search1" runat="server" />
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
<div class="wrapper col3">
  <div class="container">
    <div class="content">
      <div id="topstory">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td valign="middle" class="title-01" width="100%">
                    <asp:Label ID="lblKey" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblListName" runat="server" Text="產品清單"></asp:Label>
                </td>
            </tr>
        </table>
        <ul>
            <asp:Repeater ID="Repeater1" runat="server" onitemdatabound="Repeater1_ItemDataBound">
                <ItemTemplate>                    
                    <asp:Literal ID="ltlPic" runat="server"></asp:Literal> 
                </ItemTemplate>
            </asp:Repeater> 
          <%--<li><a href="../poss/product_detail.aspx?id=1&cid=1&bid=1"><img src="../../common/PictureShow.ashx?type=fjx&fileName=xxx.jpg" alt="" width="190" height="130"/></a>
            <p>美國寶麗萊鏡片 鐵框雷朋造型</p>            
          </li>
          <li class="last"><a href="../poss/product_detail.aspx?id=1&cid=1&bid=1"><img src="../../common/PictureShow.ashx?type=fjx&fileName=xxx.jpg" alt="" width="190" height="130"/></a>
            <p>韓版復古風</p>            
          </li> --%>           
        </ul>
        <br class="clear" />
      </div>
        <div style="text-align: center;">
            <Webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageIndexOutOfRangeErrorMessage="頁索引超出範圍！"
                Width="100%" OnPageChanged="AspNetPager1_PageChanged" PageSize="12" CssClass="gvPage">
            </Webdiyer:AspNetPager>
        </div>
    </div>
    <div class="column">
      <div class="subnav">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td valign="middle" class="title-01" width="100%">
                    產品分類
                </td>
            </tr>
        </table>
        <ucProduct:product_BC1 id="ucProduct_BC1" runat="server" />
      </div>
    </div>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
    <asp:HiddenField ID="hdnCId" runat="server" />
    <asp:HiddenField ID="hdnBId" runat="server" />
    <asp:HiddenField ID="hdnBType" runat="server" />
    <asp:HiddenField ID="hdnKey" runat="server" />
</asp:Content>

