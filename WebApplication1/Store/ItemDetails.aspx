<%@ Page Title="Item Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemDetails.aspx.cs" Inherits="WebStore.Store.ItemDetails" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>

    <article>
        <img src="/Images/Items/<%: GetImagePath() %>" style="float: left; margin-right: 20px" alt="Item image"/>
        <h3><%: GetItemName() %></h3>
        <%: GetItemDescription() %> <br/>
        In store: <%: GetItemInStore() %> <br/>
        <asp:TextBox runat="server" ID="ItemCount" Width="25" Text="1"></asp:TextBox>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button runat="server" ID="AddToCartButton" OnClick="AddToCartButton_OnClick" Text="Add to cart"/>
                <asp:Label ID="LabelAdd" runat="server" Text="Item added to cart" AssociatedControlID="AddToCartButton" CssClass="field-item-added" BackColor="yellow" Width="200px" Visible="False"/>
            </ContentTemplate>
        </asp:UpdatePanel>
    </article>
</asp:Content>