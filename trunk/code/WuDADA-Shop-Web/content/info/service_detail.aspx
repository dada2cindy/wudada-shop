<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage_White.master" AutoEventWireup="true" CodeFile="service_detail.aspx.cs" Inherits="content_info_service_detail" %>

<%@ Register Src="../userControls/AdInfo1.ascx" TagName="adInfo1" TagPrefix="ucAd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!--lightbox-->
    <link rel="stylesheet" href="../../css/lightbox.css" type="text/css" media="screen" />
    <script src="../../js/prototype.js" type="text/javascript"></script>
    <script src="../../js/scriptaculous.js?load=effects,builder" type="text/javascript"></script>
    <script src="../../js/lightbox.js" type="text/javascript"></script>
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
      <li class="current"><a href="../info/service_list.aspx">眼鏡與我</a></li>
    </ul>
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
                      <asp:Label ID="lblPublicDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div>    	  
        <table border="0" cellpadding="0" cellspacing="0" style="border-color:#FFFFFF;">
            <tr style="border-color:#FFFFFF;">
                <td style="border-color:#FFFFFF; float:left;" width="200">
                    <div>
                        <asp:Literal ID="ltlMainImg" runat="server"></asp:Literal>                        
                    </div>
                    <div>   
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <%--<div style="float:left;"><a href='../../common/PictureShow.ashx?fileName=<%#Eval("Path") %>&type=fjx' rel="lightbox[roadtrip]" title='<%#Eval("Note") %>'><img src='../../common/PictureShow.ashx?fileName=<%#Eval("Path") %>&type=fjx' style="border-color:#FFFFFF;" width="70px"/></a></div>--%>
                                <div style="float:left;"><a href='../../common/PictureShow.ashx?fileName=<%#Eval("Path") %>&type=fjx&auto=w&size=800' rel="lightbox[roadtrip]" title='<%#Eval("Note") %>'><img src="../../common/PictureShow.ashx?fileName=<%#Eval("Path") %>&type=fjx&auto=w&size=40" style="border-color:#FFFFFF;" height="30"/></a></div>                                          
                            </ItemTemplate>
                        </asp:Repeater>                                                
                    </div>
                </td>
                <td style="border-color:#FFFFFF;" width="100%">
                    <h2><asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
                    <asp:Literal ID="ltlContent" runat="server"></asp:Literal>                    
               </td>   
            </tr>
            <tr>
                <td colspan="2" style="border-color:#FFFFFF;">
                    <p class="readmore"><a href="../info/service_list.aspx">Back &raquo;</a></p>
                </td>
            </tr>
        </table>
        </div>
        <br class="clear" />
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

