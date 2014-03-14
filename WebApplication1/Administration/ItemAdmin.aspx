<%@ Page Title="Item administration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemAdmin.aspx.cs" Inherits="WebStore.Administration.ItemAdmin" %>

<%@ Register TagPrefix="ws" TagName="Nav" Src="~/Controls/AdminNavigation.ascx" %>
<%@ Register Assembly="WebStore" Namespace="WebStore.Controls" TagPrefix="ws" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <ws:Nav runat="server" />
    <asp:DropDownList runat="server"
        ID="FileList"
        OnLoad="PopulateList"
        Visible="False" />
    <asp:Panel runat="server" ID="AdminPanel">
        <asp:UpdatePanel runat="server"
            UpdateMode="Always">
            <ContentTemplate>
                <asp:ListView runat="server"
                    ID="ItemList"
                    DataKeyNames="ID"
                    ItemType="WebStore.App_Data.Model.Item"
                    SelectMethod="GetItems">
                    <LayoutTemplate>
                        <table class="bottomBorder">
                            <tr>
                                <td style="border-bottom: transparent">
                                    <asp:Button runat="server" 
                                        Text="Save changes" 
                                        UseSubmitBehavior="False" 
                                        OnClick="SaveChanges"/>
                                    <asp:Label runat="server" 
                                        ID="ChangesSavedLabel1" 
                                        Visible="False" 
                                        Text="Changes saved!" 
                                        BackColor="yellow" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Item image</b>
                                </td>
                                <td>
                                    <b>Item name, description and category</b>
                                </td>
                                <td>
                                    <b>In store</b>
                                </td>
                                <td>
                                    <b>Price</b>
                                </td>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="ItemPlaceholder" />
                            <tr>
                                <td style="border-bottom: transparent">
                                    <asp:Button runat="server" 
                                        Text="Save changes" 
                                        UseSubmitBehavior="False" 
                                        OnClick="SaveChanges"/>
                                    <asp:Label runat="server" 
                                        ID="ChangesSavedLabel2" 
                                        Visible="False" 
                                        Text="Changes saved!" 
                                        BackColor="yellow"/>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style="vertical-align: top">
                            <td>
                                <ws:ListWithValue runat="server"
                                    Value="<%#: Item.ID %>"
                                    SelectedValue="<%# GetFileNameForItem(Item.ID) %>"
                                    OnLoad="CopyList"
                                    OnSelectedIndexChanged="ImageIndex_Changed"
                                    AutoPostBack="False"
                                    Width="200" />
                                <br />
                                <img src="/Images/Items/<%#: Item.Image %>" alt="Item image" />
                            </td>
                            <td>
                                Name:<br/>
                                <ws:ValueTextBox runat="server" 
                                    Text="<%#: Item.Name %>" 
                                    Width="150"
                                    AutoPostBack="False" 
                                    OnTextChanged="Name_Changed" 
                                    Value="<%#: Item.ID %>" />
                                <br/>
                                Description:<br/>
                                <ws:ValueTextBox runat="server" 
                                    Text="<%#: Item.Description %>" 
                                    Font-Size="9"
                                    AutoPostBack="False" 
                                    OnTextChanged="Description_Changed" 
                                    Value="<%#: Item.ID %>"
                                    TextMode="MultiLine" 
                                    Wrap="True" 
                                    Width="300"
                                    Height="30" />
                                <br/>
                                Category:<br/>
                                <ws:ListWithValue runat="server" 
                                    Value="<%#: Item.ID %>"
                                    OnPreRender="FillDropDownWithCategories"
                                    OnSelectedIndexChanged="Category_Changed" />
                            </td>
                            <td>x<ws:ValueTextBox runat="server" 
                                Text="<%#: Item.Quantity %>" 
                                Font-Size="9" 
                                Width="40"
                                AutoPostBack="False" 
                                OnTextChanged="Quantity_Changed" 
                                Value="<%#: Item.ID %>" />
                            </td>
                            <td>$<ws:ValueTextBox runat="server" 
                                Text="<%#: Item.Price %>" 
                                Font-Size="9" 
                                Width="50"
                                AutoPostBack="False" 
                                OnTextChanged="Price_Changed" 
                                Value="<%#: Item.ID %>" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
