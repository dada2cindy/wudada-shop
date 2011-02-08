using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.information;
using com.wudada.web.util;
using com.wudada.console.generic.util;
using com.wudada.console.service.information.vo;
using com.wudada.web.util.page;
using com.wudada.console.var;
using com.wudada.console.service.common.vo;

public partial class content_info_service_detail : BasePage
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
        InfoVO infoVO = myService.DaoGetVOById<InfoVO>(id);

        UIHelper.FillUI(PanelUI, infoVO);
        lblPublicDate.Text = ConvertUtil.ConvertDateToStr(infoVO.CreateTime);

        //圖片
        FileVO fileVO = informationService.Get_FirstFile(infoVO);
        if (fileVO != null)
        {
            //主圖(第一章圖)
            ltlMainImg.Text = string.Format("<a href='{0}?fileName={1}&type=fjx' class = 'cloud-zoom' id='zoom1' rel=\"adjustX: 10,zoomWidth:390, adjustY:-4\"><img src='{0}?fileName={1}&type=fjx' alt='' width='200px'/></a>", SHOW_PIC, fileVO.Path);

            //全部圖
            Repeater1.DataSource = infoVO.FileList;
            Repeater1.DataBind();
        }

        //廣告
        ucAdInfo1.RP_Bind(VarHelper.WuDADA_ITEMPARAM_ID_AD_INFO4);
    }
}
