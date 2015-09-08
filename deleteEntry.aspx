<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deleteEntry.aspx.cs" Inherits="PhamacyDB.deleteEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="menu" class="menu" runat="server">
    </div>
    <form id="form1" runat="server">
    <div>
        <h1 id="h1Title" runat="server">
            Delete</h1>
        Are You Sure?
        <br />
        <asp:Button ID="btnDelete" runat="server" Text="Yes" 
            onclick="btnDelete_Click" />
        <div id="footerMenu" class="footerMenu" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
