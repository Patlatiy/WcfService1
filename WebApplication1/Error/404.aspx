<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="WebStore.Error._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Page not found</h1>
    <article>
        <p>
            Whoops. We couldn't find the page you're looking for.
        </p>
        <p>
            Try <a style="font-weight: bold" href="/News/News.aspx">home page</a>.
        </p>
    </article>
</asp:Content>
