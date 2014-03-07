<%@ Page Title="Item administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemAdmin.aspx.cs" Inherits="WebStore.Administration.ItemAdmin" %>

<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>
<%@ Register Assembly="WebStore" Namespace="WebStore.Controls" TagPrefix="ws" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ws:Nav runat="server" />
    <asp:DropDownList runat="server"
        ID="FileList"
        OnLoad="PopulateList" 
        Visible="False"/>
    <asp:Panel runat="server" ID="AdminPanel">
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
                            <b>Item name and description</b>
                        </td>
                        <td>
                            <b>In store</b>
                        </td>
                        <td>
                            <b>Price</b>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="ItemPlaceholder" />
                </table>

            </LayoutTemplate>
            <ItemTemplate>
                <tr style="vertical-align: top">
                    <td>
                        <ws:ListWithValue runat="server"
                            ID="FileList"
                            Value="<%#: Item.ID %>"
                            SelectedValue="<%# GetFileNameForItem(Item.ID) %>"
                            OnLoad="CopyList"
                            OnSelectedIndexChanged="ImageIndex_Changed"
                            AutoPostBack="True"
                            Width="200" />
                        <br />
                        <img src="/Images/Items/<%#: Item.Image %>" alt="Item image" />
                    </td>
                    <td>
                        <ws:ValueTextBox runat="server" Text="<%#: Item.Name %>" Width="150"
                            AutoPostBack="True" OnTextChanged="Name_Changed" Value="<%#: Item.ID %>" />
                        <br />
                        <ws:ValueTextBox runat="server" Text="<%#: Item.Description %>" Font-Size="9"
                            AutoPostBack="True" OnTextChanged="Description_Changed" Value="<%#: Item.ID %>"
                            TextMode="MultiLine" Wrap="True" Width="300" />
                    </td>

                    <td>
                        x<ws:ValueTextBox runat="server" Text="<%#: Item.Quantity %>" Font-Size="9" Width="40"
                            AutoPostBack="True" OnTextChanged="Quantity_Changed" Value="<%#: Item.ID %>" />
                    </td>
                    <td>
                        $<ws:ValueTextBox runat="server" Text="<%#: Item.Price %>" Font-Size="9" Width="50"
                            AutoPostBack="True" OnTextChanged="Price_Changed" Value="<%#: Item.ID %>" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
