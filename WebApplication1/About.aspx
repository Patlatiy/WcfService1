<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication1.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>

    <article>
        <p>        
             This is my test task as a beginning developer in Akvelon company. It features a simple web store selling various things. It is written on ASP.NET.
        </p>
        <p>
            Thanks for visiting!
        </p>
    </article>

    <aside>
        <h3>By the way...</h3>
        <p>        
            You can visit other pages:
        </p>
        <ul>
            <li><a runat="server" href="~/">Home</a></li>
            <li><a id="A1" runat="server" href="~/Shop">Store</a></li>
            <li><a runat="server" href="~/About">About</a></li>
            <li style="display:none"><a runat="server" href="~/Contact">Contact</a></li>
        </ul>
    </aside>
</asp:Content>