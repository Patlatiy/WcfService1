<%@ Page Title="Order Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderAdmin.aspx.cs" Inherits="WebStore.Administration.OrderAdmin" %>

<%@ Register Assembly="WebStore" Namespace="WebStore.Controls" TagPrefix="ws" %>

<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ws:Nav runat="server" />
    <asp:Panel runat="server" ID="AdminPanel">
        <asp:ListView runat="server"
            ID="OrderList"
            DataKeyNames="ID"
            ItemType="WebStore.App_Data.Model.Order"
            SelectMethod="GetOrders">
            <LayoutTemplate>
                <table class="bottomBorder">
                    <tr>
                        <td>
                            <b>User login</b>
                        </td>
                        <td>
                            <b>Items</b>
                        </td>
                        <td>
                            <b>Total</b>
                        </td>
                        <td>
                            <b>State</b>
                        </td>
                        <td>
                            <b>Date issued</b>
                        </td>
                        <td>
                            <b>Date delivered</b>
                        </td>
                        <td>
                            <b>Comment</b>
                        </td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="ItemPlaceholder" />
                </table>

            </LayoutTemplate>
            <ItemTemplate>
                <tr style="vertical-align: top">
                    <td>
                        <%#: Item.User.Login %>
                    </td>
                    <td>
                        <%#: GetItemsForOrder(Item.ID) %>
                    </td>
                    <td>
                        <%#: GetTotal(Item.ID) %>
                    </td>
                    <td>
                        <ws:ListWithValue ID="StateDropDown" runat="server"
                            SelectMethod="GetAllStates"
                            ItemType="WebStore.App_Data.Model.OrderState"
                            DataTextField="Name"
                            DataValueField="ID"
                            SelectedValue="<%# GetStateIDForOrder(Item.ID) %>"
                            OnSelectedIndexChanged="Index_Changed"
                            AutoPostBack="True"
                            Value="<%# Item.ID %>" />
                    </td>
                    <td>
                        <%#: Item.DateIssued == null ? string.Empty : Item.DateIssued.Value.ToString("d") %>
                    </td>
                    <td>
                        <%#: Item.DateEnded == null ? string.Empty : Item.DateEnded.Value.ToString("d") %>
                    </td>
                    <td>
                        <ws:ValueTextBox runat="server" Text="<%#: Item.Comment %>" Width="150"
                            AutoPostBack="True" OnTextChanged="Comment_Changed" Value="<%#: Item.ID %>"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
