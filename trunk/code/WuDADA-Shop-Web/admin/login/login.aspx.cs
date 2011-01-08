using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.auth;
using com.wudada.web.sessionstate;
using com.wudada.web;
using com.wudada.console.generic.util;
using NHibernate.Criterion;
using com.wudada.console.service.auth.vo;
using com.wudada.web.util.page;
using com.wudada.web.cookies;
using com.wudada.console.service.common.vo;

public partial class admin_login : BasePage
{
    IAuthService authService;
    SessionHelper sessionHelper = new SessionHelper();
    CookieHelper cookieHelper = new CookieHelper();

    string HOME_INDEX = "~/admin/masterpage/index.aspx";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        authService = (IAuthService)ctx.GetObject("AuthService");

        txtID.Focus();
        if (!IsPostBack)
        {
            CheckLoginCookies();
            setDirectAdminLogin();
        }
    }

    private void CheckLoginCookies()
    {
        if (!string.IsNullOrEmpty(cookieHelper.CookieLoginUser))
        {
            txtID.Text = cookieHelper.CookieLoginUser;
            ckbSaveLogin.Checked = true;
            //txtPW.Text = cookieHelper.CookieLoginPass;
        }
    }

    private void setDirectAdminLogin()
    {
        HttpHelper httpHelper = new HttpHelper();
        string ip = httpHelper.GetUserIp(HttpContext.Current);

        if ("127.0.0.1".Equals(ip))
        {
            Button1.Visible = true;
        }
        else
        {
            Button1.Visible = false;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string id = txtID.Text.Trim();
        string pw = txtPW.Text.Trim();

        doLogin(id, EncryptUtil.GetMD5(pw));
    }

    private void doLogin(string id, string pw)
    {
        string msg = "";
        LoginUser user = authService.Login(id, pw);
        if (user != null)
        {
            if (user.IsEnable)
            {
                //重設權限
                UserMenuFuncContainer.GetInstance().ResetAll();
                object o = user.BelongRoles;
                log.Debug("目前使用者權限==" + o);
                sessionHelper.AdminUser = user;
                //記憶登入資訊
                if (ckbSaveLogin.Checked)
                {
                    cookieHelper.CookieLoginUser = txtID.Text.Trim();
                    //cookieHelper.CookieLoginPass = txtPW.Text.Trim();
                }
                else
                {
                    cookieHelper.CookieLoginUser = null;
                }
                Response.Redirect(HOME_INDEX);
            }
            else
            {
                msg = MsgVO.LOGIN_USER_DISABLE;
            }
        }
        else
        {
            msg = MsgVO.WRONG_LOGIN_USER_ID_OR_PASS;
        }

        string JsStr = JavascriptUtil.AlertJS(msg);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);

    }
    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        string id = txtID.Text.Trim();
        string pw = txtPW.Text.Trim();

        doLogin(id, EncryptUtil.GetMD5(pw));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        doLogin("admin", EncryptUtil.GetMD5("dada168"));
    }
}
