using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.poss;
using com.wudada.console.service.system;
using com.wudada.web.sessionstate;
using com.wudada.console.generic.util;
using com.wudada.console.service.poss.vo;
using com.wudada.web.util.page;
using com.wudada.web.service.auth;
using com.wudada.console.service.auth.vo;
using com.wudada.console.service.common.vo;
using com.wudada.console.service.system.vo;
using com.wudada.web.util;

public partial class admin_shop_BrandDetail : BasePage
{
    IPossService possService;
    ISystemService systemService;
    SessionHelper sessionHelper = new SessionHelper();
    readonly string LIST_URL = "BrandList.aspx";
    readonly string DETAIL_URL = "BrandDetail.aspx";
    string JsStr = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");
        systemService = (ISystemService)ctx.GetObject("SystemService");

        if (!Page.IsPostBack)
        {
            SetControl();
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

    private void SetControl()
    {
        //分類
        ddl_Classify.Items.Clear();
        ddl_Classify.Items.Add(new ListItem("請選擇", ""));
        IList<ItemParamVO> classifyList = systemService.GetAllItemParamVOByNoDel("品牌管理");
        if (!CollectionUtil.IsNullOrEmpty(classifyList))
        {
            UIHelper.AddDropDowListItem(ddl_Classify, classifyList.GetEnumerator(), "Name", "Id");
        }
    }

    private void UseAddMode()
    {
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        //新增時不能設定上架，要去上傳圖片
        ckbIsEnable.Enabled = false;
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
        BrandVO brandVO = myService.DaoGetVOById<BrandVO>(id);
        UIHelper.FillUI(PanelUI, brandVO);
        //分類
        if (brandVO.Classify != null)
        {
            ddl_Classify.SelectedValue = brandVO.Classify.Id.ToString();
        }
        
        //圖片清單
        ltlFileIfrm.Text = string.Format("<iframe width='500' scrolling='no' frameborder='0' id='ifmFileList' src='{0}' onload='ResizeIframe(this)'></iframe>", string.Format("{0}?bid={1}&btnAddName={2}", ConfigHelper.FileList_Admin, id, "上傳圖片"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        BrandVO brandVO = new BrandVO();
        UIHelper.FillVO(PanelUI, brandVO);
        //分類
        if (!string.IsNullOrEmpty(ddl_Classify.SelectedValue))
        {
            brandVO.Classify = myService.DaoGetVOById<ItemParamVO>(ConvertUtil.ToInt32(ddl_Classify.SelectedValue));
        }

        WuDADAAuthService.FillAuthData(brandVO, FuncOprt.OprtAction.INSERT);
        possService.Insert_BrandVO(brandVO);

        JsStr = JavascriptUtil.AlertJSAndRedirect(string.Format("{0}，請繼續上傳品牌圖片", MsgVO.INSERT_OK), string.Format("{0}?id={1}", DETAIL_URL, brandVO.Id));
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        BrandVO brandVO = myService.DaoGetVOById<BrandVO>(ConvertUtil.ToInt32(hdnId.Value));
        UIHelper.FillVO(PanelUI, brandVO);
        //分類
        if (!string.IsNullOrEmpty(ddl_Classify.SelectedValue))
        {
            brandVO.Classify = myService.DaoGetVOById<ItemParamVO>(ConvertUtil.ToInt32(ddl_Classify.SelectedValue));
        }

        WuDADAAuthService.FillAuthData(brandVO, FuncOprt.OprtAction.UPDATE);
        myService.DaoUpdate(brandVO);

        JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.UPDATE_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_URL);
    }
}
