<%@ Page Title="Your order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebStore.Cart.Cart" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>
    <asp:UpdatePanel ID="UpdPnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LoginView runat="server" ID="CartLoginView">
                <LoggedInTemplate>
                    <p>
                        Here is your order details:
                    </p>

                    <asp:ListView runat="server"
                        ID="CartList"
                        DataKeyNames="ID"
                        ItemType="WebStore.App_Data.Model.Item"
                        SelectMethod="GetItems">
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
                                <asp:Button ID="Button1" runat="server" Text="Remove" Font-Size="7" OnClick="RemoveButton_Click" CommandName="remove" CommandArgument='<%# Item.ID %>'/>
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
                            <p runat="server" id="itemPlaceholder"></p>
                            <hr />
                            <h2>Total: 
                    <asp:PlaceHolder ID="TotalPricePlaceholder" runat="server">
                        <%= GetTotalItemPriceForAllItems() %>
                    </asp:PlaceHolder>
                            </h2>
                        </LayoutTemplate>
                    </asp:ListView>
                </LoggedInTemplate>
                <AnonymousTemplate>
                    <p>
                        Please <b><a href="../Account/Login.aspx">log in</a></b> or <b><a href="../Account/Register.aspx">register</a></b> to complete your purchase.
                    </p>
                </AnonymousTemplate>
            </asp:LoginView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
