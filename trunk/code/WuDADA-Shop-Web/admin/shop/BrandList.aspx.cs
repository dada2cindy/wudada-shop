using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using NHibernate.Criterion;
using com.wudada.console.service.poss.vo;
using com.wudada.web.util.page;
using com.wudada.console.service.common.vo;
using com.wudada.console.service.system.vo;
using com.wudada.console.service.system;
using com.wudada.console.generic.util;

public partial class admin_shop_BrandList : BasePage
{
    ISystemService systemService;
    string JsStr = "";
    readonly string LIST_URL = "BrandList.aspx";
    readonly string DETAIL_URL = "BrandDetail.aspx";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        systemService = (ISystemService)ctx.GetObject("SystemService");

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
        IList<ItemParamVO> classifyList = systemService.GetAllItemParamVOByNoDel("品牌管理");
        if (!CollectionUtil.IsNullOrEmpty(classifyList))
        {
            UIHelper.AddDropDowListItem(ddlClassify, classifyList.GetEnumerator(), "Name", "Id");
        }
    }

    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(BrandVO));

        string search1 = txtSearch1.Text.Trim();
        if (!string.IsNullOrEmpty(search1))
        {
            dCriteria.Add(Expression.Like("Name", search1, MatchMode.Anywhere));
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
            dCriteria.CreateCriteria("Classify").Add(Expression.Eq("Id", int.Parse(classifyId)));
        }

        dCriteria.AddOrder(Order.Asc("ListOrder"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        lblMsg.Text = string.Format("<span class='searchAlterTxt'>(共有</span> {0} <span class='searchAlterTxt'>筆資料)</span>", AspNetPager1.RecordCount);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        GridView1.DataSource = myService.ExecutableDetachedCriteria<BrandVO>(dCriteria, startIndex, maxRecord);
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
                BrandVO brandVO = myService.DaoGetVOById<BrandVO>(int.Parse(e.CommandArgument.ToString()));
                myService.DaoDelete(brandVO);
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
            BrandVO brandVO = (BrandVO)e.Row.DataItem;

            //分類
            if (brandVO.Classify != null)
            {
                UIHelper.SetLabelText(ctrl, "lblClassify", brandVO.Classify.Name);
            }
        }
    }
}
