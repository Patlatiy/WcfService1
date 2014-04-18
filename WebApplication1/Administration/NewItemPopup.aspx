<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewItemPopup.aspx.cs" Inherits="WebStore.Administration.NewItemPopup" %>

<!DOCTYPE html>
<script type="text/javascript">
    function SubmitPage() {
        window.opener.location.reload(false);
        this.close();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">

    function changepicture() {
        var img = document.getElementById("ItemImage");
        var filelist = document.getElementById("FileList");
        img.src = "/Images/Items/" + filelist.options[filelist.selectedIndex].text;
        //$("img").attr("src", "/Images/Items/" + $("#FileList :selected").text());

    }
</script>
<head runat="server">
    <title>Create new item</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="AdminPanel" Visible="False">
            <div>
                <table>
                    <tr style="vertical-align: top">
                        <td>
                            <br />
                            <asp:DropDownList runat="server"
                                ID="FileList"
                                OnLoad="FillDropDownWithImagePaths"
                                AutoPostBack="False"
                                Width="200" />
                            <img id="ItemImage" src="/Images/Items/noimage.png" alt="Item image" />
                        </td>
                        <td>Name:<br />
                            <asp:TextBox runat="server"
                                ID="NameTextBox"
                                Width="150"
                                AutoPostBack="False" />
                            <br />
                            Description:<br />
                            <asp:TextBox runat="server"
                                ID="DescriptionTextBox"
                                Font-Size="9"
                                AutoPostBack="False"
                                TextMode="MultiLine"
                                Wrap="True"
                                Width="300"
                                Height="30" />
                            <br />
                            Category:<br />
                            <asp:DropDownList runat="server"
                                ID="CategoryList"
                                OnPreRender="FillDropDownWithCategories" />
                        </td>
                        <td>x<asp:TextBox runat="server"
                            ID="QuantityTextBox"
                            Text="0"
                            Font-Size="9"
                            Width="40"
                            AutoPostBack="False" />
                        </td>
                        <td>$<asp:TextBox runat="server"
                            ID="PriceTextBox"
                            Text="0"
                            Font-Size="9"
                            Width="50"
                            AutoPostBack="False" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="SubmitButton"
                    runat="server"
                    Text="Submit"
                    OnClick="SubmitPage" />
                <br />
                <asp:Label runat="server"
                    ID="ErrorLabel"
                    Text="An error occured. Try again!"
                    Font-Bold="True"
                    ForeColor="orangered"
                    Visible="False" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>
