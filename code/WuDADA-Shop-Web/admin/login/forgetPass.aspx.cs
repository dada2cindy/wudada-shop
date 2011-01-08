using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.auth.vo;
using com.wudada.web.util.page;
using com.wudada.web.auth;
using com.wudada.web.mail;
using com.wudada.console.service.common.vo;
using com.wudada.console.service.auth;

public partial class admin_login_forgetPass : BasePage
{
    IAuthService authService;
    WuDADAMailService wudadaMailService;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        authService = (IAuthService)ctx.GetObject("AuthService");
        wudadaMailService = new WuDADAMailService();

    }

    protected void imgbtnForgetPass_Click(object sender, ImageClickEventArgs e)
    {
        string msg = MsgVO.WRONG_LOGIN_USER_ID_OR_EMAIL;

        if (!string.IsNullOrEmpty(txtId.Text.Trim()) && !string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            //LoginUser user = myService.DaoGetVOById<LoginUser>(txtId.Text.Trim());
            LoginUser user = authService.Get_LoginUser_ByUserId(txtId.Text.Trim());
            if (user != null)
            {
                if (user.Email.Equals(txtEmail.Text.Trim()))
                {
                    wudadaMailService.SendMail_ToLoginUser_ResetPass(user.UserId);
                    msg = MsgVO.RESET_LOGIN_USER_PASS;
                }
            }
        }

        string JsStr = JavascriptUtil.AlertJS(msg);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }
}
