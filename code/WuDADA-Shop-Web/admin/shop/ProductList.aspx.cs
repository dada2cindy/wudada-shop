using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.poss.vo;
using com.wudada.console.service.poss;
using com.wudada.console.generic.util;
using com.wudada.web.util.page;
using NHibernate.Criterion;
using com.wudada.console.service.common.vo;

public partial class admin_shop_ProductList : BasePage
{
    IPossService possService;
    string JsStr = "";
    readonly string LIST_URL = "ProductList.aspx";
    readonly string DETAIL_URL = "ProductDetail.aspx";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");

        if (!Page.IsPostBack)
        {
            SetControl();
            imgbtnSearch_Click(null, null);
        }
    }

    private void SetControl()
    {
        //分類
        ddlClassify.Items.Clear();
        ddlClassify.Items.Add(new ListItem("全部", ""));
        IList<ProductClassifyVO> classifyList = possService.GetAllProductClassifyVO(true);
        if (!CollectionUtil.IsNullOrEmpty(classifyList))
        {
            UIHelper.AddDropDowListItem(ddlClassify, classifyList.GetEnumerator(), "Name", "Id");
        }

        //品牌
        ddlBrand.Items.Clear();
        ddlBrand.Items.Add(new ListItem("全部", ""));
        IList<BrandVO> brandList = possService.GetAllBrandVO(true);
        if (!CollectionUtil.IsNullOrEmpty(brandList))
        {
            UIHelper.AddDropDowListItem(ddlBrand, brandList.GetEnumerator(), "Name", "Id");
        }
    }

    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(ProductVO));

        string search1 = txtSearch1.Text.Trim();
        if (!string.IsNullOrEmpty(search1))
        {
            dCriteria.Add(Expression.Like("Name", search1, MatchMode.Anywhere));
        }

        string search2 = txtSearch2.Text.Trim();
        if (!string.IsNullOrEmpty(search2))
        {
            dCriteria.Add(Expression.Like("SN", search2, MatchMode.Anywhere));
        }

        //啟用
        string isEnable = ddlIsEnable.SelectedValue;
        if (!string.IsNullOrEmpty(isEnable))
        {
            dCriteria.Add(Expression.Eq("IsEnable", bool.Parse(isEnable)));
        }

        //分類
        string classifyId = ddlClassify.SelectedValue;
        if (!string.IsNullOrEmpty(classifyId))
        {
            dCriteria.CreateCriteria("ClassifyList").Add(Expression.Eq("Id", int.Parse(classifyId)));
        }

        //品牌
        string brandId = ddlBrand.SelectedValue;
        if (!string.IsNullOrEmpty(brandId))
        {
            dCriteria.CreateCriteria("Brand").Add(Expression.Eq("Id", int.Parse(brandId)));
        }

        dCriteria.AddOrder(Order.Asc("SN"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        lblMsg.Text = string.Format("<span class='searchAlterTxt'>(共有</span> {0} <span class='searchAlterTxt'>筆資料)</span>", AspNetPager1.RecordCount);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        GridView1.DataSource = myService.ExecutableDetachedCriteria<ProductVO>(dCriteria, startIndex, maxRecord);
        GridView1.DataBind();
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(DETAIL_URL);
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        imgbtnSearch_Click(null, null);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "MyEdit":
                Response.Redirect(string.Format("{0}?id={1}", DETAIL_URL, e.CommandArgument.ToString()));
                break;
            case "MyDel":
                ProductVO productVO = myService.DaoGetVOById<ProductVO>(int.Parse(e.CommandArgument.ToString()));
                myService.DaoDelete(productVO);
                imgbtnSearch_Click(null, null);
                JsStr = JavascriptUtil.AlertJS(MsgVO.DELETE_OK);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
                break;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            Control ctrl = e.Row;
            ProductVO productVO = (ProductVO)e.Row.DataItem;

            //品牌
            if (productVO.Brand != null)
            {
                UIHelper.SetLabelText(ctrl, "lblBrand", productVO.Brand.Name);
            }
        }
    }
}

