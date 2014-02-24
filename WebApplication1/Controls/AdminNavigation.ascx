<%@ Control Inherits="WebStore.Controls.AdminNavigation" Language="C#" AutoEventWireup="true" CodeBehind="AdminNavigation.ascx.cs"%>

    <nav>
        <ul style="float: left; margin-right: 10px; font-weight: bold">
            <li style="display: <%: UserAdminVisibility() %>"><a href="/Administration/UserAdmin.aspx">Edit users</a></li>
            <li style="display: <%: OrderAdminVisibility() %>"><a href="/Administration/OrderAdmin.aspx">Edit orders</a></li>
        </ul>
        <div style="width: 0px; height: 100px; float: left; border: 1px inset; margin-right: 10px"></div>
    </nav>