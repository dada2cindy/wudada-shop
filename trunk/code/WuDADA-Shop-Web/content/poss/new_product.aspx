<%@ Page Title="" Language="C#" MasterPageFile="~/content/masterpage/MasterPage.master" AutoEventWireup="true" CodeFile="new_product.aspx.cs" Inherits="content_poss_new_product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="../../css/imageflow.packed.css" type="text/css" />
    <script type="text/javascript" src="../../js/imageflow.packed.js"></script>
    <script type="text/javascript" defer="defer">
        domReady(function() {
            var basic_2 = new ImageFlow();
            basic_2.init({
                ImageFlowID: 'basic_2'
                , slider: false                      //滾桿
                , circular: true                     //圓形的slide 不會有左右到底
                , buttons: false                     //左右按鈕
                , slideshow: true                    //幻燈片播放
                , slideshowAutoplay: true            //自動播放
                , slideshowSpeed: 3000               //播放時間(毫秒)
                , glideToStartID: false
                , imageScaling: true                   //影像縮放切換 
                , imageFocusMax: 5                    //左右的顯示張數
                , imagesM: 1                       //所有影像的顯示比例
                , startID: 3                         //從第幾張開始
	            , imageFocusM: 1                      //圖片的顯示比例
	            , xStep: 130                          //圖片x軸間距
                , opacity: true                       //透明效果
				, opacityArray: [10, 5, 3, 1]            //透明效果設定10~1
                , reflectionP: 0.25                   //透明圖片高度縮放比例
                //, onClick: function() { window.open(this.url, '_blank'); }  //連結另開視窗
                , onClick: function() { window.open("../ad/default.aspx?id=" + this.id, '_blank'); }  //連結另開視窗
                , reflections: true
            });

        });
	</script>
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
      <li class="current"><a href="../poss/new_product.aspx">時尚新品</a></li>
    </ul>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
<div class="wrapper col3">
  <div class="container">
    <div class="content_imgflow">
      <!--<h2>新品</h2>-->
      <div id="basic_2" class="imageflow" style="background-color: #000000; width: 100%; color: #FFFFFF;">
          <asp:Repeater ID="Repeater1" runat="server">
              <ItemTemplate>
                <img id='<%#Eval("Id") %>' src="../../common/PictureShow.ashx?fileName=<%#Eval("ImgPath") %>&type=adpic" longdesc="../../common/PictureShow.ashx?fileName=<%#Eval("ImgPath") %>&type=adpic" alt='<%#Eval("Name") %>' />
                <%--<img id="1" src="../../images/demo/new1.jpg" longdesc="../../images/demo/new1.jpg" alt="潮流眼鏡" />--%>
              </ItemTemplate>
          </asp:Repeater>			
	  </div>
    </div>
    <br class="clear" />
  </div>
</div>
<!-- ####################################################################################################### -->
</asp:Content>

