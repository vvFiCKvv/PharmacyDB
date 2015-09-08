<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checkInteractions.aspx.cs"
    Inherits="PhamacyDB.checkInteractions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 50%;
        }
    </style>
</head>
<body>
    <div id="menu" class="menu" runat="server">
    </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="checkInteractionsScriptManager" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="/globalLibrary.js"></script>

    <script type="text/javascript">
        function postBackUpdatePharmacies() {

            __doPostBack("updatePharmaciesPostBack", "");
        }
        function postBackUpdateInteractions() {
            __doPostBack("updateInteractionsPostBack", "");
        }
        function postBackUpdateCheckInteractions() {
            __doPostBack("updateCheckInteractionsPostBack", "");
        }
        function updatePharmaciesIndex(index) {
            getPopupObject("hiddenPharmaciesIndex").value = index;
            postBackUpdatePharmacies();
        }
        function postBackUpdateData() {
            __doPostBack("hiddenData", "");
        }
        function updateData(data) {
            getPopupObject("hiddenData").value = data;
            postBackUpdateData();
        }
    </script>

    <asp:HiddenField ID="hiddenPharmaciesIndex" runat="server" Value="" />
    <asp:HiddenField ID="hiddenData" runat="server" Value="" />
    <div>
        <h1>
            Έλεγχος αλληλεπιδράσεων</h1>
        <div class="level0">
            <table width="100%">
                <tr>
                    <td class="style1" valign="top">
                        <h2>
                            Αναζήτηση</h2>
                        <asp:TextBox ID="txtFilter" runat="server" Width="68%" onkeyup="javascript:postBackUpdatePharmacies();"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="28%" OnClick="btnSearch_Click" />
                        <br />
                        <h2>
                            Αποτελέσματα</h2>
                        <asp:UpdatePanel ID="updatePharmacies" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:HiddenField ID="updatePharmaciesPostBack" runat="server" />
                                <div class="grindView">
                                    <asp:GridView ID="lstPharmacies" AutoGenerateColumns="False" runat="server" >
                                        <Columns>
                                            <asp:HyperLinkField HeaderText="Φαρμακευτικό Προϊόν" DataTextField="name">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Εταιρεία" DataTextField="company">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Μορφή" DataTextField="morph">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField Text="Add" />
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
                                <asp:AsyncPostBackTrigger ControlID="updatePharmaciesPostBack" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td valign="top">
                        <h2>
                            Αλληλεπιδράσεις</h2>
                        <asp:UpdatePanel ID="updateInteractions" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:HiddenField ID="updateInteractionsPostBack" runat="server" />
                                <div class="grindView">
                                    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server">
                                        <Columns>
                                            <asp:HyperLinkField HeaderText="Φαρμακευτικό Προϊόν" DataTextField="name">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Εταιρεία" DataTextField="company">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Μορφή" DataTextField="morph">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField Text="Remove" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="updateInteractionsPostBack" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
            </table>
        </div>
        <h1>
            Προσοχή</h1>
        <div class="level0">
            <asp:UpdatePanel ID="updateCheckInteractions" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false">
                <ContentTemplate>
                    <asp:HiddenField ID="updateCheckInteractionsPostBack" runat="server" />
                    <div id="interactioTable" runat="server">
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="updateCheckInteractionsPostBack" />
                </Triggers>
            </asp:UpdatePanel>

            
           
        <div id="footerMenu" class="footerMenu" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
