using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wudada.web.page;
using com.wudada.console.service.auth;
using com.wudada.console.service.member;
using com.wudada.console.service.common;
using com.wudada.web;
using System.IO;
using com.wudada.console.generic.util;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using com.wudada.console.service.member.vo;
using com.wudada.console.service.auth.vo;
using NHibernate.Impl;
using Spring.Core.IO;
using System.Xml;
using com.wudada.console.service.system.vo;
using com.wudada.console.service.system;

public partial class schema : BasePage
{
    readonly string PASS_WORD = "dada168";
    readonly string[] ASSEMBLES = new string[] { "WuDADA-Console", "WuDADA-Console.Member", "WuDADA-Console.Auth", "WuDADA-Console.Poss", "WuDADA-Console.Shop", "WuDADA-Console.Information", "WuDADA-Console.Advertisement" };

    IAuthService authService;
    ISystemService systemService;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        authService = (IAuthService)ctx.GetObject("AuthService");
        systemService = (ISystemService)ctx.GetObject("SystemService");

        if (!IsPostBack)
        {
            HttpHelper httpHelper = new HttpHelper();
            string ip = httpHelper.GetUserIp(HttpContext.Current);

            if ("127.0.0.1".Equals(ip))
            {
                hdnIsOK.Value = "Y";
            }

            GridView1.DataSource = myService.DaoGetAllVO<ItemParamVO>();
            GridView1.DataBind();
        }

