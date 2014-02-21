<%@ Page Title="Order Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderAdmin.aspx.cs" Inherits="WebStore.Administration.OrderAdmin" %>

<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ws:Nav runat="server" />
    <asp:Panel runat="server" ID="AdminPanel" Visible="False">
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
                            <b>User email</b>
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
                        <%#: Item.User.Email %>
                    </td>
                    <td>
                        <%#: GetItemsForOrder(Item.ID) %>
                    </td>
                    <td>
                        <%#: GetTotal(Item.ID) %>
                    </td>
                    <td>
                        <asp:DropDownList ID="StateDropDown" runat="server"
                            SelectMethod="GetAllStates"
                            ItemType="WebStore.App_Data.Model.OrderState"
                            DataTextField="Name"
                            DataValueField="ID"
                            SelectedValue="<%# GetStateIDForOrder(Item.ID) %>"
                            OnSelectedIndexChanged="Index_Changed"
                            AutoPostBack="True"/>
                    </td>
                    <td>
                        <%#: Item.DateIssued %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
