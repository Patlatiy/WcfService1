<%@ Page Title="Too much items ordered" Language="C#" AutoEventWireup="true" CodeBehind="NoItems.aspx.cs" Inherits="WebStore.Store.NoItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sorry</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        We don't have so many items, please order fewer.<br/>
        <b><a href="/Store/Cart.aspx">Go back to order</a></b>
    </div>
    </form>
</body>
</html>
