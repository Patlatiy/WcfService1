<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCategoryPopup.aspx.cs" Inherits="WebStore.Administration.NewCategoryPopup" %>

<!DOCTYPE html>
<script type="text/javascript">
    function SubmitPage() {
        //window.opener.document.getElementById('TextBox1').value = document.getElementById('TextBox1').value;
        //window.opener.document.getElementById('TextBox2').value = document.getElementById('TextBox2').value;
        //window.opener.document.getElementById('TextBox3').value = document.getElementById('TextBox3').value;
        //window.opener.document.getElementById('TextBox1').focus();
        window.opener.location.reload(false);
        this.close();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Name:
            <asp:TextBox 
                ID="NameTextBox" 
                runat="server" 
                ClientIDMode="Static" />
            <asp:RequiredFieldValidator runat="server"
                ErrorMessage="Please enter category name"
                ControlToValidate="NameTextBox"
                Display="Dynamic"/>
            <br/>
            Description:
            <asp:TextBox 
                ID="DescriptionTextBox" 
                runat="server" 
                ClientIDMode="Static" />
            <br/>
            <asp:Button 
                runat="server" 
                Text="Submit" 
                OnClick="SubmitPage" />
            <br/>
            <asp:Label runat="server" 
                ID="ErrorLabel" 
                Text="An error occured. Try again or go whine about it." 
                Font-Bold="True" 
                ForeColor="orangered" 
                Visible="False"/>
        </div>
    </form>
</body>
</html>
