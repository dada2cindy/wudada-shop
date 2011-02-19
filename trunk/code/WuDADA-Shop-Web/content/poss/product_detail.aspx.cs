using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.poss;
using com.wudada.web.util;
using com.wudada.console.generic.util;
using com.wudada.console.service.poss.vo;
using com.wudada.web.util.page;
using com.wudada.console.service.common.vo;

public partial class content_poss_product_detail : BasePage
{
    IPossService possService;
    string JsStr = "";
    readonly string SHOW_PIC = ConfigHelper.PictureShow;
    readonly string PIC_DIR = ConfigHelper.FjxFileURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                hdnId.Value = Request.QueryString["id"];
                LoadDataToUI(ConvertUtil.ToInt32(hdnId.Value));
            }
        }
    }

    private void LoadDataToUI(int id)
    {
        ProductVO productVO = myService.DaoGetVOById<ProductVO>(id);

        UIHelper.FillUI(PanelUI, productVO);

        //圖片
        FileVO fileVO = possService.Get_FirstFile(productVO);
        if (fileVO != null)
        {
            //主圖(第一章圖)
            ltlMainImg.Text = string.Format("<a href='{0}?fileName={1}&type=fjx' class = 'cloud-zoom' id='zoom1' rel=\"adjustX: 10,zoomWidth:390, adjustY:-4\"><img src='{0}?fileName={1}&type=fjx' alt='' width='200px'/></a>", SHOW_PIC, fileVO.Path);

            //全部圖
            Repeater1.DataSource = productVO.FileList;
            Repeater1.DataBind();
        }
    }
}