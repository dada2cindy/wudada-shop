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
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label></h2>
                    <p>
                        <asp:Label ID="lblDetail" runat="server" Text='<%#Eval("Detail") %>'></asp:Label></p>
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

