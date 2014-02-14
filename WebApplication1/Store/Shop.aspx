<%@ Page Title="Store" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="WebStore.Store.Shop" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:ListView ID="categoryList"  
        ItemType="WebStore.App_Data.Model.ItemCategory" 
        runat="server"
        SelectMethod="GetCategories" >
        <LayoutTemplate>
            <b style="font-size: medium; font-style: normal">
                <a href="Shop.aspx">All items</a> | <div id="itemPlaceholder" runat="server"></div>
            </b>
        </LayoutTemplate>
        <ItemTemplate>
            <b style="font-size: medium; font-style: normal">
                <a href="Shop.aspx?id=<%#: Item.ID %>">
                    <%#: Item.Name %>
                </a>
            </b>
        </ItemTemplate>
        <ItemSeparatorTemplate>  |  </ItemSeparatorTemplate>
    </asp:ListView>
    <asp:ListView ID="productList" runat="server" 
        DataKeyNames="ID" GroupItemCount="4"
        ItemType="WebStore.App_Data.Model.Item" SelectMethod="GetItems">
        <EmptyDataTemplate>
            <table >
                <tr>
                    <td>There are no items.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
            <td/>
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <td id="Td1" runat="server">
                <table>
                    <tr>
                        <td>
                            <a href="ItemDetails.aspx?id=<%#:Item.ID%>">
                                <img src="/Images/Items/Thumbs/<%#:Item.Image %>" alt="<%#:Item.Name %>"
                                    width="100" height="75" style="border: solid"/>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="ItemDetails.aspx?id=<%#:Item.ID%>">
                                <span>
                                    <%#:Item.Name%>
                                </span>
                            </a>
                            <br />
                            <span>
                                <b>Price: </b><%#:String.Format("{0:c}", Item.Price)%>
                            </span>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                </p>
            </td>
        </ItemTemplate>
        <LayoutTemplate>
            <table style="width:100%;">
                <tbody>
                    <tr>
                        <td>
                            <table id="groupPlaceholderContainer" runat="server" style="width:100%">
                                <tr id="groupPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr></tr>
                </tbody>
            </table>
        </LayoutTemplate>
    </asp:ListView>
</asp:Content>
