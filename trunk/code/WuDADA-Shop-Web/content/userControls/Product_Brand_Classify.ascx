<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Product_Brand_Classify.ascx.cs"
    Inherits="content_userControls_Product_Brand_Classify" %>
<ul>
    <li>
        <div style="float:right;">
            <asp:DropDownList ID="ddlShops" runat="server" DataTextField="Name" DataValueField="Id"
                AutoPostBack="true" OnSelectedIndexChanged="ddlShops_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <a href="../poss/brand_list.aspx">品牌</a>        
        <ul>
            <asp:Repeater ID="rptBrandType" runat="server" 
                onitemdatabound="rptBrandType_ItemDataBound">
                <ItemTemplate>
                    <li><a href='../poss/brand_list.aspx?bType=<%#Eval("Id") %>'><%#Eval("Name") %></a>
                        <ul>
                            <asp:Repeater ID="rptBrand" runat="server">
                                <ItemTemplate>
                                    <li><a href='../poss/brand_detail.aspx?bid=<%#Eval("Id") %>'><%#Eval("Name")%></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%--<li><a href="../poss/product_list.aspx?bid=1">興市</a></li>--%>
                        </ul>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <%--<li><a href="../poss/product_list.aspx?bType=1">日系品牌</a>
                <ul>
                    <li><a href="product_list.html">興市</a></li>
                    <li><a href="product_list.html">EFFECTOR</a></li>
                    <li><a href="product_list.html">YELLOW PLUS</a></li>
                    <li><a href="product_list.html">內田孝昭</a></li>
                </ul>
            </li>
            <li><a href="../poss/product_list.aspx?bType=2">歐系品牌</a>
                <ul>
                    <li><a href="product_list.html">GUCCI</a></li>
                    <li><a href="product_list.html">LV</a></li>
                </ul>
            </li>--%>
        </ul>
    </li>
    <li><a href="#">分類</a>
        <ul>
            <asp:Repeater ID="rptClassify" runat="server">
                <ItemTemplate>
                    <li><a href='../poss/product_list.aspx?cid=<%#Eval("Id") %>'>
                        <%#Eval("Name") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </li>
</ul>
