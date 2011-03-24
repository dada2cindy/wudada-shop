<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage_White.master"
    AutoEventWireup="true" CodeFile="product_detail.aspx.cs" Inherits="content_poss_product_detail" %>

<%@ Register Src="../userControls/Product_Brand_Classify.ascx" TagName="product_BC1"
    TagPrefix="ucProduct" %>
<%@ Register Src="../userControls/Product_Search.ascx" TagName="product_Search" TagPrefix="ucProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--cloud zoom-->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/jquery-ui.min.js"></script>
    <link href="../../css/cloud-zoom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/cloud-zoom.1.0.2.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- ####################################################################################################### -->
    <div class="wrapper col0" style="background-color: #333333;">
        <div id="topline" style="background-color: #333333;">
            <ul>
                <li class="first">您的位置</li>
                <li>&#187;</li>
                <li><a href="../masterpage/index.aspx">首頁</a></li>
                <li>&#187;</li>
                <li class="current"><a href="../poss/brand_list.aspx">產品櫥窗</a></li>
            </ul>
            <ucProduct:product_Search ID="ucProduct_Search1" runat="server" />
            <br class="clear" />
        </div>
    </div>
    <!-- ####################################################################################################### -->
    <div class="wrapper col3" style="background-color: #FFFFFF;">
        <div class="container" style="background-color: #FFFFFF;">
            <div class="content">
            <asp:Panel ID="PanelUI" runat="server">
                <div id="topstory2">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td valign="middle" class="title-01" width="100%">
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="zoom-section">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-color: #FFFFFF;">
                            <tr style="border-color: #FFFFFF;">
                                <td style="border-color: #FFFFFF;" width="200">
                                    <div class="zoom-small-image">
                                        <asp:Literal ID="ltlMainImg" runat="server"></asp:Literal></div>
                                    <p style="text-align: center;">
                                        <b>產品編號：<asp:Label ID="lblSN" runat="server" Text=""></asp:Label></b>
                                    </p>
                                    <%--<div style="text-align: left;">
                                        中性潮流超酷個性金屬卯釘排列造型方框平光眼鏡...
                                    </div>--%>
                                </td>
                                <td style="border-color: #FFFFFF; float: left;">
                                    <div class="zoom-desc" style="width: 400px;">
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <div style="float: left;" id="productdetail_small_image">
                                                    <a href='../../common/PictureShow.ashx?fileName=<%#Eval("Path") %>&type=fjx' class='cloud-zoom-gallery'
                                                        title='' rel="useZoom: 'zoom1', smallImage: '../../common/PictureShow.ashx?fileName=<%#Eval("Path") %>&type=fjx&auto&size=200' ">
                                                        <img class="zoom-tiny-image" src="../../common/PictureShow.ashx?fileName=<%#Eval("Path") %>&type=fjx"
                                                            style="border-color: #FFFFFF;" /></a></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <%--<div style="float: left;">
                                            <a href='upload/pic/pic1_1.jpg' class='cloud-zoom-gallery' title='' rel="useZoom: 'zoom1', smallImage: 'upload/pic/pic1_2.jpg' ">
                                                <img class="zoom-tiny-image" src="upload/pic/pic1_3.jpg" style="border-color: #FFFFFF;"
                                                    width="70px" /></a></div>--%>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-color: #FFFFFF;">
                                    <p class="readmore">
                                        <a href="javascript:window.history.back()">Back &raquo;</a></p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br class="clear" />
                </div>
            </asp:Panel>
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
                    <ucProduct:product_BC1 ID="ucProduct_BC1" runat="server" />
                </div>
            </div>
            <br class="clear" />
        </div>
    </div>
    <!-- ####################################################################################################### -->
    <asp:HiddenField ID="hdnId" runat="server" />
</asp:Content>
