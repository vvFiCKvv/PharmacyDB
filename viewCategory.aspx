<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewCategory.aspx.cs" Inherits="PhamacyDB.viewCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="menu" class="menu" runat="server">
    </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="checkInteractionsScriptManager" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="/globalLibrary.js"></script>

    <script type="text/javascript">
        function postBackUpdateCategories() {

            __doPostBack("updateCategoriesPostBack", "");
        }
    </script>

    <div>
        <h1 id="h1CategoryName" runat="server">
            Category</h1>
        <div class="level0">
            <h2>
                Αναζήτηση</h2>
            <asp:TextBox ID="txtFilter" runat="server" Width="30%" onkeyup="javascript:postBackUpdateCategories();"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="10%" OnClick="btnSearch_Click" />
            <br />
            <h2>
                Αποτελέσματα</h2>
            <asp:UpdatePanel ID="updateCategory" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:HiddenField ID="updateCategoriesPostBack" runat="server" />
                    <div class="grindView">
                        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">
                            <Columns>
                                <asp:HyperLinkField HeaderText="Χημική Ονομασία" DataTextField="chemicalName">
                                    <ControlStyle Width="100%" />
                                </asp:HyperLinkField>
                            </Columns>
                        </asp:GridView>
                        <table cellspacing="0">
                            <tr>
                                <td align="left">
                                    <a id="btnPrev" runat="server">Prev</a>
                                </td>
                                <td align="center">
                                    <asp:Label ID="txtIndex" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <a id="btnNext" runat="server">Next</a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:AsyncPostBackTrigger ControlID="updateCategoriesPostBack" />
                </Triggers>
            </asp:UpdatePanel>
            <div id="footerMenu" class="footerMenu" runat="server">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
