using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.var;
using com.wudada.web.util;
using NHibernate.Criterion;
using com.wudada.console.service.information.vo;
using com.wudada.console.service.information;
using com.wudada.console.generic.util;
using com.wudada.web.util.page;

public partial class content_info_news_list : BasePage
{
    IInformationService informationService;
    readonly int PARAM_ID_CLASSIFY = VarHelper.WuDADA_ITEMPARAM_ID_NEWS1;
    readonly string PIC_DIR = ConfigHelper.AdPicURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        informationService = (IInformationService)ctx.GetObject("InformationService");

        if (!Page.IsPostBack)
        {
            LoadDataToUI();
        }
    }

    private void LoadDataToUI()
    {
        RP_Bind();

        //廣告
        ucAdInfo1.RP_Bind(VarHelper.WuDADA_ITEMPARAM_ID_AD_INFO3);
    }

    private void RP_Bind()
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(NewsVO));

        //分類
        dCriteria.CreateCriteria("Classify").Add(Expression.Eq("Id", PARAM_ID_CLASSIFY));
        //啟用
        dCriteria.Add(Expression.Eq("IsEnable", true));
        //日期
        dCriteria.Add(Expression.Le("PublicDate", ConvertUtil.ToDateTimeMax(DateTime.Today)));

        dCriteria.AddOrder(Order.Desc("PublicDate"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);        

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        Repeater1.DataSource = myService.ExecutableDetachedCriteria<NewsVO>(dCriteria, startIndex, maxRecord);
        Repeater1.DataBind();
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            NewsVO newsVO = (NewsVO)e.Item.DataItem;

            string content = ConvertUtil.FilterHtml(newsVO.Content);
            if (!string.IsNullOrEmpty(content) && content.Length > 200)
            {
                UIHelper.SetLabelText(ctrl, "lblContent", content.Substring(0, 100) + "......");
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        RP_Bind();
    }
}
