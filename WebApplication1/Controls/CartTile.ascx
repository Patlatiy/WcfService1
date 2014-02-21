<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartTile.ascx.cs" Inherits="WebStore.Controls.CartTile" %>

<asp:UpdatePanel runat="server" ID="ItemCountPanel" UpdateMode="Conditional" RenderMode="Block">
    <ContentTemplate>
        <div style="height: 35px">
            <div style="display: block; width: 220px; float: right">
                <a id="A1" runat="server" href="~/Store/Cart.aspx">
                    <img alt="Cart" src="../Images/Cart.gif" style="float: left; margin-left: 57px; width: 58px; height: 52px" />
                    Cart
                </a>
                <br />
                Items in cart:
                <asp:Label runat="server" ID="ItemsInOrderLabel"><%: ItemsInOrder() %></asp:Label>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
&nbsp;

