﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.util.page;
using com.wudada.console.service.system;
using com.wudada.web.sessionstate;
using com.wudada.console.service.auth;
using com.wudada.console.service.auth.vo;
using com.wudada.console.generic.util;
using com.wudada.web.page;
using com.wudada.console.service.common.vo;


public partial class admin_auth_RoleDetail : BasePage
{
    IAuthService authService;
    SessionHelper sessionHelper = new SessionHelper();

    string LIST_URL = "UserList.aspx";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        authService = (IAuthService)ctx.GetObject("AuthService");

        if (!Page.IsPostBack)
        {
            SetConcrol();
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
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

        string JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.INSERT_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
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

        string JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.UPDATE_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
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
