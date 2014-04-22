<%@ Page Title="Order Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderAdmin.aspx.cs" Inherits="WebStore.Administration.OrderAdmin" %>

<%@ Import Namespace="WebStore.Managers" %>

<%@ Register Assembly="WebStore" Namespace="WebStore.Controls" TagPrefix="ws" %>

<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function hideLabels() {
            $(".successlabel").hide();
        }
    </script>
    <ws:Nav runat="server" />
    <asp:Panel runat="server" ID="AdminPanel">
        <asp:UpdatePanel runat="server"
            UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ListView runat="server"
                    ID="OrderList"
                    DataKeyNames="ID"
                    ItemType="WebStore.App_Data.Model.Order"
                    SelectMethod="GetOrders">
                    <LayoutTemplate>
                        Show orders in state:
                        <asp:DropDownList runat="server"
                            ID="FilterList"
                            OnLoad="FillFilterList"
                            OnSelectedIndexChanged="OnFilterChange"
                            AutoPostBack="True" />
                        <span style="margin-left: 165px; font-size: 10px">
                            <br />
                            <asp:Button ID="SaveButton" runat="server"
                                Text="Save changes"
                                UseSubmitBehavior="False"
                                OnClick="SaveChanges"
                                OnClientClick="hideLabels" />
                        </span>
                        <asp:Label ID="ChangesSavedLabel" runat="server" Text="Changes has been saved!" class="successlabel" Visible="False" />
                        <br />
                        Page:
                        <asp:DataPager ID="Pager" runat="server" PagedControlID="OrderList" PageSize="10">
                            <Fields>
                                <asp:NumericPagerField />
                            </Fields>
                        </asp:DataPager>
                        <table class="bottomBorder">
                            <tr>
                                <td>
                                    <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="Login" Text="User login" />
                                </td>
                                <td>
                                    <b>Items</b>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="Total" Text="Total"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="StateName" Text="Order state" />
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="DateIssued" Text="Date issued" />
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="DateEnded" Text="Date delivered" />
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" OnClick="ApplyFilter" CommandName="Comment" Text="Comment" />
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="ItemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="vertical-align: top">
                            <td>
                                <%#: Item.User.Login %>
                            </td>
                            <td>
                                <%#: GetItemsForOrder(Item.ID) %>
                            </td>
                            <td>
                                $<%#: Item.Total %>
                            </td>
                            <td>
                                <ws:ListWithValue ID="StateDropDown" runat="server"
                                    SelectMethod="GetAllStates"
                                    ItemType="WebStore.App_Data.Model.OrderState"
                                    DataTextField="Name"
                                    DataValueField="ID"
                                    SelectedValue="<%# GetStateIDForOrder(Item.ID) %>"
                                    OnSelectedIndexChanged="OrderState_Index_Changed"
                                    onchange="hideLabels();"
                                    AutoPostBack="False"
                                    Value="<%# Item.ID %>"
                                    Enabled="<%# !OrderManager.IsStateFinal(Item.OrderState.ID) %>" />
                            </td>
                            <td>
                                <%#: Item.DateIssued == null ? string.Empty : Item.DateIssued.Value.ToString("d") %>
                            </td>
                            <td>
                                <%#: Item.DateEnded == null ? string.Empty : Item.DateEnded.Value.ToString("d") %>
                            </td>
                            <td>
                                <ws:ValueTextBox runat="server" Text="<%#: Item.Comment %>" Width="150"
                                    AutoPostBack="False" OnTextChanged="Comment_Changed" Value="<%#: Item.ID %>" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
