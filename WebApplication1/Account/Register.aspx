<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebStore.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Use the form below to create a new account.</h2>
    </hgroup>

    <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled" OnCreatedUser="RegisterUser_CreatedUser" >
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
            <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                <ContentTemplate>
                    <p class="message-info">
                        Passwords are required to be a minimum of <%: Membership.MinRequiredPasswordLength %> characters in length and should contain at least one number.
                    </p>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>Registration Form</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="ShownName">Shown name</asp:Label>
                                <asp:TextBox runat="server" ID="ShownName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ShownName"
                                    CssClass="field-validation-error" ErrorMessage="This field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="UserName">Login</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="This field is required." Display="Dynamic"/>
                                <asp:CustomValidator runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="This user already exists."
                                    OnServerValidate="LoginExistsValidation" Display="Dynamic"/>
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Email">Email address</asp:Label>
                                <asp:TextBox runat="server" ID="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="This field is required." Display="Dynamic"/>
                                <asp:CustomValidator runat="server"  ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="The email address is not valid."
                                    OnServerValidate="EmailValidation" Display="Dynamic"/>
                                <asp:CustomValidator runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="This email address already exists."
                                    OnServerValidate="EmailExistsValidation" Display="Dynamic"></asp:CustomValidator>
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="This field is required." Display="Dynamic"/>
                                <asp:CustomValidator ID="PasswordStrengthValidator" runat="server" ControlToValidate="Password" CssClass="field-validation-error" 
                                    ErrorMessage="Six characters long, at least one number please." OnServerValidate="PasswordValidation" Display="Dynamic"/>
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="This field is required." />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                            </li>
                        </ol>
                        <asp:Button runat="server" CommandName="MoveNext" Text="Register" />
                    </fieldset>
                </ContentTemplate>
                <CustomNavigationTemplate />
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>