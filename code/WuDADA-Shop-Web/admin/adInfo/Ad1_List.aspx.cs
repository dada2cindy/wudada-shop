using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.var;
using NHibernate.Criterion;
using com.wudada.console.service.advertisement.vo;
using com.wudada.web.util.page;
using com.wudada.console.service.common.vo;
using com.wudada.console.generic.util;
using com.wudada.web.util;

public partial class admin_adInfo_Ad1_List : BasePage
{
    string JsStr = "";
    readonly string LIST_URL = "Ad1_List.aspx";
    readonly string DETAIL_URL = "Ad1_Detail.aspx";
    readonly int PARAM_ID_CLASSIFY = VarHelper.WuDADA_ITEMPARAM_ID_AD_INFO1;
    readonly string PIC_DIR = ConfigHelper.AdPicURL;

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
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(AdInfoVO));

        string search1 = txtSearch1.Text.Trim();
        if (!string.IsNullOrEmpty(search1))
        {
            dCriteria.Add(Expression.Like("Name", search1, MatchMode.Anywhere));
        }

        //分類
        dCriteria.CreateCriteria("Classify").Add(Expression.Eq("Id", PARAM_ID_CLASSIFY));

        //啟用
        string isEnable = ddlIsEnable.SelectedValue;
        if (!string.IsNullOrEmpty(isEnable))
        {
            dCriteria.Add(Expression.Eq("IsEnable", bool.Parse(isEnable)));
        }

        dCriteria.AddOrder(Order.Asc("ListOrder"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        lblMsg.Text = string.Format("<span class='searchAlterTxt'>(共有</span> {0} <span class='searchAlterTxt'>筆資料)</span>", AspNetPager1.RecordCount);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        GridView1.DataSource = myService.ExecutableDetachedCriteria<AdInfoVO>(dCriteria, startIndex, maxRecord);
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
                string fileName = "";
                AdInfoVO adInfoVO = myService.DaoGetVOById<AdInfoVO>(int.Parse(e.CommandArgument.ToString()));
                fileName = adInfoVO.ImgPath;
                myService.DaoDelete(adInfoVO);
                DoFileDelete(fileName);
                imgbtnSearch_Click(null, null);
                JsStr = JavascriptUtil.AlertJS(MsgVO.DELETE_OK);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
                break;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowIndex != -1)
        //{
        //    Control ctrl = e.Row;
        //    InfoVO infoVO = (InfoVO)e.Row.DataItem;

        //}
    }

    private bool DoFileDelete(string fileName)
    {
        bool result = true;

        try
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Server.MapPath(PIC_DIR);
                filePath += fileName;
                FileUploadHelper.DeleteUploadFile(filePath);
            }
        }
        catch (Exception ex)
        {
            result = false;
            log.Error(ex);
        }

        return result;
    }
}
