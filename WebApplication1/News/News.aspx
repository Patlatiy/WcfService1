<%@ Page Title="News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="WebStore.News.News" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
            </hgroup>
            <p>
                Here be news or something else</p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Hello and welcome to this humble site</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            There is not much to look at at the moment but we are working hard to make the best web store ever!
        </li>
        <li class="two">
            <h5>There will be store</h5>
            You can navigate the site by using links at the top of the page
        </li>
        <li class="three">
            <h5>There will be more!</h5>
            I am planning to add various features like admin page, a little chat or forum maybe. Stay tuned!
        </li>
    </ol>
</asp:Content>
