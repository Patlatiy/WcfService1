<%@ Page Title="Your order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebStore.Cart" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>

    <article>
        <p>        
             Your current order will be displayed here in nearest future.
        </p>

    </article>

    <aside>
        <h3>By the way...</h3>
        <p>        
            You can visit other pages:
        </p>
        <ul>
            <li><a runat="server" href="~/News/News.aspx">News</a></li>
            <li><a id="A1" runat="server" href="~/Store/Shop.aspx">Store</a></li>
            <li><a runat="server" href="~/About.aspx">About</a></li>
            <li style="display:none"><a runat="server" href="~/Contact">Contact</a></li>
        </ul>
    </aside>
</asp:Content>