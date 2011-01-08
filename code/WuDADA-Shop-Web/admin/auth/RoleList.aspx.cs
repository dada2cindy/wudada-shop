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


public partial class admin_auth_RoleList : System.Web.UI.Page
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IAuthService authService;    
    string DETAIL_PATE = "RoleDetail.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
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
        //注入條件
        DetachedCriteria detachedCriteria = GenerateRule();

        AspNetPager1.RecordCount = authService.myService.CountDetachedCriteriaRow(detachedCriteria);

        lblMsg.Text += " <span class='searchAlterTxt'>(共有</span> " + AspNetPager1.RecordCount + " <span class='searchAlterTxt'>筆資料)</span>";

        fillPagedGridView(GenerateRule());
    }

    private void fillPagedGridView(DetachedCriteria detachedCriteria)
    {
        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);
        fillGridView(detachedCriteria, startIndex, maxRecord);
    }

    private void fillGridView(DetachedCriteria detachedCriteria, int startIndex, int MaxRecord)
    {
        GridView1.DataSource = authService.myService.ExecutableDetachedCriteria<LoginRole>(detachedCriteria, startIndex, MaxRecord);
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "MyEdit":
                Response.Redirect(DETAIL_PATE + "?id=" + e.CommandArgument.ToString());
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

                string jsStr = JavascriptUtil.AlertJS("刪除成功");
                ScriptManager.RegisterClientScriptBlock(lblMsg, lblMsg.GetType(), "data", jsStr, false);
                btnSearch_Click(null, null);
                break;

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(DETAIL_PATE);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private DetachedCriteria GenerateRule()
    {
        DetachedCriteria query = DetachedCriteria.For(typeof(LoginRole));

        string searchClassify = txtSearchTitle.Text;

        if (!String.IsNullOrEmpty(searchClassify))
        {
            query.Add(Expression.Like("RoleName", "%" + searchClassify + "%"));
        }
      
        query.AddOrder(Order.Asc("RoleId"));


        return query;
    }
}
