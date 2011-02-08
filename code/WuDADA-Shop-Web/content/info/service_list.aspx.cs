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
using com.wudada.console.service.common.vo;

public partial class content_info_service_list : BasePage
{
    IInformationService informationService;
    readonly int PARAM_ID_CLASSIFY = VarHelper.WuDADA_ITEMPARAM_ID_INFO1;
    readonly string SHOW_PIC = ConfigHelper.PictureShow;

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
        ucAdInfo1.RP_Bind(VarHelper.WuDADA_ITEMPARAM_ID_AD_INFO4);
    }

    private void RP_Bind()
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(InfoVO));

        //分類
        dCriteria.CreateCriteria("Classify").Add(Expression.Eq("Id", PARAM_ID_CLASSIFY));
        //啟用
        dCriteria.Add(Expression.Eq("IsEnable", true));
        //日期
        dCriteria.Add(Expression.Le("CreateTime", ConvertUtil.ToDateTimeMax(DateTime.Today)));

        dCriteria.AddOrder(Order.Desc("CreateTime"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        Repeater1.DataSource = myService.ExecutableDetachedCriteria<InfoVO>(dCriteria, startIndex, maxRecord);
        Repeater1.DataBind();
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            InfoVO infoVO = (InfoVO)e.Item.DataItem;

            string content = ConvertUtil.FilterHtml(infoVO.Content);
            if (!string.IsNullOrEmpty(content) && content.Length > 200)
            {
                UIHelper.SetLabelText(ctrl, "lblContent", content.Substring(0, 100) + "......");
            }

            //圖片
            FileVO fileVO = informationService.Get_FirstFile(infoVO);
            if (fileVO != null)
            {
                UIHelper.ImageImgUrl(ctrl, "imgPath", string.Format("{0}?type=fjx&fileName={1}", SHOW_PIC, fileVO.Path));
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        RP_Bind();
    }
}
