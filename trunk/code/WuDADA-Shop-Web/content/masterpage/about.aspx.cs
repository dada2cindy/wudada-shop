using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.var;
using com.wudada.console.service.system.vo;

public partial class content_masterpage_about : BasePage
{
    readonly int PARAM_ID_COM_INFO1 = VarHelper.WuDADA_ITEMPARAM_ID_COM_INFO1;
    readonly int PARAM_IDID_COM_INFO2 = VarHelper.WuDADA_ITEMPARAM_ID_COM_INFO2;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        if (!Page.IsPostBack)
        {
            LoadDataToUI();
        }
    }

    private void LoadDataToUI()
    {
        //關於新明
        ItemParamVO itemParam_Com1 = myService.DaoGetVOById<ItemParamVO>(PARAM_ID_COM_INFO1);
        ltlComInfo1.Text = itemParam_Com1.Value;

        //聯絡資訊
        ItemParamVO itemParam_Com2 = myService.DaoGetVOById<ItemParamVO>(PARAM_IDID_COM_INFO2);
        ltlComInfo2.Text = itemParam_Com2.Value;
    }
}
