using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using com.wudada.web.sessionstate;
using com.wudada.console.service.auth.vo;
using com.wudada.console.service.auth;
using Spring.Context;
using Spring.Context.Support;
using com.wudada.web.page;

public partial class admin_login_NoAuth : BasePage
{
    SessionHelper sHelper = new SessionHelper();
    IAuthService authService;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        authService = (IAuthService)ctx.GetObject("AuthService");

        if (!Page.IsPostBack)
        {    
        }
    }
}
