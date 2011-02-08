using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.information;
using com.wudada.console.var;
using com.wudada.web.util;
using com.wudada.console.service.information.vo;
using com.wudada.web.util.page;
using com.wudada.console.generic.util;

public partial class content_info_news_detail : BasePage
{
    IInformationService informationService;   
    string JsStr = "";
    readonly string SHOW_PIC = ConfigHelper.PictureShow;
    readonly string PIC_DIR = ConfigHelper.FjxFileURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        informationService = (IInformationService)ctx.GetObject("InformationService");

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
        NewsVO newsVO = myService.DaoGetVOById<NewsVO>(id);

        UIHelper.FillUI(PanelUI, newsVO);
        lblPublicDate.Text = ConvertUtil.ConvertDateToStr(newsVO.PublicDate);

        //圖片
        if (!string.IsNullOrEmpty(newsVO.ImgPath))
        {
            imgImgPath.ImageUrl = string.Format("{0}?type=fjx&fileName={1}", SHOW_PIC, newsVO.ImgPath);
        }

        //廣告
        ucAdInfo1.RP_Bind(VarHelper.WuDADA_ITEMPARAM_ID_AD_INFO3);
    }
}
