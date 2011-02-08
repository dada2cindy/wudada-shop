using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.console.service.advertisement;
using com.wudada.web.page;
using com.wudada.console.service.system;
using com.wudada.web.sessionstate;
using com.wudada.console.var;
using com.wudada.console.generic.util;
using com.wudada.console.service.advertisement.vo;
using com.wudada.web.util.page;
using com.wudada.console.service.system.vo;
using com.wudada.console.service.common.vo;
using com.wudada.web.service.auth;
using com.wudada.console.service.auth.vo;
using com.wudada.web.util;

public partial class admin_adInfo_Ad1_Detail : BasePage
{
    IAdService adService;
    ISystemService systemService;
    SessionHelper sessionHelper = new SessionHelper();
    readonly string LIST_URL = "Ad1_List.aspx";
    string JsStr = "";
    int PARAM_ID_CLASSIFY = VarHelper.WuDADA_ITEMPARAM_ID_AD_INFO1;
    readonly string SHOW_PIC = ConfigHelper.PictureShow;
    readonly string PIC_DIR = ConfigHelper.AdPicURL;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        adService = (IAdService)ctx.GetObject("AdService");
        systemService = (ISystemService)ctx.GetObject("SystemService");

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
        AdInfoVO adInfoVO = myService.DaoGetVOById<AdInfoVO>(id);

        UIHelper.FillUI(PanelUI, adInfoVO);        

        //圖片
        if (!string.IsNullOrEmpty(adInfoVO.ImgPath))
        {
            pnlFile.Visible = true;
            imgFile.ImageUrl = string.Format("{0}?type=adpic&fileName={1}", SHOW_PIC, adInfoVO.ImgPath);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            AdInfoVO adInfoVO = new AdInfoVO();
            UIHelper.FillVO(PanelUI, adInfoVO);
            adInfoVO.Classify = myService.DaoGetVOById<ItemParamVO>(PARAM_ID_CLASSIFY);

            bool checkUpload1 = true;
            //上傳圖片
            if (FileUpload1.HasFile)
            {
                checkUpload1 = DoFileUpdate(FileUpload1, adInfoVO);
            }

            if (checkUpload1)
            {
                WuDADAAuthService.FillAuthData(adInfoVO, FuncOprt.OprtAction.INSERT);
                adService.Insert_AdInfo(adInfoVO);

                JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.INSERT_OK, LIST_URL);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
            }
            else
            {
                JsStr = JavascriptUtil.AlertJS("上傳檔案失敗");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
            }
        }
        else
        {
            JsStr = JavascriptUtil.AlertJS("請上傳圖片");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile || pnlFile.Visible)
        {
            AdInfoVO adInfoVO = myService.DaoGetVOById<AdInfoVO>(ConvertUtil.ToInt32(hdnId.Value));
            UIHelper.FillVO(PanelUI, adInfoVO);           

            bool checkUpload1 = true;
            //上傳圖片
            if (FileUpload1.HasFile)
            {
                checkUpload1 = DoFileUpdate(FileUpload1, adInfoVO);
            }
            if (checkUpload1)
            {
                WuDADAAuthService.FillAuthData(adInfoVO, FuncOprt.OprtAction.UPDATE);
                myService.DaoUpdate(adInfoVO);

                JsStr = JavascriptUtil.AlertJSAndRedirect(MsgVO.UPDATE_OK, LIST_URL);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
            }
            else
            {
                JsStr = JavascriptUtil.AlertJS("上傳檔案失敗");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
            }
        }
        else
        {
            JsStr = JavascriptUtil.AlertJS("請上傳圖片");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(LIST_URL);
    }

    private bool DoFileUpdate(FileUpload fileUpload, AdInfoVO adInfoVO)
    {
        bool result = false;
        string uploadDirPath = Server.MapPath(PIC_DIR);
        FileUploadHelper.CheckDir(uploadDirPath);
        string uploadFileName = ConvertUtil.Get_NewFileName(fileUpload.FileName);
        //string uploadFileName = string.Format("{0}{1}{2}{3}{4}{5}_{6}", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, fileUpload.FileName);
        string uploadFullPath = uploadDirPath + uploadFileName;

        if (!FileUploadHelper.CheckFile(uploadFullPath))
        {
            fileUpload.SaveAs(uploadFullPath);
            if (!string.IsNullOrEmpty(adInfoVO.ImgPath))
            {
                DoFileDelete(adInfoVO);
            }
            adInfoVO.ImgPath = uploadFileName;
            result = true;
        }
        else
        {
            string JsStr = JavascriptUtil.AlertJS("圖片已經存在");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);
        }
        return result;
    }

    protected void btnFileDel_Click(object sender, EventArgs e)
    {
        AdInfoVO adInfoVO = myService.DaoGetVOById<AdInfoVO>(ConvertUtil.ToInt32(hdnId.Value));

        DoFileDelete(adInfoVO);

        myService.DaoUpdate(adInfoVO);

        string JsStr = JavascriptUtil.AlertJS(MsgVO.DELETE_OK);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);

        pnlFile.Visible = false;
    }

    private bool DoFileDelete(AdInfoVO adInfoVO)
    {
        bool result = true;

        try
        {
            string filePath = Server.MapPath(PIC_DIR);
            filePath += adInfoVO.ImgPath;
            adInfoVO.ImgPath = "";
            FileUploadHelper.DeleteUploadFile(filePath);
        }
        catch (Exception ex)
        {
            result = false;
            log.Error(ex);
        }

        return result;
    }
}
