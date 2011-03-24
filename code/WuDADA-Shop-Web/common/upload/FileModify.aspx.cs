using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.poss;
using OboutInc.FileUpload;
using System.IO;
using com.wudada.console.service.common.vo;
using com.wudada.console.generic.util;
using com.wudada.web.util.page;
using com.wudada.console.service.information;

public partial class common_upload_FileModify : BasePage
{
    IPossService possService;
    IInformationService informationService;
    public string btnSureClientId = "";

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");
        informationService = (IInformationService)ctx.GetObject("InformationService");
        btnSureClientId = btnSure.ClientID;

        if (Page.IsPostBack)
        {
            OboutFileCollection files = uploadProgress.Files;

            uploadedFiles.Text = "";
            log.Error("files.Count = " + files.Count);
            for (int i = 0; i < files.Count; i++)
            {
                OboutPostedFile file = files[i];

                string path = this.Server.MapPath(@"../../fileUpload/fjx/");
                string fileName = Path.GetFileName(file.FileName);                

                //寫入/更新 檔案
                string newFileName = ConvertUtil.Get_NewFileName(fileName);
                file.SaveAs(path + newFileName);

                FileVO fileVO = new FileVO();
                if (!string.IsNullOrEmpty(hdnId.Value))
                {
                    fileVO = myService.Dao.DaoGetVOById<FileVO>(int.Parse(hdnId.Value));
                }
                fileVO.Name = fileName;
                fileVO.Path = newFileName;
                fileVO.OriginalName = fileName;
                if (!string.IsNullOrEmpty(hdnId.Value))
                {
                    myService.DaoUpdate(fileVO);
                }
                else
                {
                    myService.DaoInsert(fileVO);
                }
                hdnId.Value = fileVO.Id.ToString();

                txtName.Text = fileName;
            }

            if (uploadedFiles.Text.Length == 0 && string.IsNullOrEmpty(hdnId.Value))
            {
                string js = JavascriptUtil.AlertJS("請選擇檔案");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", js, false);
            }
            //uploadedFiles.Text = "請選擇檔案";
            //else
            //    uploadedFiles.Text += "</table>";
        }
        else
        {
            //檔案Id
            if (!string.IsNullOrEmpty(Request.QueryString["fid"]))
            {
                hdnId.Value = Request.QueryString["fid"];
                LoadDataToUI(hdnId.Value);
            }

            //設定關聯主檔的Id
            if (!string.IsNullOrEmpty(Request.QueryString["pid"])) //商品
            {
                hdnTargetId.Value = Request.QueryString["pid"];
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["infoid"])) //眼鏡與我
            {
                hdnTargetId.Value = Request.QueryString["infoid"];
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["bid"])) //品牌
            {
                hdnTargetId.Value = Request.QueryString["bid"];
            } 
        }
    }

    private void LoadDataToUI(string id)
    {
        log.Debug("file id = " + id);
        FileVO fileVO = myService.Dao.DaoGetVOById<FileVO>(int.Parse(id));

        UIHelper.FillUI(PanelUI, fileVO);

        //btnUpload.Enabled = false;
    }

    protected void btnSure_Click(object sender, EventArgs e)
    {
        FileVO fileVO = myService.DaoGetVOById<FileVO>(int.Parse(hdnId.Value));
        if (string.IsNullOrEmpty(fileVO.Path))
        {
            string js = JavascriptUtil.AlertJS("請上傳檔案");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data", js, false);
        }
        else
        {
            UIHelper.FillVO(PanelUI, fileVO);

            myService.Dao.DaoUpdate(fileVO);

            //設定檔案關聯
            if (!string.IsNullOrEmpty(Request.QueryString["pid"])) //商品
            {
                possService.SetFileToProductVO(int.Parse(hdnId.Value), int.Parse(hdnTargetId.Value));
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["infoid"])) //眼鏡與我
            {
                informationService.SetFileToInfoVO(int.Parse(hdnId.Value), int.Parse(hdnTargetId.Value));
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["bid"])) //品牌
            {
                possService.SetFileToBandVO(int.Parse(hdnId.Value), int.Parse(hdnTargetId.Value));
            }

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "data2", "window.close()", true);
        }
    }
}
