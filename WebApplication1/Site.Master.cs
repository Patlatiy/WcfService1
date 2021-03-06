﻿using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.DbWorker;

namespace WebStore
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && Session["ID"] == null)
            {
                var usr = DbContext.Instance.Users.First(user => user.Login == Page.User.Identity.Name);
                Session["ID"] = usr.ID;
                Session["Name"] = usr.Name;
                Session["Login"] = usr.Login;
                Session["Email"] = usr.Email;
            }
        }

        protected void Page_PreRender(object source, EventArgs e)
        {
            var loginName1 = (Label)LoginView1.FindControl("LoginName1");
            if (loginName1 != null && Session["Name"] != null)
            {
                loginName1.Text = (string)Session["Name"];
            }
        }

        /// <summary>
        /// Abandons session on user logout
        /// </summary>
        protected void Unnamed1_LoggedOut(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        /// <summary>
        /// Determins if navigation link to admin panel should be displayed or not
        /// </summary>
        /// <returns></returns>
        protected string AdminPanelDisplay()
        {
            if (!Page.User.Identity.IsAuthenticated) 
                return "none";
            return Page.User.IsInRole("Admin") || Page.User.IsInRole("Salesperson") ? string.Empty : "none";
        }
    }
}