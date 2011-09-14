using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.advertisement;
using com.wudada.console.var;
using com.wudada.web.util;
using com.wudada.console.service.advertisement.vo;
using NHibernate.Criterion;
using com.wudada.web.util.page;

public partial class content_masterpage_index : BasePage
{
    IAdService adService;
    readonly int LIST_COUNT = 5;
    readonly int PARAM_ID_CLASSIFY = VarHelper.WuDADA_ITEMPARAM_ID_AD_INFO1;
    readonly string PIC_DIR = ConfigHelper.AdPicURL;
    readonly string SHOW_PIC = ConfigHelper.PictureShow;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        adService = (IAdService)ctx.GetObject("AdService");

        if (!Page.IsPostBack)
        {
            RP_Bind();
        }
    }

    private void RP_Bind()
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(AdInfoVO));

        //分類
        dCriteria.CreateCriteria("Classify").Add(Expression.Eq("Id", PARAM_ID_CLASSIFY));

        //啟用
        dCriteria.Add(Expression.Eq("IsEnable", true));

        dCriteria.AddOrder(Order.Asc("ListOrder"));

        Repeater1.DataSource = myService.ExecutableDetachedCriteria<AdInfoVO>(dCriteria, 0, LIST_COUNT);
        Repeater1.DataBind();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            AdInfoVO adInfoVO = (AdInfoVO)e.Item.DataItem;

            //圖片
            UIHelper.ImageImgUrl(ctrl, "imgPic", string.Format("{0}?fileName={1}&type=adpic&auto=w&size=930", SHOW_PIC, adInfoVO.ImgPath));
        }
    }
}
