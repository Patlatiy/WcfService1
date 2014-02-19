<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebStore.Error.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <h1>Error! Panic!</h1>
        <p>
            Something really bad happened. But stay calm, we're working on it.
        </p>
        <p>
            <a href="/News/News.aspx">Go home</a>
        </p>
    </article>
</asp:Content>
