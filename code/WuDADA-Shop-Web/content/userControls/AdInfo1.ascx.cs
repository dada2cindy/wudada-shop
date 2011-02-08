using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using Spring.Context;
using Spring.Context.Support;
using com.wudada.console.service.advertisement;
using com.wudada.console.var;
using com.wudada.web.util;
using NHibernate.Criterion;
using com.wudada.console.service.advertisement.vo;

public partial class content_userControls_AdInfo1 : System.Web.UI.UserControl
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IAdService adService;
    readonly int LIST_COUNT = 5;   
    readonly string PIC_DIR = ConfigHelper.AdPicURL;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void RP_Bind(int classId)
    {
        IApplicationContext ctx = ContextRegistry.GetContext();
        adService = (IAdService)ctx.GetObject("AdService");

        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(AdInfoVO));

        //分類
        dCriteria.CreateCriteria("Classify").Add(Expression.Eq("Id", classId));

        //啟用
        dCriteria.Add(Expression.Eq("IsEnable", true));

        dCriteria.AddOrder(Order.Asc("ListOrder"));

        Repeater1.DataSource = adService.myService.ExecutableDetachedCriteria<AdInfoVO>(dCriteria, 0, LIST_COUNT);
        Repeater1.DataBind();
    }
}
