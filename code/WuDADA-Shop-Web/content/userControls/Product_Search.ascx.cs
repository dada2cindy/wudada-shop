using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class content_userControls_Product_Search : System.Web.UI.UserControl
{
    readonly string PRODUCT_DETAIL_URL = "../poss/product_list.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //分類
            if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                hdnCId.Value = Request.QueryString["cid"];
            }
            //品牌類別
            if (!string.IsNullOrEmpty(Request.QueryString["bType"]))
            {
                hdnBType.Value = Request.QueryString["bType"];
            }
            //品牌
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
            {
                hdnBId.Value = Request.QueryString["bid"];
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("{0}?key={1}&cid={2}&bid={3}&bType={4}", PRODUCT_DETAIL_URL, txtName.Text.Trim(), hdnCId.Value, hdnBId.Value, hdnBType.Value));
    }
}
