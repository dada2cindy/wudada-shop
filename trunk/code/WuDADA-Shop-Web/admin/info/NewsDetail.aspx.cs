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

public partial class admin_info_NewsDetail : BasePage
{
    IInformationService informationService;
    SessionHelper sessionHelper = new SessionHelper();
    int PARAM_ID_CLASSIFY = VarHelper.WuDADA_ITEMPARAM_ID_NEWS1;
    readonly string LIST_URL = "NewsList.aspx";
    string JsStr = "";
    readonly string SHOW_PIC = ConfigHelper.PictureShow;
    readonly string PIC_DIR = ConfigHelper.FjxFileURL;

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
    }

    private void UseUpdateMode()
    {
        btnAdd.Visible = false;
        btnUpdate.Visible = true;
    }

    private void LoadDataToUI(int id)
    {
        NewsVO newsVO = myService.DaoGetVOById<NewsVO>(id);

        UIHelper.FillUI(PanelUI, newsVO);

        //圖片
        if (!string.IsNullOrEmpty(newsVO.ImgPath))
        {
            pnlFile.Visible = true;
            imgFile.ImageUrl = string.Format("{0}?type=fjx&fileName={1}", SHOW_PIC, newsVO.ImgPath);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(fckContent.Value))
        {
            if (FileUpload1.HasFile)
            {
                NewsVO newsVO = new NewsVO();
                UIHelper.FillVO(PanelUI, newsVO);
                newsVO.Classify = myService.DaoGetVOById<ItemParamVO>(PARAM_ID_CLASSIFY);

                bool checkUpload1 = true;
                //上傳圖片
                if (FileUpload1.HasFile)
                {
                    checkUpload1 = DoFileUpdate(FileUpload1, newsVO);
                }

                if (checkUpload1)
                {
                    WuDADAAuthService.FillAuthData(newsVO, FuncOprt.OprtAction.INSERT);
                    informationService.Insert_News(newsVO);

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
            if (FileUpload1.HasFile || pnlFile.Visible)
            {
                NewsVO newsVO = myService.DaoGetVOById<NewsVO>(ConvertUtil.ToInt32(hdnId.Value));

                UIHelper.FillVO(PanelUI, newsVO);

                bool checkUpload1 = true;
                //上傳圖片
                if (FileUpload1.HasFile)
                {
                    checkUpload1 = DoFileUpdate(FileUpload1, newsVO);
                }
                if (checkUpload1)
                {
                    WuDADAAuthService.FillAuthData(newsVO, FuncOprt.OprtAction.UPDATE);
                    myService.DaoUpdate(newsVO);

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

    private bool DoFileUpdate(FileUpload fileUpload, NewsVO newsVO)
    {
        bool result = false;
        string uploadDirPath = Server.MapPath(PIC_DIR);
        FileUploadHelper.CheckDir(uploadDirPath);
        string uploadFileName = ConvertUtil.Get_NewFileName(fileUpload.FileName);
        string uploadFullPath = uploadDirPath + uploadFileName;

        if (!FileUploadHelper.CheckFile(uploadFullPath))
        {
            fileUpload.SaveAs(uploadFullPath);
            if (!string.IsNullOrEmpty(newsVO.ImgPath))
            {
                DoFileDelete(newsVO);
            }
            newsVO.ImgPath = uploadFileName;
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
        NewsVO newsVO = myService.DaoGetVOById<NewsVO>(ConvertUtil.ToInt32(hdnId.Value));

        DoFileDelete(newsVO);

        myService.DaoUpdate(newsVO);

        string JsStr = JavascriptUtil.AlertJS(MsgVO.DELETE_OK);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", JsStr, false);

        pnlFile.Visible = false;
    }

    private bool DoFileDelete(NewsVO newsVO)
    {
        bool result = true;

        try
        {
            string filePath = Server.MapPath(PIC_DIR);
            filePath += newsVO.ImgPath;
            newsVO.ImgPath = "";
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

