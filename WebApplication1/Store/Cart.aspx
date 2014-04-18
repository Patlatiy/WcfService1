<%@ Page Title="Your order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebStore.Store.Cart" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
        <script type="text/javascript">
            function promptForCount() {
                var count = prompt("Please enter amount", "");
                if (count != null) {
                    var newCount = document.getElementById('<%= NewCount.ClientID %>');
                    newCount.value = count;
                    return true;
                }
                return false;
            }
            
        </script>
    </hgroup>
    <asp:HiddenField runat="server" ID="NewCount"/>
    <asp:UpdatePanel ID="UpdPnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

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
                        <asp:Button runat="server" ID="CountChanger" Text ="Change amount" Font-Size="7" OnClick="ChangeCount" CommandName="change" CommandArgument='<%# Item.ID %>' OnClientClick="return promptForCount()" />
                        <asp:Button runat="server" ID="PostnRemover" Text="Remove" Font-Size="7" OnClick="RemoveButton_Click" CommandName="remove" CommandArgument='<%# Item.ID %>' />
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
                    <p runat="server" id="itemPlaceholder" />
                    <hr />
                    <h2>Total: 
                            <asp:PlaceHolder ID="TotalPricePlaceholder" runat="server">
                                <%= GetTotalItemPriceForAllItems() %>
                            </asp:PlaceHolder>
                    </h2>
                </LayoutTemplate>
            </asp:ListView>
            <asp:Panel runat="server" ID="LoggedInPanel" Visible="False">
                <fieldset>
                    <br />
                    <asp:Label ID="Label1" runat="server" ToolTip="Required field">Delivery address: *</asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="AddressTextBox" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AddressTextBox"
                        CssClass="field-validation-error" ErrorMessage="Please enter delivery address."
                        ValidationGroup="Delivery" Display="Dynamic" />
                    <br />
                    <br />
                    Any additional information:
                            <br />
                    <asp:TextBox runat="server" ID="CommentTextBox" TextMode="MultiLine" />
                    <br />
                    <br />
                    Select payment method to complete purchase:<br />
                    <asp:ListView runat="server"
                        ID="PaymentMethodList"
                        DataKeyNames="ID"
                        ItemType="WebStore.App_Data.Model.PaymentMethod"
                        SelectMethod="GetPaymentMethods">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="<%# ImagePath(Item.Image) %>" Width="200" AlternateText="<%#: Item.Name %>" BackColor="transparent" BorderStyle="None" OnClick="CompletePurchase" CommandName="<%#: Item.ID %>" ValidationGroup="Delivery" />
                        </ItemTemplate>
                    </asp:ListView>
                </fieldset>
            </asp:Panel>
            <asp:Panel runat="server" ID="AnonymousPanel" Visible="False">
                <p>
                    Please <b><a href="../Account/Login.aspx">log in</a></b> or <b><a href="../Account/Register.aspx">register</a></b> to complete your purchase.
                </p>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
