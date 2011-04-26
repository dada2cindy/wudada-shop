using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.var;
using com.wudada.console.service.system.vo;

public partial class content_masterpage_shop_info : BasePage
{
    readonly int PARAM_ID_COM_INFO2 = VarHelper.WuDADA_ITEMPARAM_ID_COM_INFO2;
    readonly int PARAM_ID_COM_INFO3 = VarHelper.WuDADA_ITEMPARAM_ID_COM_INFO3;

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
        //中華店
        ItemParamVO itemParam_Com2 = myService.DaoGetVOById<ItemParamVO>(PARAM_ID_COM_INFO2);
        ltlSopInfo1.Text = itemParam_Com2.Value;

        //敦北店
        ItemParamVO itemParam_Com3 = myService.DaoGetVOById<ItemParamVO>(PARAM_ID_COM_INFO3);
        ltlSopInfo2.Text = itemParam_Com3.Value;
    }
}