using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.information;
using com.wudada.web.sessionstate;
using com.wudada.console.generic.util;
using com.wudada.console.service.poss.vo;
using com.wudada.web.util.page;
using com.wudada.web.service.auth;
using com.wudada.console.service.auth.vo;
using com.wudada.console.service.common.vo;
using com.wudada.console.service.poss;

public partial class admin_shop_ClassifyDetail : BasePage
{
    IPossService possService;
    SessionHelper sessionHelper = new SessionHelper();
    readonly string LIST_URL = "ClassifyList.aspx";
    string JsStr = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");

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
        ProductClassifyVO productClassifyVO = myService.DaoGetVOById<ProductClassifyVO>(id);

        UIHelper.FillUI(PanelUI, productClassifyVO);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ProductClassifyVO productClassifyVO = new ProductClassifyVO();
        UIHelper.FillVO(PanelUI, productClassifyVO);

        WuDADAAuthService.FillAuthData(productClassifyVO, FuncOprt.OprtAction.INSERT);
        possService.Insert_ProductClassify(productClassifyVO);

        JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.INSERT_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ProductClassifyVO productClassifyVO = myService.DaoGetVOById<ProductClassifyVO>(ConvertUtil.ToInt32(hdnId.Value));

        UIHelper.FillVO(PanelUI, productClassifyVO);

        WuDADAAuthService.FillAuthData(productClassifyVO, FuncOprt.OprtAction.UPDATE);
        myService.DaoUpdate(productClassifyVO);

        JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.UPDATE_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_URL);
    }
}
