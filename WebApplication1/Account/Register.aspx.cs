using System;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.Managers;

namespace WebStore.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        /// <summary>
        /// Runs after user is created, sets cookies, additional fields for user and redirects him to continueUrl
        /// </summary>
        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);
            var shownNameTextBox = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("ShownName");
            var nameTextBox = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("UserName");
            UserManager.SetShownName(nameTextBox.Text, shownNameTextBox.Text);

            var continueUrl = RegisterUser.ContinueDestinationPageUrl;
            Response.Redirect(continueUrl);
        }

        /// <summary>
        /// Checks if password fits password strength rules
        /// </summary>
        protected void PasswordValidation(object source, ServerValidateEventArgs args)
        {
            var expString = Membership.Provider.PasswordStrengthRegularExpression;
            var exp = new Regex(expString);
            args.IsValid = (exp.IsMatch(args.Value));
        }

        /// <summary>
        /// Checks if email is correct
        /// </summary>
        protected void EmailValidation(object source, ServerValidateEventArgs args)
        {
            const string theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                           + "@"
                                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            var exp = new Regex(theEmailPattern);
            args.IsValid = (exp.IsMatch(args.Value));
        }

        /// <summary>
        /// Checks if entered email already exists in database
        /// </summary>
        protected void EmailExistsValidation(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !UserManager.EmailExists(args.Value);
        }

        /// <summary>
        /// Checks if entered login already exists in database
        /// </summary>
        protected void LoginExistsValidation(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !UserManager.LoginExists(args.Value);
        }
    }
}