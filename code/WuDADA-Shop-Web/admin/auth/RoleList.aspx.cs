using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate.Criterion;
using com.wudada.web.util.page;
using com.wudada.console.service.system;
using Spring.Context;
using Spring.Context.Support;
using Common.Logging;
using com.wudada.console.service.auth;
using com.wudada.console.service.auth.vo;
using com.wudada.web.page;
using com.wudada.console.service.common.vo;


public partial class admin_auth_RoleList : BasePage
{    
    IAuthService authService;    
    string DETAIL_URL = "RoleDetail.aspx";

    protected new void Page_Load(object sender, EventArgs e)
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
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(LoginRole));

        string search1 = txtSearch1.Text.Trim();
        if (!string.IsNullOrEmpty(search1))
        {
            dCriteria.Add(Expression.Like("RoleName", search1, MatchMode.Anywhere));
        }

        dCriteria.AddOrder(Order.Asc("RoleName"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        lblMsg.Text = string.Format("<span class='searchAlterTxt'>(共有</span> {0} <span class='searchAlterTxt'>筆資料)</span>", AspNetPager1.RecordCount);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        GridView1.DataSource = myService.ExecutableDetachedCriteria<LoginRole>(dCriteria, startIndex, maxRecord);
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
                LoginRole loginRole = authService.myService.DaoGetVOById<LoginRole>(int.Parse(e.CommandArgument.ToString()));

                int roleId = int.Parse(e.CommandArgument.ToString());
                         
                if (loginRole.BelongUsers != null && loginRole.BelongUsers.Count > 0)
                {

                    lblMsg.Text = "有使用者存在於群組中";

                    return;
                }
                if (loginRole.MenuFuncs != null && loginRole.MenuFuncs.Count > 0)
                {
                    lblMsg.Text = "有功能選單與群組關聯";
                    return;
                }
                
                authService.myService.DaoDelete(loginRole);

                string jsStr = JavascriptUtil.AlertJS(MsgVO.DELETE_OK);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", jsStr, false);
                btnSearch_Click(null, null);
                break;

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(DETAIL_URL);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

}
