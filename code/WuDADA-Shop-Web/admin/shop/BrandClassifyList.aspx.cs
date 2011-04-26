﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.wudada.web.page;
using com.wudada.console.service.system.vo;
using NHibernate.Criterion;
using com.wudada.console.service.common.vo;
using com.wudada.web.util.page;

public partial class admin_shop_BrandClassifyList : BasePage
{
    string JsStr = "";
    readonly string LIST_URL = "BrandClassifyList.aspx";
    readonly string DETAIL_URL = "BrandClassifyDetail.aspx";
    readonly string ITEM_PARAM_CLASSIFY = "品牌管理";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!Page.IsPostBack)
        {
            imgbtnSearch_Click(null, null);
        }
    }

    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(ItemParamVO));

        dCriteria.Add(Expression.Eq("Classify", ITEM_PARAM_CLASSIFY));

        string search1 = txtSearch1.Text.Trim();
        if (!string.IsNullOrEmpty(search1))
        {
            dCriteria.Add(Expression.Like("Name", search1, MatchMode.Anywhere));
        }

        //啟用
        string isDeleted = ddlDeleted.SelectedValue;
        if (!string.IsNullOrEmpty(isDeleted))
        {
            dCriteria.Add(Expression.Eq("Deleted", bool.Parse(isDeleted)));
        }

        dCriteria.AddOrder(Order.Asc("ListOrder"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        lblMsg.Text = string.Format("<span class='searchAlterTxt'>(共有</span> {0} <span class='searchAlterTxt'>筆資料)</span>", AspNetPager1.RecordCount);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        GridView1.DataSource = myService.ExecutableDetachedCriteria<ItemParamVO>(dCriteria, startIndex, maxRecord);
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
                ItemParamVO itemParamVO = myService.DaoGetVOById<ItemParamVO>(int.Parse(e.CommandArgument.ToString()));
                try
                {
                    myService.DaoDelete(itemParamVO);
                    imgbtnSearch_Click(null, null);
                    JsStr = JavascriptUtil.AlertJS(MsgVO.DELETE_OK);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    JsStr = JavascriptUtil.AlertJS("無法刪除，此品牌分類底下已有品牌，請將底下品牌的分類修改後才可刪除");
                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
                break;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowIndex != -1)
        //{
        //    Control ctrl = e.Row;
        //    ProductClassifyVO productClassifyVO = (ProductClassifyVO)e.Row.DataItem;

        //}
    }
}
