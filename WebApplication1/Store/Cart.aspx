﻿<%@ Page Title="Your order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebStore.Cart.Cart" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>
    <asp:UpdatePanel ID="UpdPnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel runat="server" ID="LoggedInPanel" Visible="False">
                <p>
                    Here is your order details:
                </p>
                <asp:ListView runat="server" ID="CartList" DataKeyNames="ID" ItemType="WebStore.App_Data.Model.Item" SelectMethod="GetItems" OnDataBound="CartList_DataBound">
                    <ItemTemplate>

                        <div runat="server">
                            <img src="/Images/Items/Thumbs/<%#:Item.Image %>" alt="<%#:Item.Name %>"
                                width="100" height="75" style="border: solid; float: left; margin-right: 20px" />
                            <span>
                                <b><%#:Item.Name%></b>
                            </span>
                            <br />
                            <span>
                                <%#:GetTotalItemPrice(Item.ID)%>
                            </span>
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="Remove" Font-Size="7" OnClick="RemoveButton_Click" CommandName="remove" CommandArgument='<%# Item.ID %>' />
                            <p>&nbsp;</p>
                        </div>

                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <p>
                            You have no items in cart. Go to the <b><a href="../Store/Shop.aspx">store page</a></b> and get what you want!
                        </p>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <hr />
                        <p>&nbsp;</p>
                        <p runat="server" id="itemPlaceholder"/>
                        <hr />
                        <h2>
                            Total: 
                            <asp:PlaceHolder ID="TotalPricePlaceholder" runat="server">
                                <%= GetTotalItemPriceForAllItems() %>
                            </asp:PlaceHolder>
                        </h2>
                        Delivery address: <asp:TextBox runat="server" ID="AddressTextBox" />
                        <br/>
                        <asp:ListView runat="server"
                            ID="PaymentMethodList"
                            DataKeyNames="ID"
                            ItemType="WebStore.App_Data.Model.PaymentMethod"
                            SelectMethod="GetPaymentMethods">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ImageUrl="<%# ImagePath(Item.Image) %>" Width="200" AlternateText="<%#: Item.Name %>" BackColor="transparent" BorderStyle="None" OnClick="CompletePurchase" CommandName="<%#: Item.ID %>"/>
                            </ItemTemplate>
                        </asp:ListView>
                    </LayoutTemplate>
                </asp:ListView>
            </asp:Panel>
            <asp:Panel runat="server" ID="AnonymousPanel" Visible="False">
                <p>
                    Please <b><a href="../Account/Login.aspx">log in</a></b> or <b><a href="../Account/Register.aspx">register</a></b> to complete your purchase.
                </p>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>