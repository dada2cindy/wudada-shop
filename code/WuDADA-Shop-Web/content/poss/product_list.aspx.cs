using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.poss;
using com.wudada.web.util;
using NHibernate.Criterion;
using com.wudada.console.service.poss.vo;
using com.wudada.console.generic.util;
using com.wudada.console.service.common.vo;
using com.wudada.web.util.page;
using NHibernate.SqlCommand;
using com.wudada.console.service.system.vo;

public partial class content_poss_product_list : BasePage
{
    IPossService possService;
    readonly string SHOW_PIC = ConfigHelper.PictureShow;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");

        if (!Page.IsPostBack)
        {
            //分類
            if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                hdnCId.Value = Request.QueryString["cid"];
            }
            //品牌類別
            if (!string.IsNullOrEmpty(Request.QueryString["bType"]))
            {
                hdnBType.Value = Request.QueryString["bType"];
            }
            //品牌
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
            {
                hdnBId.Value = Request.QueryString["bid"];
            }
            //關鍵字
            if (!string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                hdnKey.Value = Request.QueryString["key"];
            }
            LoadDataToUI();
        }
    }

    private void LoadDataToUI()
    {
        //清單文字
        string listName = "產品清單";
        if (!string.IsNullOrEmpty(hdnBType.Value))  //品牌類別
        {
            ItemParamVO itemParamVO = myService.DaoGetVOById<ItemParamVO>(ConvertUtil.ToInt32(hdnBType.Value));
            listName = string.Format("產品清單 - {0}", itemParamVO.Name);
        }
        else if (!string.IsNullOrEmpty(hdnBId.Value))  //品牌
        {
            BrandVO brandVO = myService.DaoGetVOById<BrandVO>(ConvertUtil.ToInt32(hdnBId.Value));
            listName = string.Format("產品清單 - {0} - {1}", brandVO.Classify.Name, brandVO.Name);
        }
        else if (!string.IsNullOrEmpty(hdnCId.Value))  //品牌類別
        {
            ProductClassifyVO productClassifyVO = myService.DaoGetVOById<ProductClassifyVO>(ConvertUtil.ToInt32(hdnCId.Value));
            listName = string.Format("產品清單 - {0}", productClassifyVO.Name);
        }
        lblListName.Text = listName;

        //關鍵字
        if (!string.IsNullOrEmpty(hdnKey.Value))
        {
            lblKey.Text = string.Format("符合關鍵字：<font color='red'>{0}</font>  的", hdnKey.Value);
        }

        RP_Bind();
    }

    private void RP_Bind()
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(ProductVO));
        DetachedCriteria dCriteria_Classify = dCriteria.CreateCriteria("ClassifyList", JoinType.LeftOuterJoin);
        DetachedCriteria dCriteria_Brand = dCriteria.CreateCriteria("Brand", JoinType.LeftOuterJoin);

        //分類
        if (!string.IsNullOrEmpty(hdnCId.Value))
        {
            dCriteria_Classify.Add(Expression.Eq("Id", ConvertUtil.ToInt32(hdnCId.Value)));
        }

        //品牌
        if (!string.IsNullOrEmpty(hdnBId.Value))
        {
            dCriteria_Brand.Add(Expression.Eq("Id", ConvertUtil.ToInt32(hdnBId.Value)));
        }

        //品牌類別
        if (!string.IsNullOrEmpty(hdnBType.Value))
        {
            dCriteria_Brand.CreateCriteria("Classify", JoinType.LeftOuterJoin).Add(Expression.Eq("Id", ConvertUtil.ToInt32(hdnBType.Value)));
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
                string cssStyle = "";
                if (((e.Item.ItemIndex + 1) % 3).Equals(0))
                {
                    cssStyle = "class='last'";
                }
                UIHelper.SetLiteralText(ctrl, "ltlPic", string.Format("<li {0}><a href='../poss/product_detail.aspx?id={1}&cid={2}&bid={3}&bType={4}'><img src='{5}?type=fjx&fileName={6}' alt='' width='190' height='130'/></a><p>{7}</p></li>"
                    , cssStyle, productVO.Id, hdnCId.Value, hdnBId.Value, hdnBType.Value, SHOW_PIC, fileVO.Path, productVO.Name));
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        RP_Bind();
    }
}
