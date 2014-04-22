<%@ Page Title="Item administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemAdmin.aspx.cs" Inherits="WebStore.Administration.ItemAdmin" %>

<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>
<%@ Register Assembly="WebStore" Namespace="WebStore.Controls" TagPrefix="ws" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function OpenPopup() {
            hideLabels();
            popup("/Administration/NewItemPopup.aspx");
            return false;
        }

        function popup(url) {
            var width = 800;
            var height = 300;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ', height=' + height;
            params += ', top=' + top + ', left=' + left;
            params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=no';
            params += ', toolbar=no';
            var newwin = window.open(url, 'windowname5', params);
            if (window.focus) {
                newwin.focus();
            }
            return false;
        }

        function hideLabels() {
            $(".successlabel").hide();
        }

        function changePicture(sender) {
            var rand = sender.parentElement.children[2];
            rand.src = "/Images/Items/" + sender.value;
        }

    </script>
    <ws:Nav runat="server" />
    <asp:DropDownList runat="server"
        ID="FileList"
        OnLoad="PopulateList"
        Visible="False" />
    <asp:Panel runat="server" ID="AdminPanel">
        <asp:UpdatePanel runat="server"
            UpdateMode="Conditional">
            <ContentTemplate>
                <span>Show items in category:
                <asp:DropDownList runat="server"
                    ID="FilterList"
                    OnLoad="FillFilterList"
                    AutoPostBack="True" />
                </span>
                <br />
                <span style="font-size: 10px">
                    <asp:Button ID="Button1" runat="server"
                        Text="Save changes"
                        UseSubmitBehavior="False"
                        OnClick="SaveChanges"
                        OnClientClick="hideLabels" />
                    <asp:Button ID="Button2" runat="server"
                        Text="New Item"
                        OnClientClick="OpenPopup(); return false;" />
                    <span runat="server" id="ChangesSavedLabel1" visible="False" class="successlabel">Changes has been saved!
                    </span>
                </span>
                <span>
                    <br/>
                    Page:
                    <asp:DataPager ID="Pager" runat="server" PagedControlID="ItemList" PageSize="5">
                        <Fields>
                            <asp:NumericPagerField />
                        </Fields>
                    </asp:DataPager>
                </span>
                <asp:ListView runat="server"
                    ID="ItemList"
                    DataKeyNames="ID"
                    ItemType="WebStore.App_Data.Model.Item"
                    SelectMethod="GetItems">
                    <LayoutTemplate>

                        <table class="bottomBorder">
                            <tr>
                                <td>
                                    <b>Item image</b>
                                </td>
                                <td>
                                    <b>Item 
                                        <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="Name">name</asp:LinkButton>,
                                        <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="Description">description</asp:LinkButton>
                                         and 
                                        <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="CategoryName">category</asp:LinkButton>
                                    </b>
                                </td>
                                <td>
                                    <b>
                                        <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="Quantity">In store</asp:LinkButton>
                                    </b>
                                </td>
                                <td>
                                    <b>
                                        <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="Price">Price</asp:LinkButton>
                                    </b>
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="ItemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="vertical-align: top">
                            <td>
                                <ws:ListWithValue runat="server"
                                    Value="<%#: Item.ID %>"
                                    OnPreRender="CopyList"
                                    OnSelectedIndexChanged="ImageIndex_Changed"
                                    onchange="hideLabels(); changePicture(this);"
                                    AutoPostBack="False"
                                    Width="200" />
                                <br />
                                <img src="/Images/Items/<%#: Item.Image %>" alt="Item image" id="image"/>
                            </td>
                            <td>Name:<br />
                                <ws:ValueTextBox runat="server"
                                    Text="<%#: Item.Name %>"
                                    Width="150"
                                    AutoPostBack="False"
                                    OnTextChanged="Name_Changed"
                                    onchange="hideLabels();"
                                    Value="<%#: Item.ID %>" />
                                <br />
                                Description:<br/>
                                <ws:ValueTextBox runat="server"
                                    Text="<%#: Item.Description %>"
                                    Font-Size="9"
                                    AutoPostBack="False"
                                    OnTextChanged="Description_Changed"
                                    onchange="hideLabels();"
                                    Value="<%#: Item.ID %>"
                                    TextMode="MultiLine"
                                    Wrap="True"
                                    Width="300"
                                    Height="30" />
                                <br />
                                Category:<br />
                                <ws:ListWithValue runat="server"
                                    Value="<%#: Item.ID %>"
                                    OnPreRender="FillDropDownWithCategories"
                                    OnSelectedIndexChanged="Category_Changed"
                                    onchange="hideLabels();" />
                            </td>
                            <td>x<ws:ValueTextBox runat="server"
                                Text="<%#: Item.Quantity %>"
                                Font-Size="9"
                                Width="40"
                                AutoPostBack="False"
                                OnTextChanged="Quantity_Changed"
                                onchange="hideLabels();"
                                Value="<%#: Item.ID %>" />
                            </td>
                            <td>$<ws:ValueTextBox runat="server"
                                Text="<%#: Item.Price %>"
                                Font-Size="9"
                                Width="50"
                                AutoPostBack="False"
                                OnTextChanged="Price_Changed"
                                onchange="hideLabels();"
                                Value="<%#: Item.ID %>" />
                            </td>
                            <td>
                                <asp:Button runat="server"
                                    Font-Size="10px"
                                    Text="Delete"
                                    CommandName="<%#: Item.ID %>"
                                    OnClick="DeleteItem"
                                    OnClientClick="return confirm('Do you really want to delete this item?')" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
