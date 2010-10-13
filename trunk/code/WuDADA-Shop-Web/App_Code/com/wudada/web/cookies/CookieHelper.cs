using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// CookieHelper
/// </summary>
namespace com.wudada.web.cookies
{
    public class CookieHelper
    {
        private readonly string COOKIE_LOGIN_USER = "MY_COOKIE_LOGIN_USER";
        private readonly string COOKIE_LOGIN_PASS = "MY_COOKIE_LOGIN_PASS";

        public CookieHelper()
        {
        }

        /// <summary>
        /// 儲存的登入者Id
        /// </summary>
        public String CookieLoginUser
        {
            get
            {
                return (HttpContext.Current.Request.Cookies[COOKIE_LOGIN_USER] == null ? null : HttpContext.Current.Request.Cookies[COOKIE_LOGIN_USER].Value);
            }

            set
            {
                HttpCookie myCookie = new HttpCookie(COOKIE_LOGIN_USER);
                DateTime now = DateTime.Now;


                myCookie.Value = value;

                myCookie.Expires = now.AddMonths(1);

                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        /// <summary>
        /// 儲存的登入者PassWord
        /// </summary>
        public String CookieLoginPass
        {
            get
            {
                return (HttpContext.Current.Request.Cookies[COOKIE_LOGIN_PASS] == null ? null : HttpContext.Current.Request.Cookies[COOKIE_LOGIN_PASS].Value);
            }

            set
            {
                HttpCookie myCookie = new HttpCookie(COOKIE_LOGIN_PASS);
                DateTime now = DateTime.Now;


                myCookie.Value = value;

                myCookie.Expires = now.AddMonths(1);

                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }
    }
}
