using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using Spring.Context.Support;
using Spring.Context;
using com.wudada.console.service.auth;
using com.wudada.console.service.auth.vo;
using NHibernate;
using com.wudada.console.service.common.vo;

public partial class admin_auth_User : System.Web.UI.Page
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IAuthService authService;

    protected void Page_Load(object sender, EventArgs e)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
        authService = (IAuthService)ctx.GetObject("AuthService");

        if (!IsPostBack)
        {
            initGV();
          
        }

        //新增模式
        ToInsertMode();
    }

    private void initGV()
    {
        GridView1.DataSource = authService.myService.DaoGetAllVO<LoginUser>();
        GridView1.DataBind();
    }

    private void ToInsertMode()
    {

        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        txtId.ReadOnly = false;
    }

    private void ToUpdateMode()
    {
        btnUpdate.Visible = true;
        btnAdd.Visible = false;
        txtId.ReadOnly = true;
    }

    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        clearInput();
    }
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = "";
        string id = txtId.Text;
        string pw = txtPw.Text;
        string pw2 = txtPw2.Text;
        string userName = txtName.Text;

        LoginUser user = authService.Get_LoginUser_ByUserId(id);

        if (user != null)
        {
            //lblMsg.Text = MsgVO.USER_ALREADY_EXIST;
            lblMsg.Text = MsgVO.EXIST_USER_ACCOUNT;
            return;
        }
        else
        {
            LoginUser newUser = new LoginUser();
            newUser.UserId = id;
            newUser.Password = id;
            newUser.Name = userName;
            authService.myService.DaoInsert(newUser);
            lblMsg.Text = MsgVO.INSERT_OK;
            clearInput();
            initGV();
        }

    }

    private void clearInput()
    {
        txtId.Text = "";
        txtPw.Text = "";
        txtPw2.Text = "";
        txtName.Text = "";

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        string id = txtId.Text;
        string pw = txtPw.Text;
        string pw2 = txtPw2.Text;
        string userName = txtName.Text;

        LoginUser user = authService.Get_LoginUser_ByUserId(id);

        user.Password = pw;
        user.Name = userName;

        try
        {
            authService.myService.DaoUpdate(user);

            lblMsg.Text = MsgVO.UPDATE_OK;

            clearInput();
            ToInsertMode();
            GridView1.DataBind();
        }
        catch (StaleObjectStateException ex)
        {
            log.Info(ex);
            lblMsg.Text = MsgVO.STALE_EXCEPTION_MSG;
            clearInput();
        }

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";

        switch (e.CommandName)
        {
            case "MyEdit":

                string userId = e.CommandArgument.ToString();
                log.Debug("get UserId=" + userId);
                loadVoToUI(userId);
                ToUpdateMode();
                break;

            case "Select":
                log.Debug("commandName= Select,get UserId=" + e.CommandArgument.ToString());
                Response.Redirect("~/admin/UserRoleSet/UserRoleSet.aspx?UserId=" + e.CommandArgument.ToString());
                break;

            case "MyDelete":
                string uid = e.CommandArgument.ToString();
                authService.myService.DaoDelete(authService.myService.DaoGetVOById<LoginUser>(uid));
                initGV();
                break;
        }
    }


    private void loadVoToUI(string userId)
    {
        LoginUser user = authService.Get_LoginUser_ByUserId(userId);
        txtId.Text = user.UserId;
        txtName.Text = user.Name;

        txtPw.Text = user.Password;
        txtPw2.Text = user.Password;
    }
}
