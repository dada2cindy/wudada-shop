<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdInfo1.ascx.cs" Inherits="content_userControls_AdInfo1" %>
<div class="column">
    <div class="sponsors">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td valign="middle" class="title-01" width="100%">
                    熱賣商品
                </td>
            </tr>
        </table>
        <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <div class="b_250">
                <asp:HyperLink ID="hlinkMore" runat="server" NavigateUrl='<%#Eval("URL") %>' Target='<%#Eval("Target") %>'>                
                    <img src="../../common/PictureShow.ashx?fileName=<%#Eval("ImgPath") %>&type=adpic&auto=w&size=250" alt='<%#Eval("Name") %>'/></asp:HyperLink></div>
        </ItemTemplate>        
      </asp:Repeater>
    </div>
</div>
