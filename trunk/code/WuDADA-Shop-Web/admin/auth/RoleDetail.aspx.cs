using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.util.page;
using com.wudada.console.service.system;
using Spring.Context;
using com.wudada.web.sessionstate;
using Spring.Context.Support;
using Common.Logging;
using com.wudada.console.service.auth;
using com.wudada.console.service.auth.vo;

public partial class admin_auth_RoleDetail : System.Web.UI.Page
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
	IAuthService authService;
    SessionHelper sessionHelper = new SessionHelper();

    string LIST_PAGE = "RoleList.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
        authService = (IAuthService)ctx.GetObject("AuthService");
        

        if (!Page.IsPostBack)
        {
            ClearData();

            if (Request.QueryString["id"] != null)
            {
                UseUpdateMode();
                LoadDataToUI(int.Parse(Request.QueryString["id"].ToString()));
            }
            else
            {
                UseAddMode();
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        LoginRole role = new LoginRole();

        role.RoleName = txtRoleName.Text.Trim();
        
        authService.myService.DaoInsert(role);

        string JsStr = JavascriptUtil.AlertJSAndRedirect("新增成功", LIST_PAGE);
        ScriptManager.RegisterClientScriptBlock(lblMsg, lblMsg.GetType(), "data", JsStr, false);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        LoginRole loginRole = authService.myService.DaoGetVOById<LoginRole>(int.Parse(lblId.Text));

        loginRole.RoleName = txtRoleName.Text.Trim();

        authService.myService.DaoUpdate(loginRole);

        string JsStr = JavascriptUtil.AlertJSAndRedirect("更新成功",LIST_PAGE);
        ScriptManager.RegisterClientScriptBlock(lblMsg, lblMsg.GetType(), "data", JsStr, false);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_PAGE);
    }
    private void UseAddMode()
    {
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
    }

    private void UseUpdateMode()
    {
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
    }
    private void LoadDataToUI(int id)
    {
        LoginRole role = authService.myService.DaoGetVOById<LoginRole>(id);

        lblId.Text = role.RoleId.ToString();
        txtRoleName.Text = role.RoleName;      
    }
    private void ClearData()
    {
        lblId.Text = "";
    
    }
}
