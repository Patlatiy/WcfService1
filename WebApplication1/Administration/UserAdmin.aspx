<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="WebStore.Administration.UserAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="AdminPanel" Visible="False">
        <asp:ListView runat="server"
            ID="UserList"
            DataKeyNames="ID"
            ItemType="WebStore.App_Data.Model.User"
            SelectMethod="GetUsers">
            <LayoutTemplate>
                <table>
                    <tr>
                        <td>
                            <b>Name</b>
                        </td>
                        <td>
                            <b>Role</b>
                        </td>
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
                        <asp:DropDownList ID="RoleDropDown" runat="server"
                            SelectMethod="GetAllRoles"
                            ItemType="WebStore.App_Data.Model.UserRole"
                            DataTextField="Name"
                            DataValueField="ID"
                            SelectedValue="<%# GetRoleIDForUser(Item.Login) %>"
                            OnSelectedIndexChanged="Index_Changed"
                            AutoPostBack="True"/>
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
</asp:Content>
