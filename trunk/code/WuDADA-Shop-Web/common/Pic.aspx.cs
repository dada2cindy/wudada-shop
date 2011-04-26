using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.util;

public partial class common_Pic : System.Web.UI.Page
{
    readonly string PIC_SHOW = ConfigHelper.PictureShow;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            img.ImageUrl = string.Format("{0}?type={1}&fileName={2}&auto={3}&size={4}", PIC_SHOW, Request.QueryString["type"], Request.QueryString["fileName"], Request.QueryString["auto"], Request.QueryString["size"]);
        }
    }
}
