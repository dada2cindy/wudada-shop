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

public partial class admin_system_ComInfo3 : BasePage
{
    int PARAM_ID = VarHelper.WuDADA_ITEMPARAM_ID_COM_INFO3;
    string JsStr = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!Page.IsPostBack)
        {
            LoadDataToUI();
        }
    }

    private void LoadDataToUI()
    {
        ItemParamVO itemParamVO = myService.DaoGetVOById<ItemParamVO>(PARAM_ID);

        UIHelper.FillUI(PanelUI, itemParamVO);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ItemParamVO itemParamVO = myService.DaoGetVOById<ItemParamVO>(PARAM_ID);

        UIHelper.FillVO(PanelUI, itemParamVO);
        myService.DaoUpdate(itemParamVO);

        JsStr = JavascriptUtil.AlertJS(MsgVO.UPDATE_OK);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }
}
