<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartTile.ascx.cs" Inherits="WebStore.Controls.CartTile" %>
<style type="text/css">
    .auto-style1 {
        width: 58px;
        height: 52px;
    }
</style>
<div style="text-align: left">
    <p>
        
        <asp:UpdatePanel runat="server" ID="ItemCountPanel" UpdateMode="Conditional">
            <ContentTemplate>
                <a id="A1" runat="server" href="~/Store/Cart.aspx">
                    <img alt="Cart" class="auto-style1" src="../Images/Cart.gif" style="float: left; margin-left: 57px" />
                    Cart
                </a><br/>
                Items in cart: <asp:Label runat="server" ID="ItemsInOrderLabel"><%: ItemsInOrder() %></asp:Label>    
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
</div>
<p>&nbsp;</p>

