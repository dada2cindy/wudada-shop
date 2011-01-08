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

public partial class schema : BasePage
{
    readonly string PASS_WORD = "dada168";    
    readonly string[] ASSEMBLES = new string[] { "WuDADA-Console", "WuDADA-Console.Member", "WuDADA-Console.Auth", "WuDADA-Console.Poss", "WuDADA-Console.Shop" };

    IAuthService authService;

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
        authService = (IAuthService)ctx.GetObject("AuthService");

        if (!IsPostBack)
        {
            HttpHelper httpHelper = new HttpHelper();
            string ip = httpHelper.GetUserIp(HttpContext.Current);

            if ("127.0.0.1".Equals(ip))
            {
                hdnIsOK.Value = "Y";
            }
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
        spVO1.Account = "dada@pro2e.com.tw";
        spVO1.SendEmail = "dada@pro2e.com.tw";
        spVO1.MailPort = "587";
        spVO1.IsEnableSSL = true;
        spVO1.Password = "28005786";
        spVO1.ComName = "WuDADA";
        spVO1.ComEngName = "WuDADA";
        spVO1.ComCEO = "WuDADA";

        myService.DaoInsert(spVO1);
    }

    private void initMenu()
    {
        //商店
        initMenu_Shop();
        //進銷存
        initMenu_Poss();
        //權限
        initMenu_Auth();
        //系統設定
        iniMenu_System();
        //個人專區
        iniMenu_Person();

        grantAdminPermit();
    }

    private void iniMenu_Person()
    {
        MenuFunc menuFunc = new MenuFunc("個人專區", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "使用者資訊", "admin/personal/LoginUserDetail.aspx");
    }

    private void iniMenu_System()
    {
        MenuFunc menuFunc = new MenuFunc("系統管理", null);
        myService.DaoInsert(menuFunc);

        MenuFunc m1 = authService.AddSubMenu(menuFunc, "系統參數設定", "admin/system/SystemSettingList.aspx");
        MenuFunc m2 = authService.AddSubMenu(menuFunc, "系統更新LOG", "admin/system/SystemUpdateList.aspx");
        MenuFunc m3 = authService.AddSubMenu(menuFunc, "系統LOG查詢", "admin/system/SystemLogList.aspx");
    }

    private void initMenu_Poss()
    {
        MenuFunc menuFunc = new MenuFunc("進銷存管理", null);
        myService.DaoInsert(menuFunc);
    }

    private void initMenu_Shop()
    {
        MenuFunc menuFunc = new MenuFunc("商店管理", null);
        myService.DaoInsert(menuFunc);
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
}
