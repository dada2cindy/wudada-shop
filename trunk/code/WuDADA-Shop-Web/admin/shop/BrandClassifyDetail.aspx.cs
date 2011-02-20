using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.wudada.web.page;
using com.wudada.web.sessionstate;
using com.wudada.console.service.system.vo;
using com.wudada.console.service.common.vo;
using com.wudada.web.util.page;
using com.wudada.console.generic.util;

public partial class admin_shop_BrandClassifyDetail : BasePage
{
    SessionHelper sessionHelper = new SessionHelper();
    readonly string LIST_URL = "BrandClassifyList.aspx";
    string JsStr = "";
    readonly string ITEM_PARAM_CLASSIFY = "品牌管理";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                UseUpdateMode();
                hdnId.Value = Request.QueryString["id"];
                LoadDataToUI(ConvertUtil.ToInt32(hdnId.Value));
            }
            else
            {
                UseAddMode();
            }
        }
    }

    private void UseAddMode()
    {
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
    }

    private void UseUpdateMode()
    {
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
    }

    private void LoadDataToUI(int id)
    {
        ItemParamVO itemParamVO = myService.DaoGetVOById<ItemParamVO>(id);

        UIHelper.FillUI(PanelUI, itemParamVO);
        ckbIsEnable.Checked = !itemParamVO.Deleted;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ItemParamVO itemParamVO = new ItemParamVO();
        UIHelper.FillVO(PanelUI, itemParamVO);
        itemParamVO.Classify = ITEM_PARAM_CLASSIFY;
        itemParamVO.Value = itemParamVO.Name;
        itemParamVO.Deleted = !ckbIsEnable.Checked;
        myService.DaoInsert(itemParamVO);


        JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.INSERT_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ItemParamVO itemParamVO = myService.DaoGetVOById<ItemParamVO>(ConvertUtil.ToInt32(hdnId.Value));
        UIHelper.FillVO(PanelUI, itemParamVO);
        itemParamVO.Value = itemParamVO.Name;
        itemParamVO.Deleted = !ckbIsEnable.Checked;
        myService.DaoUpdate(itemParamVO);

        JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.UPDATE_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_URL);
    }
}