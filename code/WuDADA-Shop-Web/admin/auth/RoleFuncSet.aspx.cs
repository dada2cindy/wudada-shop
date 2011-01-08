using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.console.service.auth;
using Common.Logging;
using Spring.Context;
using Spring.Context.Support;
using com.wudada.console.service.auth.vo;
using bgo.web.ui;
using com.wudada.web.util.page;
using com.wudada.console.generic.util;


public partial class admin_auth_RoleFuncSet : System.Web.UI.Page
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	IAuthService authService;

    protected void Page_Load(object sender, EventArgs e)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
        authService = (IAuthService)ctx.GetObject("AuthService");

        if (!Page.IsPostBack)
        {
            initData();
        }
    }

    private void initData()
    {
        initDDLRole();
        initDDLMenuFunc();
        initGridView();
    }

    private void initDDLMenuFunc()
    {
        IList<MenuFunc> MenuFuncList = authService.GetTopMenuFunc();

        foreach (MenuFunc menu in MenuFuncList)
        {
            ListItem item = new ListItem(menu.MenuFuncName, menu.Id.ToString());
            ddlMenuFunc.Items.Add(item);
        }
        ddlMenuFunc.Items.Add(new ListItem("全部", "0"));
        ddlMenuFunc.DataBind();
    }

    private void initDDLRole()
    {
        IList<LoginRole> roleList = authService.myService.DaoGetAllVO<LoginRole>(); ;

        foreach (LoginRole role in roleList)
        {
            ListItem item = new ListItem(role.RoleName, role.RoleId.ToString());
            ddlRole.Items.Add(item);
        }

        ddlRole.DataBind();
    }

    private void initGridView()
    {
        btnSearch_Click(null, null);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string selectedRole = ddlRole.SelectedValue;
        string selectedMenuFunc = ddlMenuFunc.SelectedValue;
        IList<RoleMenuShowVO> showVOList = new List<RoleMenuShowVO>();

        if (!string.IsNullOrEmpty(selectedRole))
        {
            LoginRole role = authService.myService.DaoGetVOById<LoginRole>(int.Parse(selectedRole));

            IList<MenuFunc> selectedMenuFuncList;

            if (selectedMenuFunc.Equals("0"))
            {
                selectedMenuFuncList = authService.GetNotTopMenuFunc();
            }
            else
            {
                selectedMenuFuncList = authService.GetMenuFuncByParentId(int.Parse(selectedMenuFunc));
            }

            foreach (MenuFunc menu in selectedMenuFuncList)
            {
                RoleMenuShowVO showVO = new RoleMenuShowVO();

                showVO.Id = menu.Id;
                showVO.Name = menu.MenuFuncName;
                showVO.No = menu.Note;

                if (CollectionUtil.ContainValue<MenuFunc>(role.MenuFuncs, "Id", menu.Id.ToString()))
                {
                    showVO.IsAuth = true;
                }
                else
                {
                    showVO.IsAuth = false;
                }

           
                             
                showVOList.Add(showVO);
            }

            gvAuth.DataSource = showVOList;
            gvAuth.DataBind();
        }
    }

    protected void ddlMenuFunc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(null, null);
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(null, null);
    }

    protected void ckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckAll = (CheckBox)sender;


        switch (ckAll.ID)
        {
            case "ckAll":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckIsAuth", ckAll.Checked);
                }
                break;

            case "ckAllQuery":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckQuery", ckAll.Checked);
                }
                break;
            case "ckAllInsert":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckInsert", ckAll.Checked);
                }
                break;
            case "ckAllUpdate":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckUpdate", ckAll.Checked);
                }
                break;
            case "ckAllDelete":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckDelete", ckAll.Checked);
                }
                break;
            case "ckAllPrint":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckPrint", ckAll.Checked);
                }
                break;

            case "ckAllExport":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckExport", ckAll.Checked);
                }
                break;
            case "ckAllElse":
                foreach (GridViewRow row in gvAuth.Rows)
                {
                    Control ctrl = row;
                    UIHelper.SetCheckBoxChecked(ctrl, "ckElse", ckAll.Checked);
                }
                break;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string selectedRole = ddlRole.SelectedValue;
        LoginRole role = authService.myService.DaoGetVOById<LoginRole>(int.Parse(selectedRole));

        foreach (GridViewRow row in gvAuth.Rows)
        {
            Control ctrl = row;
            bool isCheck = UIHelper.FindCheckBox(ctrl, "ckIsAuth");
            string hdnId = UIHelper.FindHiddenValue(ctrl, "hdnId");
            MenuFunc theFunc = authService.myService.DaoGetVOById<MenuFunc>(int.Parse(hdnId));

            if (isCheck)
            {
                if (role.MenuFuncs == null)
                {
                    role.MenuFuncs = new List<MenuFunc>();
                }

                if (!authService.RoleHasMenuRight(role, theFunc))
                {
                    role.MenuFuncs.Add(theFunc);
                }
                authService.myService.DaoUpdate(role);
            }
            else
            {
                if (authService.RoleHasMenuRight(role, theFunc))
                {
                    role.MenuFuncs.Remove(theFunc);
                }
                authService.myService.DaoUpdate(role);
            }

        
        }

        UserMenuFuncContainer.GetInstance().ResetAll();

        lblMsg.Text = "更新成功";
    }

   
}


namespace bgo.web.ui
{
    public class UC14Service
    {
        AuthService authService = new AuthService();
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public IList<RoleMenuShowVO> GetGridViewRow(string selectedRole, string selectedMenuFunc)
        {

            IList<RoleMenuShowVO> showVOList = new List<RoleMenuShowVO>();

            log.Debug("selectedRole=" + selectedRole);
            log.Debug("selectedMenuFunc=" + selectedMenuFunc);

            LoginRole role = authService.myService.DaoGetVOById<LoginRole>(int.Parse(selectedRole));

            IList<MenuFunc> selectedMenuFuncList;


            if (selectedMenuFunc.Equals("0"))
            {
                selectedMenuFuncList = authService.GetNotTopMenuFunc();
            }
            else
            {
                selectedMenuFuncList = authService.GetMenuFuncByParentId(int.Parse(selectedMenuFunc));
            }

            foreach (MenuFunc menu in selectedMenuFuncList)
            {
                RoleMenuShowVO showVO = new RoleMenuShowVO();

                showVO.Id = menu.Id;
                showVO.Name = menu.MenuFuncName;
                showVO.No = menu.Note;
                if (role.MenuFuncs.Contains(menu))
                {
                    showVO.IsAuth = true;
                }
                else
                {
                    showVO.IsAuth = false;
                }

                showVOList.Add(showVO);
            }

            return showVOList;
        }

    }

    public class RoleMenuShowVO
    {
        public int Id { get; set; }
        public string No { get; set; }
        public string Name { get; set; }

        public bool IsAuth { get; set; }

        public bool IsQuery { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsExport { get; set; }
        public bool IsElse { get; set; }
    }
}