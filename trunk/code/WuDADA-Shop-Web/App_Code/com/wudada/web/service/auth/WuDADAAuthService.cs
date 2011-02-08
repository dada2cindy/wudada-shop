using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.wudada.web.sessionstate;
using com.wudada.console.service.auth.vo;

namespace com.wudada.web.service.auth
{
    public class WuDADAAuthService
    {
        public WuDADAAuthService()
        {
        }

        public static void FillAuthData(AuthBaseVO authBase, FuncOprt.OprtAction action)
        {
            SessionHelper sessionHelper = new SessionHelper();
            HttpHelper httpHelper = new HttpHelper();

            switch (action)
            {
                case FuncOprt.OprtAction.INSERT:
                    authBase.CreateId = sessionHelper.AdminUser;
                    authBase.CreateTime = DateTime.Now;
                    authBase.UpdateId = sessionHelper.AdminUser;
                    authBase.UpdateTime = DateTime.Now;
                    break;

                case FuncOprt.OprtAction.UPDATE:
                    authBase.UpdateId = sessionHelper.AdminUser;
                    authBase.UpdateTime = DateTime.Now;
                    break;
            }
        }
    }
}
