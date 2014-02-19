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
        <asp:UpdatePanel ID ="ItemUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button runat="server" ID="AddToCartButton" OnClick="AddToCartButton_OnClick" Text="Add to cart" ValidationGroup="ItemCountGroup"/>
                <asp:Panel runat="server" ID ="ItemAddedPanel" Visible="False">
                    <asp:Label ID="LabelAdd" runat="server" Text="Item added to cart" AssociatedControlID="AddToCartButton" BackColor="#ffa100" CssClass="field-item-added" Width="200px"/>
                    <asp:Label ID="LabelStore" runat="server" CssClass="field-item-added" Width="200px" BackColor="#ffa100"><a href="/Store/Shop.aspx">Back to store</a> | <a href="/Store/Cart.aspx">To order</a></asp:Label>
                </asp:Panel>
                <asp:Panel runat="server" ID ="ErrorPanel" Visible="False">
                    <asp:Label ID="Label1" runat="server" Text="An error occured" AssociatedControlID="AddToCartButton" BackColor="#ff7711" CssClass="field-validation-error" Width="200px"/>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </article>
</asp:Content>