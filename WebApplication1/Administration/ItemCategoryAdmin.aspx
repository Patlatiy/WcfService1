<%@ Page Title="Item category administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemCategoryAdmin.aspx.cs" Inherits="WebStore.Administration.ItemCategoryAdmin" %>

<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>
<%@ Register Assembly="WebStore" Namespace="WebStore.Controls" TagPrefix="ws" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function OpenPopup() {
            popup("/Administration/NewCategoryPopup.aspx");
        }

        function popup(url) {
            var width = 300;
            var height = 200;
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
    </script>
    <ws:Nav ID="Nav1" runat="server" />
    <asp:Panel runat="server" ID="AdminPanel">
        <asp:ListView runat="server"
            ID="CategoryList"
            DataKeyNames="ID"
            ItemType="WebStore.App_Data.Model.ItemCategory"
            SelectMethod="GetCategories">
            <LayoutTemplate>
                <table class="bottomBorder">
                    <tr>
                        <td>
                            <b>Category name</b>
                        </td>
                        <td>
                            <b>Category desription</b>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="ItemPlaceholder" />
                    <tr>
                        <td style="border-bottom: transparent">
                            <asp:Button runat="server"
                                OnClientClick="OpenPopup()"
                                Text="New category"
                                Font-Size="9" />
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr style="vertical-align: top">
                    <td>
                        <ws:ValueTextBox runat="server"
                            Text="<%#: Item.Name %>"
                            Font-Size="10"
                            Width="125"
                            OnTextChanged="Name_Changed"
                            AutoPostBack="True"
                            Value="<%#: Item.ID %>" />
                    </td>
                    <td>
                        <ws:ValueTextBox runat="server"
                            Text="<%#: Item.Description %>"
                            Font-Size="10"
                            Width="300"
                            Height="17"
                            TextMode="MultiLine"
                            Wrap="True"
                            OnTextChanged="Description_Changed"
                            AutoPostBack="True"
                            Value="<%#: Item.ID %>" />
                    </td>
                    <td>
                        <asp:Button runat="server"
                            Text="Delete"
                            Font-Size="9"
                            CommandName="<%#: Item.ID %>"
                            OnClick="DeleteButton_Clicked" 
                            OnClientClick="return confirm('Do you really want to delete this item category?')"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
