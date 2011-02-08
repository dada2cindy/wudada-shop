using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.poss;
using com.wudada.console.service.poss.vo;
using com.wudada.console.generic.util;
using com.wudada.console.service.common.vo;
using com.wudada.web.util;
using com.wudada.console.service.information.vo;
using com.wudada.console.service.information;

public partial class common_upload_FileList : BasePage
{
    IPossService possService;
    IInformationService informationService;

    public string hdnTargetClientId = "";
    public String Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");
        informationService = (IInformationService)ctx.GetObject("InformationService");
        hdnTargetClientId = hdnTarget.ClientID;

        //修改UI文字
        if (!string.IsNullOrEmpty(Request.QueryString["btnAddName"]))
        {
            btnAdd.Value = Request.QueryString["btnAddName"];
            hdnTarget.Value = "btnAddName=" + Request.QueryString["btnAddName"];
        }

        //判斷目前是哪個關聯的檔案上傳
        if (!string.IsNullOrEmpty(Request.QueryString["pid"])) //產品
        {
            Id = Request.QueryString["pid"];
            hdnTarget.Value += "&pid=" + Id;
            btnAdd.Attributes.Add("onclick", string.Format("DoShowModelDialog('FileModify.aspx?pid={0}')", Id));
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["infoid"])) //眼鏡與我
        {
            Id = Request.QueryString["infoid"];
            hdnTarget.Value += "&infoid=" + Id;
            btnAdd.Attributes.Add("onclick", string.Format("DoShowModelDialog('FileModify.aspx?infoid={0}')", Id));
        }       

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                hdnId.Value = Id;
                RP_Bind(Id);
            }
        }
    }

    private void RP_Bind(string id)
    {
        //判斷目前是哪個關聯的檔案上傳
        if (!string.IsNullOrEmpty(Request.QueryString["pid"])) //商品
        {
            ProductVO productVO = myService.DaoGetVOById<ProductVO>(int.Parse(id));
            rpFile.DataSource = productVO.FileList;
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["infoid"])) //眼鏡與我
        {
            InfoVO infoVO = myService.DaoGetVOById<InfoVO>(int.Parse(id));
            rpFile.DataSource = infoVO.FileList;
        }       

        rpFile.DataBind();
    }

    protected void rpFile_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "MyDel":
                //刪除實體檔案
                FileVO fileVO = myService.DaoGetVOById<FileVO>(int.Parse(e.CommandArgument.ToString()));
                string filePath = Server.MapPath(@"../../fileUpload/fjx/");
                filePath += fileVO.Path;
                FileUploadHelper.DeleteUploadFile(filePath);

                //刪除VO
                //判斷目前是哪個關聯的檔案上傳
                if (!string.IsNullOrEmpty(Request.QueryString["pid"])) //商品
                {
                    possService.DeleteFileFromProductVO(int.Parse(e.CommandArgument.ToString()), int.Parse(hdnId.Value));
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["infoid"])) //眼鏡與我
                {
                    informationService.DeleteFileFromInfoVO(int.Parse(e.CommandArgument.ToString()), int.Parse(hdnId.Value));
                }               

                RP_Bind(hdnId.Value);
                break;
        }
    }
    protected void rpFile_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;

            FileVO fileVO = (FileVO)e.Item.DataItem;
            if (ImageHelper.IsImage(fileVO.Path))
            {
                Image imgFile = (Image)ctrl.FindControl("imgFile");
                imgFile.ImageUrl = string.Format("{0}?fileName={1}&type=fjx", ConfigHelper.PictureShow, fileVO.Path);
                imgFile.Visible = true;

                HyperLink hlinkImage = (HyperLink)ctrl.FindControl("hlinkImage");
                hlinkImage.NavigateUrl = string.Format("{0}?fileName={1}&type=fjx", ConfigHelper.PictureShow, fileVO.Path);
                hlinkImage.Visible = true;
            }
            else
            {
                HyperLink hlinkFile = (HyperLink)ctrl.FindControl("hlinkFile");
                hlinkFile.Visible = true;
            }
        }
    }
}