        setInitPanel();
    }

    private void setInitPanel()
    {
        if (hdnIsOK.Value.Equals("Y"))
        {
            pnlContent.Visible = true;
            pnlLogin.Visible = false;
        }
        else
        {
            pnlContent.Visible = false;
            pnlLogin.Visible = true;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string tmpPath = Path.GetTempFileName();

            FileUpload1.SaveAs(tmpPath);

            string des = Server.MapPath("");

            ZipHelper.Uncompress(tmpPath, des);

            lblMsgUpload.Text = "更新成功";
        }
        catch (Exception ex)
        {
            lblMsgUpload.Text = ex.Message;
        }
    }

    protected void btnAddSchema_Click(object sender, EventArgs e)
    {
        Configuration cfg = GetCfg();
        SchemaExport export = new SchemaExport(cfg);
        export.Create(false, true);
        lblStatus.Text = "Schema created";
    }

    protected void btnDropSchema_Click(object sender, EventArgs e)
    {
        try
        {
            Configuration cfg = GetCfg();
            SchemaExport export = new SchemaExport(cfg);
            export.Drop(false, true);

            lblStatus.Text = "Schema dropped";
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }
    }

    protected void btnGenDDL_Click(object sender, EventArgs e)
    {
        Configuration cfg = GetCfg();

        SchemaExport export = new SchemaExport(cfg);
        string tmpFile = Path.GetTempFileName();

        export.SetOutputFile(tmpFile);
        export.Create(true, false);

        Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", HttpUtility.UrlEncode("schema.txt")));
        Response.WriteFile(tmpFile);
        Response.Flush();

        Response.End();

        lblStatus.Text = "Schema genenrated success";
    }

    protected void btnInitData_Click(object sender, EventArgs e)
    {
        //建立系統參數
        intiSystemParam();
        //建立權限/登入者
        initAuthData();
        //建立功能
        initMenu();
    }

    private void intiSystemParam()
    {
        //系統設定
        SystemParamVO spVO1 = new SystemParamVO();
        spVO1.MailSmtp = "smtp.gmail.com";
        spVO1.Account = "test@pro2e.com.tw";
        spVO1.SendEmail = "dada@pro2e.com.tw";
        spVO1.MailPort = "587";
        spVO1.IsEnableSSL = true;
        spVO1.Password = "28005786";
        spVO1.ComName = "WuDADA";
        spVO1.ComEngName = "WuDADA";
        spVO1.ComCEO = "WuDADA";
        myService.DaoInsert(spVO1);

        //公司簡介
        myService.DaoInsert(new ItemParamVO("公司簡介", "關於新明", "關於新明"));
        myService.DaoInsert(new ItemParamVO("公司簡介", "聯絡資訊", "聯絡資訊"));

        //品牌管理
        myService.DaoInsert(new ItemParamVO("品牌管理", "日系品牌", "日系品牌"));
        myService.DaoInsert(new ItemParamVO("品牌管理", "歐系品牌", "歐系品牌"));

        //資訊管理
        myService.DaoInsert(new ItemParamVO("資訊管理", "最新消息", "最新消息"));
        myService.DaoInsert(new ItemParamVO("資訊管理", "眼鏡與我", "眼鏡與我"));

        //廣告管理
        myService.DaoInsert(new ItemParamVO("廣告管理", "首頁廣告", "首頁廣告"));
        myService.DaoInsert(new ItemParamVO("廣告管理", "最新商品", "最新商品"));
        myService.DaoInsert(new ItemParamVO("廣告管理", "最新消息", "最新消息"));
        myService.DaoInsert(new ItemParamVO("廣告管理", "眼鏡與我", "眼鏡與我"));
    }

    private void initMenu()
    {
        //公司簡介
        initComInfo();
        //商店
        initMenu_Shop();
        //文章資訊管理
        initInfo();
        //廣告管理
        initAdInfo();
        //進銷存
        //initMenu_Poss();
        //系統管理
        iniMenu_System();
        //個人專區
        //iniMenu_Person();
        //權限
        initMenu_Auth();

        grantAdminPermit();
    }

    private void initComInfo()
    {
        MenuFunc menuFunc = new MenuFunc("公司簡介", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "關於新明", "admin/system/ComInfo1.aspx");
        MenuFunc m2 = authService.AddSubMenu(menuFunc, "聯絡資訊", "admin/system/ComInfo2.aspx");
    }

    private void initMenu_Shop()
    {
        MenuFunc menuFunc = new MenuFunc("商店管理", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "產品分類管理", "admin/shop/ClassifyList.aspx");
        authService.AddOtherPath(m1, "admin/shop/ClassifyDetail.aspx");

        MenuFunc m4 = authService.AddSubMenu(menuFunc, "品牌分類管理", "admin/shop/BrandClassifyList.aspx");
        authService.AddOtherPath(m4, "admin/shop/BrandClassifyDetail.aspx");

        MenuFunc m2 = authService.AddSubMenu(menuFunc, "品牌管理", "admin/shop/BrandList.aspx");
        authService.AddOtherPath(m2, "admin/shop/BrandDetail.aspx");

        MenuFunc m3 = authService.AddSubMenu(menuFunc, "商品管理", "admin/shop/ProductList.aspx");
        authService.AddOtherPath(m3, "admin/shop/ProductDetail.aspx");
    }

    private void initInfo()
    {
        MenuFunc menuFunc = new MenuFunc("資訊管理", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "最新消息管理", "admin/info/NewsList.aspx");
        authService.AddOtherPath(m1, "admin/info/NewsDetail.aspx");

        MenuFunc m2 = authService.AddSubMenu(menuFunc, "眼鏡與我管理", "admin/info/InfoList.aspx");
        authService.AddOtherPath(m2, "admin/info/InfoDetail.aspx");
    }

    private void initAdInfo()
    {
        MenuFunc menuFunc = new MenuFunc("廣告管理", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "首頁廣告管理", "admin/adInfo/Ad1_List.aspx");
        authService.AddOtherPath(m1, "admin/adInfo/Ad1_Detail.aspx");

        MenuFunc m2 = authService.AddSubMenu(menuFunc, "最新商品廣告管理", "admin/adInfo/Ad2_List.aspx");
        authService.AddOtherPath(m2, "admin/adInfo/Ad2_Detail.aspx");

        MenuFunc m3 = authService.AddSubMenu(menuFunc, "最新消息廣告管理", "admin/adInfo/Ad3_List.aspx");
        authService.AddOtherPath(m3, "admin/adInfo/Ad3_Detail.aspx");

        MenuFunc m4 = authService.AddSubMenu(menuFunc, "眼鏡與我廣告管理", "admin/adInfo/Ad4_List.aspx");
        authService.AddOtherPath(m4, "admin/adInfo/Ad4_Detail.aspx");
    }

    private void initMenu_Poss()
    {
        MenuFunc menuFunc = new MenuFunc("進銷存管理", null);
        myService.DaoInsert(menuFunc);
    }

    private void iniMenu_System()
    {
        MenuFunc menuFunc = new MenuFunc("系統管理", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "系統參數設定", "admin/system/SystemSet.aspx");
        //MenuFunc m2 = authService.AddSubMenu(menuFunc, "系統更新LOG", "admin/system/SystemUpdateList.aspx");
        //MenuFunc m3 = authService.AddSubMenu(menuFunc, "系統LOG查詢", "admin/system/SystemLogList.aspx");
    }

    private void iniMenu_Person()
    {
        MenuFunc menuFunc = new MenuFunc("個人專區", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "使用者資訊", "admin/personal/LoginUserDetail.aspx");
    }

    /// <summary>
    /// 權限設定
    /// </summary>
    private void initMenu_Auth()
    {
        MenuFunc menuFunc = new MenuFunc("權限管理", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "帳號管理", "admin/auth/UserList.aspx");
        authService.AddOtherPath(m1, "admin/auth/UserDetail.aspx");

        MenuFunc m2 = authService.AddSubMenu(menuFunc, "群組管理", "admin/auth/RoleList.aspx");
        authService.AddOtherPath(m2, "admin/auth/RoleDetail.aspx");

        authService.AddSubMenu(menuFunc, "帳號群組設定", "admin/auth/UserRoleSet.aspx");
        authService.AddSubMenu(menuFunc, "群組權限設定", "admin/auth/RoleFuncSet.aspx");

    }

    private void grantAdminPermit()
    {
        LoginRole role = myService.DaoGetVOById<LoginRole>(1);

        role.MenuFuncs = new List<MenuFunc>();

        IList<MenuFunc> allFunc = myService.DaoGetAllVO<MenuFunc>();

        foreach (MenuFunc m in allFunc)
        {
            role.MenuFuncs.Add(m);
        }

        myService.DaoUpdate(role);
    }

    private void initMemberRole()
    {
        MemberRoleVO memberRoleVO = new MemberRoleVO();
        memberRoleVO.RoleName = "評審";
        myService.DaoInsert(memberRoleVO);

    }

    private void initAuthData()
    {
        LoginUser user = new LoginUser();
        LoginRole role = new LoginRole();
        user.UserId = "admin";
        user.Password = EncryptUtil.GetMD5(PASS_WORD);
        user.Name = "系統管理者";
        user.IsEnable = true;
        user.Email = "test@pro2e.com.tw";
        myService.DaoInsert(user);

        role.RoleName = "系統管理者";
        myService.DaoInsert(role);

        List<LoginUser> userList = new List<LoginUser>();
        List<LoginRole> roleList = new List<LoginRole>();
        roleList.Add(role);
        user.BelongRoles = new List<LoginRole>();

        role.BelongUsers = userList;
        user.BelongRoles = roleList;
        myService.DaoUpdate(role);
        myService.DaoUpdate(user);

        lblStatus.Text = "初始化資料成功!!";
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/login/login.aspx");
    }

    private Configuration GetCfg()
    {
        SessionFactoryImpl s = (SessionFactoryImpl)ctx.GetObject("NHibernateSessionFactory");

        IResource resource = ctx.GetResource("file://~/App_Data/Dao.xml");
        IDictionary<string, string> dic = new Dictionary<string, string>();

        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(resource.InputStream);

        XmlElement root = xdoc.DocumentElement;

        XmlNodeList nodeList = root.GetElementsByTagName("object");

        foreach (XmlNode node in nodeList)
        {
            if (node.Attributes["id"] != null && "NHibernateSessionFactory".Equals(node.Attributes["id"].Value))
            {
                XmlNodeList nodeList2 = node.ChildNodes[3].FirstChild.ChildNodes;

                foreach (XmlNode node2 in nodeList2)
                {
                    string key = node2.Attributes["key"].Value;
                    string value = node2.Attributes["value"].Value;
                    // log.Debug(key + ":" + value);
                    dic.Add(key, value);
                }
            }
        }

        XmlNode dbNode = root.GetElementsByTagName("db:provider")[0];
        string connectionStr = dbNode.Attributes["connectionString"].Value;

        dic.Add("connection.connection_string", connectionStr);
        Configuration cfg = new Configuration();
        cfg.AddProperties(dic);

        foreach (string assem in ASSEMBLES)
        {
            cfg.AddAssembly(assem);
        }

        return cfg;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string pw = txtPassword.Text.Trim();

        if (PASS_WORD.Equals(pw))
        {
            hdnIsOK.Value = "Y";
        }
        else
        {
            hdnIsOK.Value = "N";
            lblMsg.Text = "登入失敗";
        }
        setInitPanel();
    }

    private void AddOtherPath(MenuFunc m1, string p)
    {
        if (m1.FuncionPaths == null)
        {
            m1.FuncionPaths = new List<FunctionPath>();
        }

        FunctionPath path = new FunctionPath();
        path.Path = p;
        path.BelongMenuFunc = m1;
        myService.DaoInsert(path);

        m1.FuncionPaths.Add(path);

        myService.DaoUpdate(m1);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        myService.DaoInsert(new ItemParamVO("分店資訊", "中華店", "中華店"));
        myService.DaoInsert(new ItemParamVO("分店資訊", "敦北店", "敦北店"));

        lblStatus.Text = "加入分店資訊成功!!";
    }
}
