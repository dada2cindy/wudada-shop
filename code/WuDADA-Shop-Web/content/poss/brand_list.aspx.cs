﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.poss;
using com.wudada.web.util;
using NHibernate.Criterion;
using com.wudada.console.service.poss.vo;
using com.wudada.console.generic.util;
using com.wudada.console.service.common.vo;
using com.wudada.web.util.page;
using NHibernate.SqlCommand;
using com.wudada.console.service.system.vo;
using com.wudada.web.sessionstate;

public partial class content_poss_brand_list : BasePage
{
    IPossService possService;
    readonly string SHOW_PIC = ConfigHelper.PictureShow;
    SessionHelper sessionHelper = new SessionHelper();

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        possService = (IPossService)ctx.GetObject("PossService");

        if (!Page.IsPostBack)
        {
            //品牌類別
            if (!string.IsNullOrEmpty(Request.QueryString["bType"]))
            {
                hdnBType.Value = Request.QueryString["bType"];
            }
            //關鍵字
            if (!string.IsNullOrEmpty(Request.QueryString["key"]))
            {
                hdnKey.Value = Request.QueryString["key"];
            }
            
            LoadDataToUI();
        }
    }

    private void LoadDataToUI()
    {
        //清單文字
        string listName = "品牌清單";
        if (!string.IsNullOrEmpty(hdnBType.Value))  //品牌類別
        {
            ItemParamVO itemParamVO = myService.DaoGetVOById<ItemParamVO>(ConvertUtil.ToInt32(hdnBType.Value));
            listName = string.Format("品牌清單 - {0}", itemParamVO.Name);
        }
        lblListName.Text = listName;

        //關鍵字
        if (!string.IsNullOrEmpty(hdnKey.Value))
        {
            lblKey.Text = string.Format("符合關鍵字：<font color='red'>{0}</font>  的", hdnKey.Value);
        }

        RP_Bind();
    }

    private void RP_Bind()
    {
        DetachedCriteria dCriteria = DetachedCriteria.For(typeof(BrandVO));
        DetachedCriteria dCriteria_Classify = dCriteria.CreateCriteria("Classify", JoinType.LeftOuterJoin);

        //品牌類別
        if (!string.IsNullOrEmpty(hdnBType.Value))
        {
            dCriteria_Classify.Add(Expression.Eq("Id", ConvertUtil.ToInt32(hdnBType.Value)));
        }

        //關鍵字
        if (!string.IsNullOrEmpty(hdnKey.Value))
        {
            dCriteria.Add(Expression.Like("Name", hdnKey.Value, MatchMode.Anywhere));
        }

        //分店        
        if (string.IsNullOrEmpty(sessionHelper.SelectShop))
        {
            sessionHelper.SelectShop = "18";
        }
        log.Error("sessionHelper.SelectShop = " + sessionHelper.SelectShop);
        if (!string.IsNullOrEmpty(sessionHelper.SelectShop))
        {
            dCriteria.CreateCriteria("Shops").Add(Expression.Eq("Id", int.Parse(sessionHelper.SelectShop)));
        }

        //啟用
        dCriteria.Add(Expression.Eq("IsEnable", true));
        dCriteria_Classify.Add(Expression.Eq("Deleted", false));

        dCriteria.AddOrder(Order.Asc("Name"));

        AspNetPager1.RecordCount = myService.CountDetachedCriteriaRow(dCriteria);

        int maxRecord = AspNetPager1.PageSize;
        int startIndex = AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1);

        Repeater1.DataSource = myService.ExecutableDetachedCriteria<BrandVO>(dCriteria, startIndex, maxRecord);
        Repeater1.DataBind();
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            BrandVO brandVO = (BrandVO)e.Item.DataItem;

            //圖片
            FileVO fileVO = possService.Get_FirstFile(brandVO);
            if (fileVO != null)
            {
                string cssStyle = "";
                if (((e.Item.ItemIndex + 1) % 3).Equals(0))
                {
                    cssStyle = "class='last'";
                }
                UIHelper.SetLiteralText(ctrl, "ltlPic", string.Format("<li {0}><table style='border-width: 0px;' ><tr><td width='100%' align='left' style='border-width: 0px;'><p class='txt-normal'>{5}</p><a href='../poss/brand_detail.aspx?bid={1}&bType={2}'><img src='{3}?type=fjx&fileName={4}&auto=h&size=120' alt='' /></a></td></tr></table></li>"
                    , cssStyle, brandVO.Id, hdnBType.Value, SHOW_PIC, fileVO.Path, brandVO.Name));
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        RP_Bind();
    }
}
