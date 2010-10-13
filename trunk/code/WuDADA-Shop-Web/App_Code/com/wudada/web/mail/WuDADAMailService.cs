using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using com.wudada.console.generic.mail;
using com.wudada.console.core.service;
using com.wudada.console.service.system.vo;
using Spring.Context;
using Spring.Context.Support;
using com.wudada.console.service.auth.vo;
using com.wudada.console.var;
using com.wudada.console.service.auth;

/// <summary>
/// WuDADAMailService 
/// </summary>
namespace com.wudada.web.mail
{
    public class WuDADAMailService
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        MailService mailService;
        MyService myService;
        IAuthService authService;
        SystemParamVO systemParamVO;

        public WuDADAMailService()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            myService = (MyService)ctx.GetObject("MyService");
            authService = (IAuthService)ctx.GetObject("AuthService");

            systemParamVO = myService.Dao.DaoGetVOById<SystemParamVO>(VarHelper.WuDADA_SYSTEMPARAM_ID);

            bool enableSSL = systemParamVO.IsEnableSSL;
            int port = 25;

            if (systemParamVO.MailSmtp.IndexOf("gmail") != -1)
            {
                enableSSL = true;
                port = 587;
            }
            else if (!string.IsNullOrEmpty(systemParamVO.MailPort))
            {
                port = int.Parse(systemParamVO.MailPort);
            }
            mailService = new MailService(systemParamVO.MailSmtp, port, enableSSL, systemParamVO.Account, systemParamVO.Password);
        }

        /// <summary>
        /// 判斷目前是否只發測試信
        /// </summary>
        /// <param name="mailTo"></param>
        /// <returns></returns>
        private static string GenMailTo(string mailTo)
        {
            string result = "";

            if ("Y".Equals(ConfigHelper.OnTest.ToUpper()) && !string.IsNullOrEmpty(ConfigHelper.TestRecMail))
            {
                result = ConfigHelper.TestRecMail;
            }

            return result;
        }

        /// <summary>
        /// 發送給後台使用者 重設密碼
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="loginUserId"></param>
        public void SendMail_ToLoginUser_ResetPass(string loginUserId)
        {
            LoginUser loginUser = myService.DaoGetVOById<LoginUser>(authService.Get_LoginUser_ByUserId(loginUserId).Id);
            try
            {
                string mailfrom = systemParamVO.SendEmail;
                string mailto = GenMailTo(loginUser.Email);;
                string title = "WuDADA管理後台，密碼變更通知";
                string body = GetMailBodyStr_LoginUser_ResetPass(loginUser);
                mailService.SendMail(mailfrom, mailto, title, body);
                log.Debug("Send LoginUser Pass To " + mailto);
            }
            catch (Exception ex)
            {
                log.Debug(ex);
            }
        }

        /// <summary>
        /// 取得重設後台使用者密碼 寄信的內容
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetMailBodyStr_LoginUser_ResetPass(LoginUser loginUser)
        {
            string strBody = "";

            //重設密碼
            string newPass = authService.ResetLoginPassword(loginUser, 5, 8);

            strBody += loginUser.Name + " 您好：您的新密碼與帳號訊息如下<BR><BR>";
            strBody += "登入帳號： " + loginUser.UserId + "<BR><BR>";
            strBody += "登入密碼： " + newPass + "<BR><BR>";
            //strBody += "登入頁面： " + "<a href=\"" + MEMBER_LOGIN_URL + "\">" + MEMBER_LOGIN_URL + "</a>" + "<BR><BR>";

            return strBody;
        }
    }
}