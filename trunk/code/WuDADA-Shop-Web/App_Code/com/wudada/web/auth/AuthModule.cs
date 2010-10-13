using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using com.wudada.console.service.auth.vo;
using com.wudada.console.service.auth;
using Common.Logging;
using System.Threading;
//using Spring.Context.Support;
using com.wudada.web.sessionstate;

/// <summary>
/// AuthModule 的摘要描述
/// </summary>
/// 
namespace com.wudada.web.auth
{
    public class AuthModule : IHttpModule, IRequiresSessionState
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string MANAGE_LOGIN_PATH = "~/admin/login/Login.aspx";
        private readonly string MANAGE_NOAUTH_PATH = "~/admin/login/NoAuth.aspx";
        private readonly string[] ALLOW_PAGE = new string[] { "admin/login/login.aspx", "admin/login/NoAuth.aspx", "admin/Default.aspx",
        "admin/masterpage/index.aspx", "admin/auth/FunctionPathSet.aspx","FCKeditor",  "admin/login/forgetPass.aspx"};

        //  MySessionHelper mySHelper = new MySessionHelper();

        #region IHttpModule Members

        public void Init(HttpApplication application)
        {
            // application.PreRequestHandlerExecute += new EventHandler(Authorization);
            application.AcquireRequestState += new EventHandler(Authorization);
        }

        public void Dispose() { }

        #endregion

        public void Authorization(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            Uri uri = application.Request.Url;

            string rawUrl = application.Request.RawUrl;
            //只欄aspx
            if (rawUrl.IndexOf("aspx") == -1)
            {
                return;
            }
            //放行畫面不檢查權限
            if (isAllowPage(rawUrl))
            {
                return;
            }

            log.Debug("Uri=" + uri.ToString());
            CheckAuth(application, uri, rawUrl);
        }

        /// <summary>
        /// 檢查權限
        /// </summary>
        /// <param name="application"></param>
        /// <param name="uri"></param>
        /// <param name="rawUrl"></param>
        private void CheckAuth(HttpApplication application, Uri uri, string rawUrl)
        {
            SessionHelper sHelper = new SessionHelper();

            LoginUser loginUser = sHelper.AdminUser;

            string applicationPath = application.Request.ApplicationPath;
            string mamagePath = String.IsNullOrEmpty(applicationPath) ? "/admin" : applicationPath + "/admin";
            mamagePath = mamagePath.Replace("//", "/");
            //後台
            if (rawUrl.StartsWith(mamagePath) == true)
            {
                LoginUser adminVo = sHelper.AdminUser;
                if (adminVo == null)
                {
                    toLoginPage(application.Response);
                    return;
                }

                string userId = loginUser.UserId;

                //判斷路徑是否有權限
                if (!PathHasRight(UserMenuFuncContainer.GetInstance().GetUser(userId), uri, UserMenuFuncContainer.GetInstance().PathFunc))
                {
                    toLoginNoAuthPage(application.Response);
                }

                return;
            }

            ////前台
            //string frontPath = String.IsNullOrEmpty(applicationPath) ? "/content" : applicationPath + "/content";
            //frontPath = frontPath.Replace("//", "/");

            //if (rawUrl.StartsWith(frontPath) == true)
            //{
            //    MemberVO m = sHelper.Member;

            //    if (m == null)
            //    {
            //        log.Debug("front not login");
            //        application.Response.Redirect("~/content/login.aspx");
            //        return;
            //    }
            //}
        }

        private bool PathHasRight(LoginUser loginUser, Uri uri, Dictionary<string, List<int>> pathFunc)
        {
            string url = uri.ToString();

            foreach (string path in pathFunc.Keys)
            {
                if (url.IndexOf(path) != -1)
                {
                    log.Fatal(path.IndexOf(url));

                    IList<int> funIdList = pathFunc[path];

                    //判斷是否有此功能權限
                    if (loginUser.BelongRoles != null && loginUser.BelongRoles.Count > 0)
                    {
                        foreach (LoginRole role in loginUser.BelongRoles)
                        {
                            if (role.MenuFuncs != null && role.MenuFuncs.Count > 0)
                            {
                                foreach (MenuFunc roleMenuFunc in role.MenuFuncs)
                                {
                                    foreach (int id in funIdList)
                                    {
                                        if (id == roleMenuFunc.Id)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }

                        //若未有權限 則丟回false
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            log.Debug("頁面尚未登記於後台程式清單：" + url);
            return false;

        }

        private void toLoginPage(HttpResponse httpResponse)
        {
            httpResponse.Redirect(MANAGE_LOGIN_PATH, false);
            return;
        }
        private void toLoginNoAuthPage(HttpResponse httpResponse)
        {
            httpResponse.Redirect(MANAGE_NOAUTH_PATH, false);
            return;
        }

        private bool isAllowPage(string rawUrl)
        {
            bool isAllow = false;

            foreach (string page in ALLOW_PAGE)
            {
                if (rawUrl.IndexOf(page) != -1)
                {
                    return true;
                }
            }
            return isAllow;
        }
    }
}