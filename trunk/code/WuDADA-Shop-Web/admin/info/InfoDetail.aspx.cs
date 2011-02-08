using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.web.sessionstate;
using com.wudada.console.service.information;
using com.wudada.console.var;
using com.wudada.console.generic.util;
using com.wudada.console.service.information.vo;
using com.wudada.web.util.page;
using com.wudada.console.service.system.vo;
using com.wudada.web.service.auth;
using com.wudada.console.service.auth.vo;
using com.wudada.console.service.common.vo;
using com.wudada.web.util;

public partial class admin_info_InfoDetail : BasePage
{
    IInformationService informationService;
    SessionHelper sessionHelper = new SessionHelper();
    int PARAM_ID_CLASSIFY = VarHelper.WuDADA_ITEMPARAM_ID_INFO1;
    readonly string LIST_URL = "InfoList.aspx";
    readonly string DETAIL_URL = "InfoDetail.aspx";
    string JsStr = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        informationService = (IInformationService)ctx.GetObject("InformationService");

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
        //新增時上傳圖片關掉
        trFile.Visible = false;
    }

    private void UseUpdateMode()
    {
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
    }

    private void LoadDataToUI(int id)
    {
        InfoVO infoVO = myService.DaoGetVOById<InfoVO>(id);

        UIHelper.FillUI(PanelUI, infoVO);
        //圖片清單
        ltlFileIfrm.Text = string.Format("<iframe width='500' scrolling='no' frameborder='0' id='ifmFileList' src='{0}' onload='ResizeIframe(this)'></iframe>", string.Format("{0}?infoid={1}&btnAddName={2}", ConfigHelper.FileList_Admin, id, "上傳圖片"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(fckContent.Value))
        {
            InfoVO infoVO = new InfoVO();
            UIHelper.FillVO(PanelUI, infoVO);
            infoVO.Classify = myService.DaoGetVOById<ItemParamVO>(PARAM_ID_CLASSIFY);

            WuDADAAuthService.FillAuthData(infoVO, FuncOprt.OprtAction.INSERT);
            informationService.Insert_Info(infoVO);

            JsStr = JavascriptUtil.AlertJSAndRedirect(string.Format("{0}，請繼續上傳圖片", MsgVO.INSERT_OK), string.Format("{0}?id={1}", DETAIL_URL, infoVO.Id));
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
        }
        else
        {
            JsStr = JavascriptUtil.AlertJS("內容 不可空白");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(fckContent.Value))
        {
            InfoVO infoVO = myService.DaoGetVOById<InfoVO>(ConvertUtil.ToInt32(hdnId.Value));

            UIHelper.FillVO(PanelUI, infoVO);

            WuDADAAuthService.FillAuthData(infoVO, FuncOprt.OprtAction.UPDATE);
            myService.DaoUpdate(infoVO);

            JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.UPDATE_OK, LIST_URL);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
        }
        else
        {
            JsStr = JavascriptUtil.AlertJS("內容 不可空白");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_URL);
    }
}
