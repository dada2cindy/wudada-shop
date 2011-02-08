using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Criterion;

using com.wudada.web.util.page;
using com.wudada.console.service.system;
using com.wudada.console.service.auth;
using com.wudada.console.service.auth.vo;
using com.wudada.web.page;
using Spring.Context;
using Spring.Context.Support;
using com.wudada.console.service.common.vo;


public partial class admin_auth_UserList : BasePage
{    
    IAuthService authService;
   
    string DETAIL_URL = "UserDetail.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        authService = (IAuthService)ctx.GetObject("AuthService");

        if (!Page.IsPostBack)
        {
            btnSearch_Click(null, null);
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        btnSearch_Click(null, null);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(LoginUser));

        string search1 = txtSearch1.Text.Trim();
        if (!string.IsNullOrEmpty(search1))
        {
            dCriteria.Add(Expression.Like("UserId", search1, MatchMode.Anywhere));
        }

        dCriteria.AddOrder(Order.Asc("UserId"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        lblMsg.Text = string.Format("<span class='searchAlterTxt'>(共有</span> {0} <span class='searchAlterTxt'>筆資料)</span>", AspNetPager1.RecordCount);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        GridView1.DataSource = myService.ExecutableDetachedCriteria<LoginUser>(dCriteria, startIndex, maxRecord);
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "MyEdit":
                Response.Redirect(DETAIL_URL + "?id=" + e.CommandArgument.ToString());
                break;
            case "MyDel":

                //如果剩一筆 就不能允許刪除
                string jsStr = "";
                if (GridView1.Rows.Count == 1)
                {
                   jsStr= JavascriptUtil.AlertJS("資料只剩一筆不可以刪除");
                }
                else
                {
                    LoginUser loginUser = authService.myService.DaoGetVOById<LoginUser>(int.Parse(e.CommandArgument.ToString()));
                    authService.myService.DaoDelete(loginUser);
                    jsStr = JavascriptUtil.AlertJS(MsgVO.DELETE_OK);
                    btnSearch_Click(null, null);
                }

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", jsStr, false);           
                break;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(DETAIL_URL);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {     
        //if (e.Row.RowIndex != -1)
        //{
        //    Control ctrl = e.Row;
        //    string userId = UIHelper.FindHiddenValue(ctrl, "hdnId");          
        //}
    }

}
