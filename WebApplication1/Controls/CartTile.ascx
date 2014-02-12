<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartTile.ascx.cs" Inherits="WebStore.Controls.CartTile" %>
<style type="text/css">
    .auto-style1 {
        width: 58px;
        height: 52px;
    }
</style>
<div style="text-align: left">
    <p>
        <img alt="Cart" class="auto-style1" src="../Images/Cart.gif" style="float: left; margin-left: 57px" />
        <a id="A1" runat="server" href="~/Cart/Cart.aspx">Cart</a><br />
        Items in cart: <%: ItemsInOrder() %>
    </p>
</div>
<p>&nbsp;</p>

