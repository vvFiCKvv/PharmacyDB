<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createInteraction.aspx.cs" Inherits="PhamacyDB.createInteraction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
        <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="menu" class="menu" runat="server">
    </div>
    <form id="form1" runat="server">
    <div>
    <h1 id="h1PharmacyName" runat="server">
            Create Interaction</h1>
    <div class="level0">
            <table width="100%">
                <tr>
                    <td valign="top">
                    Chemical Name: 
                    </td>
                    <td valign="top">
                    <asp:TextBox ID="txtInterP1" runat="server"></asp:TextBox>
                    </td>
                 </tr>
                  <tr>
                    <td valign="top">
                        <asp:Label ID="txtInterP2Name" runat="server" Text="Chemical Name: "></asp:Label>
                    </td>
                    <td valign="top">
                    <asp:TextBox ID="txtInterP2" runat="server"></asp:TextBox>
                    </td>
                 </tr>
                  <tr>
                    <td valign="top">
                    Comment: 
                    </td>
                    <td valign="top">
                    <asp:TextBox ID="txtInterComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                 </tr>
                 <tr>
                    <td valign="top">
                    </td>
                    <td valign="top">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                            onclick="btnSubmit_Click" />                    </td>
                 </tr>
             </table>
    </div>
    
    
    <div id="footerMenu" class="footerMenu" runat="server">
    </div>
    </div>
    </form>
</body>
</html>
