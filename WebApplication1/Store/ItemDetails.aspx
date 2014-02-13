<%@ Page Title="Item Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemDetails.aspx.cs" Inherits="WebStore.Store.ItemDetails" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>

    <article>
        <img src="/Images/Items/<%: GetImagePath() %>" style="float: left; margin-right: 20px"/>
        <h3><%: GetItemName() %></h3>
        <%: GetItemDescription() %> <br/>
        In store: <%: GetItemInStore() %>
        <asp:Button runat="server" ID="AddToCartButton" OnClick="AddToCartButton_OnClick"/>
    </article>
</asp:Content>