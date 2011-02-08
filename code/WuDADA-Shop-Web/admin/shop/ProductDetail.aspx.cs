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
using com.wudada.web.util;

public partial class admin_shop_ProductDetail : BasePage
{
    IPossService possService;
    ISystemService systemService;
    SessionHelper sessionHelper = new SessionHelper();
    readonly string LIST_URL = "ProductList.aspx";
    readonly string DETAIL_URL = "ProductDetail.aspx";
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
        ckbl_Classify.Items.Clear();
        IList<ProductClassifyVO> classifyList = possService.GetAllProductClassifyVO(true);
        if (!CollectionUtil.IsNullOrEmpty(classifyList))
        {
            UIHelper.AddCheckBoxListItem(ckbl_Classify, classifyList.GetEnumerator(), "Name", "Id");
        }

        //品牌
        ddl_Brand.Items.Clear();
        ddl_Brand.Items.Add(new ListItem("請選擇", ""));
        IList<BrandVO> brandList = possService.GetAllBrandVO(true);
        if (!CollectionUtil.IsNullOrEmpty(brandList))
        {
            UIHelper.AddDropDowListItem(ddl_Brand, brandList.GetEnumerator(), "Name", "Id");
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
        ProductVO productVO = myService.DaoGetVOById<ProductVO>(id);
        UIHelper.FillUI(PanelUI, productVO);
        //分類
        if (!CollectionUtil.IsNullOrEmpty(productVO.ClassifyList))
        {
            UIHelper.SetCheckBoxListChecked(ckbl_Classify, productVO.ClassifyList, "Id");
        }

        //品牌
        if (productVO.Brand != null)
        {
            ddl_Brand.SelectedValue = productVO.Brand.Id.ToString();
        }

        //圖片清單
        ltlFileIfrm.Text = string.Format("<iframe width='500' scrolling='no' frameborder='0' id='ifmFileList' src='{0}' onload='ResizeIframe(this)'></iframe>", string.Format("{0}?pid={1}&btnAddName={2}", ConfigHelper.FileList_Admin, id, "上傳圖片"));
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ProductVO productVO = new ProductVO();
        UIHelper.FillVO(PanelUI, productVO);
        //分類
        SetProductClassify(productVO);

        //品牌
        if (!string.IsNullOrEmpty(ddl_Brand.SelectedValue))
        {
            productVO.Brand = myService.DaoGetVOById<BrandVO>(ConvertUtil.ToInt32(ddl_Brand.SelectedValue));
        }
        else
        {
            productVO.Brand = null;
        }

        WuDADAAuthService.FillAuthData(productVO, FuncOprt.OprtAction.INSERT);
        myService.DaoInsert(productVO);

        JsStr = JavascriptUtil.AlertJSAndRedirect(string.Format("{0}，請繼續上傳商品圖片", MsgVO.INSERT_OK), string.Format("{0}?id={1}", DETAIL_URL, productVO.Id));
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ProductVO productVO = myService.DaoGetVOById<ProductVO>(ConvertUtil.ToInt32(hdnId.Value));
        UIHelper.FillVO(PanelUI, productVO);
        //分類
        SetProductClassify(productVO);

        //品牌
        if (!string.IsNullOrEmpty(ddl_Brand.SelectedValue))
        {
            productVO.Brand = myService.DaoGetVOById<BrandVO>(ConvertUtil.ToInt32(ddl_Brand.SelectedValue));
        }
        else
        {
            productVO.Brand = null;
        }

        WuDADAAuthService.FillAuthData(productVO, FuncOprt.OprtAction.UPDATE);
        myService.DaoUpdate(productVO);

        JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.UPDATE_OK, LIST_URL);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
    }

    private void SetProductClassify(ProductVO productVO)
    {
        //更新分類
        IList<ProductClassifyVO> classifyList = new List<ProductClassifyVO>();
        for (int i = 0; i < ckbl_Classify.Items.Count; i++)
        {
            if (ckbl_Classify.Items[i].Selected)
            {
                int classifyId = int.Parse(ckbl_Classify.Items[i].Value);
                ProductClassifyVO productClassifyVO = myService.DaoGetVOById<ProductClassifyVO>(classifyId);
                classifyList.Add(productClassifyVO);
            }
        }

        productVO.ClassifyList = classifyList;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_URL);
    }
}
