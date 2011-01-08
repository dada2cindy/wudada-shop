using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using com.wudada.web.sessionstate;
using com.wudada.console.service.auth;
using Spring.Context;
using Spring.Context.Support;
using com.wudada.console.service.auth.vo;
using com.wudada.web.treeview;
using com.wudada.console.service.auth.dao;
using com.wudada.web.util.page;
using com.wudada.console.generic.util;

public partial class admin_MasterPage : System.Web.UI.MasterPage
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    SessionHelper sessionHelper = new SessionHelper();
    string LOGIN_PAGE_MANAGER = "~/admin/login/login.aspx";
    AuthService authService;

    protected void Page_Load(object sender, EventArgs e)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
        authService = (AuthService)ctx.GetObject("AuthService");

        if (!Page.IsPostBack)
        {
            setPrgName();
            initMenu();
            initInfo();
        }
    }

    private void initMenu()
    {
        LoginUser user = sessionHelper.AdminUser;
        //快取載入
        UserMenuFuncContainer userContainer = UserMenuFuncContainer.GetInstance();

        if (user == null)
        {
            Response.Redirect(LOGIN_PAGE_MANAGER, false);
            return;
        }

        user = userContainer.GetUser(user.UserId);

        TreeveiwService tvService = new TreeveiwService();

        MenuFuncDao menuFuncDao = new MenuFuncDao();

        IList<MenuFunc> menuFuncList = authService.GetTopMenuFunc(user, userContainer.AllMenu, userContainer.RoleDic);

        foreach (MenuFunc menu in menuFuncList)
        {
            TreeNode treeNode = new TreeNode(menu.MenuFuncName, null, "", string.Format("javascript:__doPostBack('ctl00$tvMenu','t{0}')", menu.MenuFuncName), "");
            treeNode.Collapse();

            if (menu.SubFuncs.Count > 0)
            {
                foreach (MenuFunc subMenu in menu.SubFuncs)
                {
                    TreeNode subTreeNode = new TreeNode(subMenu.MenuFuncName, subMenu.MainPath);
                    //判斷使否選定
                    if (!string.IsNullOrEmpty(sessionHelper.FunctionName))
                    {
                        if (CollectionUtil.ContainValue(menu.SubFuncs, "MenuFuncName", sessionHelper.FunctionName))
                        {
                            treeNode.Expand();
                        }
                    }
                    treeNode.ChildNodes.Add(subTreeNode);
                }
            }
            tvMenu.Nodes.Add(treeNode);
        }

        //object c = userContainer.AllMenu;
        tvMenu.DataBind();
    }

    private void setPrgName()
    {
        hlinkPrgName.Text = sessionHelper.FunctionName;
        hlinkPrgName.NavigateUrl = sessionHelper.FunctionPath;
    }


    protected void tvMenu_Unload(object sender, EventArgs e)
    {
        //log.Debug("enter tvMenu_Unload");
        //new TreeViewState().SaveTreeView(tvMenu, this.GetType().ToString());
    }

    protected void imgLogout_Click(object sender, ImageClickEventArgs e)
    {
        sessionHelper.AdminUser = null;
        Response.Redirect(LOGIN_PAGE_MANAGER, false);
        return;
    }

    protected void imgLogout_Top_Click(object sender, ImageClickEventArgs e)
    {
        sessionHelper.AdminUser = null;
        Response.Redirect(LOGIN_PAGE_MANAGER, false);
        return;
    }

    protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        string selectedValue = tvMenu.SelectedNode.Text;
        string uri = tvMenu.SelectedNode.Value;
        log.Debug("selectedValue = " + selectedValue);
        if (selectedValue != string.Empty)
        {
            try
            {
                sessionHelper.FunctionName = selectedValue;
                Response.Redirect("~/" + tvMenu.SelectedNode.Value, false);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }

    private void initInfo()
    {
        if (sessionHelper.AdminUser != null)
        {
            lblUserId.Text = sessionHelper.AdminUser.UserId;

            //快取載入
            LoginUser user = UserMenuFuncContainer.GetInstance().GetUser(sessionHelper.AdminUser.UserId);

            IList<LoginRole> roleList = user.BelongRoles;

            List<string> roleStr = new List<string>();

            if (roleList != null && roleList.Count > 0)
            {
                foreach (LoginRole role in roleList)
                {
                    roleStr.Add(role.RoleName);
                }
            }

            lblRole.Text = String.Join(",", roleStr.ToArray());
        }
    }
}
