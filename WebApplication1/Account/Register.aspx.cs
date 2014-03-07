using System;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using WebStore.Managers;

namespace WebStore.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);
            var shownNameTextBox = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("ShownName");
            var nameTextBox = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("UserName");
            UserManager.SetShownName(nameTextBox.Text, shownNameTextBox.Text);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);

        }

        protected void PasswordValidation(object source, ServerValidateEventArgs args)
        {
            var expString = Membership.Provider.PasswordStrengthRegularExpression;
            var exp = new Regex(expString);
            args.IsValid = (exp.IsMatch(args.Value));
        }

        protected void EmailValidation(object source, ServerValidateEventArgs args)
        {
            const string theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                           + "@"
                                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            var exp = new Regex(theEmailPattern);
            args.IsValid = (exp.IsMatch(args.Value));
        }

        protected void EmailExistsValidation(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !UserManager.EmailExists(args.Value);
        }

        protected void LoginExistsValidation(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !UserManager.LoginExists(args.Value);
        }
    }
}