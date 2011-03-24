using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.advertisement.vo;
using com.wudada.console.generic.util;

public partial class content_ad_default : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                AdInfoVO adInfoVO = myService.DaoGetVOById<AdInfoVO>(ConvertUtil.ToInt32(Request.QueryString["id"]));
                if (adInfoVO != null && adInfoVO.IsEnable)
                {
                    Response.Redirect(adInfoVO.URL);
                }
            }
        }
    }
}
