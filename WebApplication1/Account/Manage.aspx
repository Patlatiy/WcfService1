<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="WebStore.Account.Manage" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<%@ Import Namespace="Microsoft.AspNet.Membership.OpenAuth" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>

    <section id="passwordForm" style="width: 49%; float: left">
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="message-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>

        <p>You're logged in as <strong><%: User.Identity.Name %></strong>.</p>

        <asp:PlaceHolder runat="server" ID="setPassword" Visible="false">
            <p>
                You do not have a local password for this site. Add a local
                password so you can log in without an external login.
            </p>
            <fieldset>
                <legend>Set Password Form</legend>
                <ol>
                    <li>
                        <asp:Label runat="server" AssociatedControlID="password">Password</asp:Label>
                        <asp:TextBox runat="server" ID="password" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="password"
                            CssClass="field-validation-error" ErrorMessage="The password field is required."
                            Display="Dynamic" ValidationGroup="SetPassword" />
                        
                        <asp:Label runat="server" ID="newPasswordMessage" CssClass="field-validation-error"
                            AssociatedControlID="password" />
                        
                    </li>
                    <li>
                        <asp:Label runat="server" AssociatedControlID="confirmPassword">Confirm password</asp:Label>
                        <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="confirmPassword"
                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required."
                            ValidationGroup="SetPassword" />
                        <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="confirmPassword"
                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match."
                            ValidationGroup="SetPassword" />
                    </li>
                </ol>
                <asp:Button runat="server" Text="Set Password" ValidationGroup="SetPassword" OnClick="setPassword_Click" />
            </fieldset>
        </asp:PlaceHolder>

        <asp:PlaceHolder runat="server" ID="changePassword" Visible="false">
            <h3>Change password</h3>
            <asp:ChangePassword runat="server" CancelDestinationPageUrl="~/" ViewStateMode="Disabled" RenderOuterTable="false" SuccessPageUrl="Manage?m=ChangePwdSuccess">
                <ChangePasswordTemplate>
                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                    <fieldset class="changePassword">
                        <legend>Change password details</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword">Current password</asp:Label>
                                <asp:TextBox runat="server" ID="CurrentPassword" CssClass="passwordEntry" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
                                    CssClass="field-validation-error" ErrorMessage="This field is required."
                                    ValidationGroup="ChangePassword" />
                            </li>
                            <li style="text-align: left">
                                <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword">New password</asp:Label>
                                <asp:TextBox runat="server" ID="NewPassword" CssClass="passwordEntry" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
                                    CssClass="field-validation-error" ErrorMessage="This field is required."
                                    ValidationGroup="ChangePassword" Display="Dynamic"/>
                                <asp:CustomValidator ID="PasswordStrengthValidator" runat="server" ControlToValidate="NewPassword" CssClass="field-validation-error" 
                                    ErrorMessage="Six characters long, at least one number please."
                                    ValidationGroup="ChangePassword" OnServerValidate="PasswordValidation" Display="Dynamic"/>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword">Confirm new password</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="passwordEntry" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="This field is required."
                                    ValidationGroup="ChangePassword" />
                                <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The new password and confirmation password do not match."
                                    ValidationGroup="ChangePassword" />
                            </li>
                        </ol>
                        <asp:Button runat="server" CommandName="ChangePassword" Text="Change password" ValidationGroup="ChangePassword" />
                    </fieldset>
                </ChangePasswordTemplate>
            </asp:ChangePassword>
        </asp:PlaceHolder>

    </section>
    <section id="shownNameForm" style="width: 49%; float: right">
        <asp:PlaceHolder ID="ChangeShownNamePH" runat="server">
            
            <fieldset>
                <legend>Change shown name</legend>
                <ol>
                    <li>
                        <asp:Label runat="server" ID="ShownNameLabel" AssociatedControlID="ShownNameTextBox">Change shown name</asp:Label>
                        <asp:TextBox runat="server" ID="ShownNameTextBox" />
                        <asp:CustomValidator ID="PasswordStrengthValidator" runat="server" ControlToValidate="ShownNameTextBox" CssClass="field-validation-error" 
                            ErrorMessage="The name is too long" ValidationGroup="ShownNameValidation" OnServerValidate="ShownNameValidation" Display="Dynamic"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ShownNameTextBox" 
                            CssClass="field-validation-error" ErrorMessage="This field is required." ValidationGroup="ShownNameValidation"/>
                    </li>
                </ol>
                <asp:Button ID="Button1" runat="server" ValidationGroup="ShownNameValidation" Text="Set shown name" OnClick="ChangeShownName"/>
            </fieldset>
        </asp:PlaceHolder>
    </section>

    <section id="externalLoginsForm" style="display: none">
        
        <asp:ListView runat="server" ID="externalLoginsList" ViewStateMode="Disabled"
            DataKeyNames="ProviderName,ProviderUserAdminId" OnItemDeleting="externalLoginsList_ItemDeleting">
        
            <LayoutTemplate>
                <h3>Registered external logins</h3>
                <table>
                    <thead><tr><th>Service</th><th>UserAdmin Name</th><th>Last Used</th><th>&nbsp;</th></tr></thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    
                    <td><%# HttpUtility.HtmlEncode(Item<OpenAuthAccountData>().ProviderDisplayName) %></td>
                    <td><%# HttpUtility.HtmlEncode(Item<OpenAuthAccountData>().ProviderUserName) %></td>
                    <td><%# HttpUtility.HtmlEncode(ConvertToDisplayDateTime(Item<OpenAuthAccountData>().LastUsedUtc)) %></td>
                    <td>
                        <asp:Button runat="server" Text="Remove" CommandName="Delete" CausesValidation="false" 
                            ToolTip='<%# "Remove this " + Item<OpenAuthAccountData>().ProviderDisplayName + " login from your account" %>'
                            Visible="<%# CanRemoveExternalLogins %>" />
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:ListView>

        <h3>Add an external login</h3>
        <uc:OpenAuthProviders runat="server" ReturnUrl="~/Account/Manage" />
    </section>
</asp:Content>
