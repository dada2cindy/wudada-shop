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
using NHibernate.Criterion;
using NHibernate.SqlCommand;

public partial class content_poss_brand_detail : BasePage
{
    IPossService possService;
    string JsStr = "";
    readonly string SHOW_PIC = ConfigHelper.PictureShow;
    readonly string PIC_DIR = ConfigHelper.FjxFileURL;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");

        if (!Page.IsPostBack)
        {
            //關鍵字
            if (!string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                hdnKey.Value = Request.QueryString["key"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
            {
                hdnBId.Value = Request.QueryString["bid"];
                LoadDataToUI(ConvertUtil.ToInt32(hdnBId.Value));
            }
        }
    }

    private void LoadDataToUI(int id)
    {
        BrandVO brandVO = myService.DaoGetVOById<BrandVO>(id);

        UIHelper.FillUI(PanelUI, brandVO);

        //圖片
        FileVO fileVO = possService.Get_FirstFile(brandVO);
        if (fileVO != null)
        {
            ltlMainImg.Text = string.Format("<img src='{0}?fileName={1}&type=fjx&auto=w&size=200' alt=''/>", SHOW_PIC, fileVO.Path);
        }

        //品牌下的所有產品
        RP_Bind();
    }

    private void RP_Bind()
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(ProductVO));
        DetachedCriteria dCriteria_Brand = dCriteria.CreateCriteria("Brand", JoinType.LeftOuterJoin);

        //品牌
        if (!string.IsNullOrEmpty(hdnBId.Value))
        {
            dCriteria_Brand.Add(Expression.Eq("Id", ConvertUtil.ToInt32(hdnBId.Value)));
        }

        //關鍵字
        if (!string.IsNullOrEmpty(hdnKey.Value))
        {
            dCriteria.Add(Expression.Like("Name", hdnKey.Value, MatchMode.Anywhere));
        }

        //啟用
        dCriteria.Add(Expression.Eq("IsEnable", true));

        dCriteria.AddOrder(Order.Asc("Name"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        Repeater1.DataSource = myService.ExecutableDetachedCriteria<ProductVO>(dCriteria, startIndex, maxRecord);
        Repeater1.DataBind();
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            ProductVO productVO = (ProductVO)e.Item.DataItem;

            //圖片
            FileVO fileVO = possService.Get_FirstFile(productVO);
            if (fileVO != null)
            {
                UIHelper.ImageImgUrl(ctrl, "imgPic", string.Format("{0}?type=fjx&fileName={1}&auto=w&size=70", SHOW_PIC, fileVO.Path));
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        RP_Bind();
    }
}
