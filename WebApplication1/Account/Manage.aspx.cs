using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Membership.OpenAuth;
using WebStore.Managers;

namespace WebStore.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }

        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                // Determine the sections to render
                //var hasLocalPassword = OpenAuth.HasLocalPassword(UserAdmin.Identity.Name);
                //setPassword.Visible = !hasLocalPassword;
                changePassword.Visible = true;

                //CanRemoveExternalLogins = hasLocalPassword;

                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage");

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "Your password has been changed."
                        : message == "SetPwdSuccess" ? "Your password has been set."
                        : message == "RemoveLoginSuccess" ? "The external login was removed."
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
                if (User.Identity.IsAuthenticated)
                {
                    ShownNameTextBox.Text = UserManager.GetShownName(User.Identity.Name);
                }
            }


            // Data-bind the list of external accounts
            var accounts = OpenAuth.GetAccountsForUser(User.Identity.Name);
            CanRemoveExternalLogins = CanRemoveExternalLogins || accounts.Count() > 1;
            externalLoginsList.DataSource = accounts;
            externalLoginsList.DataBind();

        }

        protected void setPassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var result = OpenAuth.AddLocalPassword(User.Identity.Name, password.Text);
                if (result.IsSuccessful)
                {
                    Response.Redirect("~/Account/Manage?m=SetPwdSuccess");
                }
                else
                {

                    newPasswordMessage.Text = result.ErrorMessage;

                }
            }
        }


        protected void externalLoginsList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            var providerName = (string)e.Keys["ProviderName"];
            var providerUserId = (string)e.Keys["ProviderUserId"];
            var m = OpenAuth.DeleteAccount(User.Identity.Name, providerName, providerUserId)
                ? "?m=RemoveLoginSuccess"
                : String.Empty;
            Response.Redirect("~/Account/Manage" + m);
        }

        protected T Item<T>() where T : class
        {
            return GetDataItem() as T ?? default(T);
        }


        protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
        {
            // You can change this method to convert the UTC date time into the desired display
            // offset and format. Here we're converting it to the server timezone and formatting
            // as a short date and a long time string, using the current thread culture.
            return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[never]";
        }

        protected void PasswordValidation(object source, ServerValidateEventArgs args)
        {
            var expString = Membership.Provider.PasswordStrengthRegularExpression;
            var exp = new Regex(expString);
            args.IsValid = (exp.IsMatch(args.Value));
        }

        protected void ShownNameValidation(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args.Value.Length > 0 && args.Value.Length <= 50);
        }

        protected void ChangeShownName(object source, EventArgs args)
        {
            var newName = ShownNameTextBox.Text;
            UserManager.SetShownName(User.Identity.Name, newName);
            Session["Name"] = newName;
        }
    }
}