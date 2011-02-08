<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="content_masterpage_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!-- ####################################################################################################### -->
<div class="wrapper col2">
  <div id="featured_slide">
      <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <div class="featured_box">
                <div class="floater">
                    <h2>
                        韓版復古風</h2>
                    <p>
                        中性潮流超酷個性金屬卯釘排列造型方框平光眼鏡</p>
                </div>
                <p class="readmore">
                    <asp:HyperLink ID="hlinkMore" runat="server" NavigateUrl='<%#Eval("URL") %>' Target='<%#Eval("Target") %>'>更多 &raquo;</asp:HyperLink></p>
                <img src="../../common/PictureShow.ashx?fileName=<%#Eval("ImgPath") %>&type=adpic" alt="" /></div>
        </ItemTemplate>        
      </asp:Repeater>
  </div>
</div>
<!-- ####################################################################################################### -->
</asp:Content>

