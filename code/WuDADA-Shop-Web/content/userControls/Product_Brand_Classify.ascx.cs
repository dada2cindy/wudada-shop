using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using com.wudada.console.service.system;
using Spring.Context;
using Spring.Context.Support;
using com.wudada.console.service.poss;
using NHibernate.Criterion;
using com.wudada.console.service.poss.vo;
using com.wudada.console.service.system.vo;

public partial class content_userControls_Product_Brand_Classify : System.Web.UI.UserControl
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    ISystemService systemService;
    IPossService possService;

    protected void Page_Load(object sender, EventArgs e)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
        systemService = (ISystemService)ctx.GetObject("SystemService");
        possService = (IPossService)ctx.GetObject("PossService");

        if (!Page.IsPostBack)
        {
            LoadDataToUI();
        }
    }

    private void LoadDataToUI()
    {
        //品牌分類
        RP_BrandType_Bind();
        //分類
        RP_Classify_Bind();
    }

    private void RP_BrandType_Bind()
    {
        rptBrandType.DataSource = systemService.GetAllItemParamVOByNoDel("品牌管理");
        rptBrandType.DataBind();
    }

    private void RP_Classify_Bind()
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(ProductClassifyVO));

        //啟用
        dCriteria.Add(Expression.Eq("IsEnable", true));

        dCriteria.AddOrder(Order.Asc("ListOrder"));

        rptClassify.DataSource = possService.myService.ExecutableDetachedCriteria<ProductClassifyVO>(dCriteria);
        rptClassify.DataBind();
    }

    protected void rptBrandType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            ItemParamVO itemParamVO = (ItemParamVO)e.Item.DataItem;

            //分類下的品牌 
            Repeater rptBrand = (Repeater)ctrl.FindControl("rptBrand");
            rptBrand.DataSource = possService.GetAllBrandVO_ByClassifyId(itemParamVO.Id);
            rptBrand.DataBind();
        }
    }
}
