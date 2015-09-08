<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="PhamacyDB.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function postBackupdatePanel1() {
        
             __doPostBack("updatePanel1PostBack", "");

    }
    </script>
    <asp:TextBox ID="TextBox1" runat="server" onkeyup="javascript:postBackupdatePanel1();"></asp:TextBox>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
    <br />
    <asp:HiddenField ID="updatePanel1PostBack" runat="server" />
    <div ID="Div2" runat="server"></div>
    <br />
    </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="Button1" />
    <asp:AsyncPostBackTrigger ControlID="updatePanel1PostBack" />
    </Triggers>
    </asp:UpdatePanel>

    <br />
    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    
    <br />
    
    <asp:Button ID="Button1" runat="server" Text="Button" 
        onclick="Button1_Click1" />
    </form>
</body>
</html>
