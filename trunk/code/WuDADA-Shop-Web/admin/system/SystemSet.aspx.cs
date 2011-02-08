using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.var;
using com.wudada.console.service.system.vo;
using com.wudada.web.util.page;
using com.wudada.console.service.common.vo;

public partial class admin_system_SystemSet : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!Page.IsPostBack)
        {
            hdnId.Value = VarHelper.WuDADA_SYSTEMPARAM_ID.ToString();
            LoaDataToUI(int.Parse(hdnId.Value));
        }
    }

    private void LoaDataToUI(int id)
    {
        SystemParamVO systemParamVO = myService.DaoGetVOById<SystemParamVO>(id);
        UIHelper.FillUI(PanelUI, systemParamVO);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string JsStr = "";

        SystemParamVO systemParamVO = myService.DaoGetVOById<SystemParamVO>(int.Parse(hdnId.Value));
        UIHelper.FillVO(PanelUI, systemParamVO);
        myService.DaoUpdate(systemParamVO);

        JsStr = JavascriptUtil.AlertJS(MsgVO.UPDATE_OK);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);

    }
}
