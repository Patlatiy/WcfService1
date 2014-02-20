<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebStore.About" %>

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
        <p>
            Your current role is <b><%: UserRole() %></b>
            <br/>
            <asp:Button ID="Button2" runat="server" Text="Make me user!" OnClick="MakeMeUser" BackColor="orange"/>
            <asp:Button runat="server" Text="Make me admin!" OnClick="MakeMeAdmin" BackColor="orange"/>
            <br/>
            <asp:Button ID="Button1" runat="server" OnClick="TestMethod" Text="Break the whole damned thing" BackColor="red" ForeColor="yellow"/>
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