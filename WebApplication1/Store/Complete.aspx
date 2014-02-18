<%@ Page Title="That is all" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Complete.aspx.cs" Inherits="WebStore.Store.Complete" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>
    <asp:Panel runat="server" ID="LoggedInPanel" Visible="False">
        <asp:Label runat="server" ID="ThankYouLabel" Text="Thank you for using our store."/>
        <br/>
        Purchase completed. Your order will be delivered soon!
    </asp:Panel>
    <asp:Panel runat="server" ID="AnonymousPanel" Visible="False">
        An error occured. Please <a href="/Store/Shop.aspx">try again</a>.
    </asp:Panel>
</asp:Content>
