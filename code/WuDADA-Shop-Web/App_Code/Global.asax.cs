using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Spring.Context.Support;
using Spring.Context;
using Spring.Data.NHibernate;
using System.IO;
using System.Web.Hosting;
using NHibernate.Impl;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global : System.Web.HttpApplication
{
    public Global()
    { }


    void Application_Start(object sender, EventArgs e)
    {
        // 應用程式啟動時執行的程式碼

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {


    }

    void Session_Start(object sender, EventArgs e)
    {


    }

    void Session_End(object sender, EventArgs e)
    {


    }
}
