﻿<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="WebStore.Administration.UserAdmin" %>

<%@ Register Assembly="WebStore" Namespace="WebStore.Controls" TagPrefix="ws" %>
<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ws:Nav runat="server" />
    <asp:Panel runat="server" ID="AdminPanel" Visible="False">
        <asp:ListView runat="server"
            ID="UserList"
            DataKeyNames="ID"
            ItemType="WebStore.App_Data.Model.User"
            SelectMethod="GetUsers">
            <LayoutTemplate>
                <table class="bottomBorder">
                    <tr>
                        <td>
                            <b>Name</b>
                        </td>
                        <td>
                            <b>Login</b>
                        </td>
                        <td>
                            <b>Role</b>
                        </td>
                        <td></td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="ItemPlaceholder" />
                </table>

            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#: Item.Name %>
                    </td>
                    <td>
                        <%#: Item.Login %>
                    </td>
                    <td>
                        <ws:ListWithValue ID="RoleDropDown" runat="server"
                            SelectMethod="GetAllRoles" ItemType="WebStore.App_Data.Model.UserRole" DataTextField="Name" DataValueField="ID"
                            SelectedValue="<%# GetRoleIDForUser(Item.Login) %>" OnSelectedIndexChanged="Index_Changed" OnPreRender="IsItMe"
                            AutoPostBack="True" Value="<%#: Item.Login %>" />
                    </td>
                    <td>
                        <asp:Button runat="server" OnPreRender="CorrectButtonName" OnClick="BlockUser_Clicked"
                            CommandName="<%# Item.IsBlocked.ToString() %>" CommandArgument="<%# Item.Login %>"
                            Text="<%# Item.IsBlocked.ToString() %>" Font-Size="7" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
