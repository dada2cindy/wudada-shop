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
using com.wudada.console.generic.util;


public partial class admin_auth_RoleDetail : System.Web.UI.Page
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
    AuthService authService;
    SessionHelper sessionHelper = new SessionHelper();

    string LIST_URL = "UserList.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
        authService = (AuthService)ctx.GetObject("AuthService");

        if (!Page.IsPostBack)
        {
            SetConcrol();
            if (Request.QueryString["id"] != null)
            {
                UseUpdateMode();
                hdnId.Value = Request.QueryString["id"].ToString();
                LoadDataToUI(hdnId.Value);
            }
            else
            {
                UseAddMode();
            }
        }
    }

    private void SetConcrol()
    {
        ckblRole.Items.Clear();

        IList<LoginRole> roleList = authService.myService.DaoGetAllVO<LoginRole>();
        if (!CollectionUtil.IsNullOrEmpty<LoginRole>(roleList))
        {
            foreach (LoginRole vo in roleList)
            {
                ckblRole.Items.Add(new ListItem(vo.RoleName, vo.RoleId.ToString()));
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        LoginUser user = new LoginUser();
        user.Name = txtName.Text.Trim();
        user.Email = txtEmail.Text.Trim();
        user.UserId = txtUserId.Text.Trim();
        user.Password = EncryptUtil.GetMD5(txtPassword.Text.Trim());
        user.IsEnable = Convert.ToBoolean(rdoIsEnable.SelectedValue);
        SetLoginRoles(user);
        authService.myService.DaoInsert(user);

        string JsStr = JavascriptUtil.AlertJSAndRedirect("新增成功", LIST_URL);
        ScriptManager.RegisterClientScriptBlock(lblMsg, lblMsg.GetType(), "data", JsStr, false);
    }

    private void SetLoginRoles(LoginUser user)
    {
        //更新權限
        IList<LoginRole> roleList = new List<LoginRole>();
        for (int i = 0; i < ckblRole.Items.Count; i++)
        {
            if (ckblRole.Items[i].Selected)
            {
                int roleId = int.Parse(ckblRole.Items[i].Value);
                LoginRole loginRole = authService.myService.DaoGetVOById<LoginRole>(roleId);
                roleList.Add(loginRole);
            }
        }

        user.BelongRoles = roleList;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        LoginUser user = authService.myService.DaoGetVOById<LoginUser>(int.Parse(hdnId.Value));

        user.Name = txtName.Text.Trim();
        user.Email = txtEmail.Text.Trim();
        user.IsEnable = Convert.ToBoolean(rdoIsEnable.SelectedValue);

        if (pnlUpdatePass.Visible != false)
        {
            user.Password = EncryptUtil.GetMD5(txtPassword.Text.Trim());
        }
        SetLoginRoles(user);
        authService.myService.DaoUpdate(user);

        string JsStr = JavascriptUtil.AlertJSAndRedirect("更新成功", LIST_URL);
        ScriptManager.RegisterClientScriptBlock(lblMsg, lblMsg.GetType(), "data", JsStr, false);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_URL);
    }
    private void UseAddMode()
    {
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        ckbUpdatePass.Checked = true;
        ckbUpdatePass_CheckedChanged(null, null);
    }

    private void UseUpdateMode()
    {
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
        txtUserId.Enabled = false;
    }
    private void LoadDataToUI(string id)
    {
        LoginUser user = authService.myService.DaoGetVOById<LoginUser>(int.Parse(id));

        UIHelper.FillUI(Panel1, user);
        rdoIsEnable.SelectedValue = user.IsEnable.ToString();

        //設定權限到UI
        if (!CollectionUtil.IsNullOrEmpty<LoginRole>(user.BelongRoles))
        {
            FillCkblRole(user.BelongRoles);
        }
    }

    private void FillCkblRole(IList<LoginRole> roleList)
    {
        for (int i = 0; i < ckblRole.Items.Count; i++)
        {
            string roleId = ckblRole.Items[i].Value;
            if (CollectionUtil.ContainValue<LoginRole>(roleList, "RoleId", roleId))
            {
                ckblRole.Items[i].Selected = true;
            }
        }
    }

    protected void ckbUpdatePass_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbUpdatePass.Checked)
        {
            pnlUpdatePass.Visible = true;
        }
        else
        {
            pnlUpdatePass.Visible = false;
        }
    }
}
