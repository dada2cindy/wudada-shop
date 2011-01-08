<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjaxLoading.ascx.cs" Inherits="adm_common_AjaxLoading" %>
<%@ Register Assembly="ModalUpdateProgress" Namespace="com.pro2e.web" TagPrefix="pro2e" %>
<link href="../css/ajaxcss.css" rel="stylesheet" type="text/css" />
<pro2e:ModalUpdateProgess ID="ModalUpdateProgess1" runat="server" BackgroundCssClass="modalBackground" DropShadow="true">
    <ProgressTemplate>
        <div id="updateAnimation">
            <div align="center">
             <br />
             
                <asp:Image ID="Image1" runat="server" ImageUrl="~/admin/images/ajax-loader.gif" AlternateText="請稍候...." />
                <br />
                處理中 請稍候</div>
        </div>
    </ProgressTemplate>
</pro2e:ModalUpdateProgess>
