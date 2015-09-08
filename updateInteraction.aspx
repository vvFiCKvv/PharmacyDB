<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateInteraction.aspx.cs"
    Inherits="PhamacyDB.updateInteraction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        function postBackUpdatePharmacies() {

            __doPostBack("updatePharmaciesPostBack", "");
        }
        function postBackUpdateCategories() {

            __doPostBack("updateCategoriesPostBack", "");
        }
    </script>

    <div>
        <h1 id="h1PharmacyName" runat="server">
            Update Interactions</h1>
        <div class="level0">
            <table width="100%">
                <tr>
                    <td valign="top">
                        <h2>
                            Pharmacy Search</h2>
                        <asp:TextBox ID="txtPharmacyChemicalFilter" runat="server" Width="68%" onkeyup="javascript:postBackUpdatePharmacies();"></asp:TextBox>
                        <asp:Button ID="btnPharmacyChemicalSearch" runat="server" Text="Search" Width="28%"
                            OnClick="btnPharmacyChemicalSearch_Click" />
                        <br />
                        <h2>
                            Pharmacy Results</h2>
                        <asp:UpdatePanel ID="updatePharmacies" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:HiddenField ID="updatePharmaciesPostBack" runat="server" />
                                <div class="grindView">
                                    <asp:GridView ID="grdPharmacyChemical" AutoGenerateColumns="False" runat="server"
                                        CssClass="fullWidth" OnRowCommand="grdInteractions_RowCommand">
                                        <Columns>
                                            <asp:HyperLinkField HeaderText="Chemical Name" DataTextField="chemicalName">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Category" DataTextField="category">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:ButtonField CommandName="addRow" Text="Add" ButtonType="Link" />
                                        </Columns>
                                    </asp:GridView>
                                    <table cellspacing="0" class="fullWidth">
                                        <tr>
                                            <td align="left">
                                                <a id="btnPharmacyChemicalPrev" runat="server">Prev</a>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="txtPharmacyChemicalIndex" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <a id="btnPharmacyChemicalNext" runat="server">Next</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnPharmacyChemicalSearch" />
                                <asp:AsyncPostBackTrigger ControlID="updatePharmaciesPostBack" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td valign="top">
                        <h2>
                            Sub Category Search</h2>
                        <asp:TextBox ID="txtSubCategoryFilter" runat="server" Width="68%" onkeyup="javascript:postBackUpdateCategories();"></asp:TextBox>
                        <asp:Button ID="btnSubCategorySearch" runat="server" Text="Search" Width="28%" OnClick="btnSubCategorySearch_Click" />
                        <br />
                        <h2>
                            Sub Category Results</h2>
                        <asp:UpdatePanel ID="updateCategories" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:HiddenField ID="updateCategoriesPostBack" runat="server" />
                                <div class="grindView">
                                    <asp:GridView ID="grdSubCategory" AutoGenerateColumns="False" runat="server" CssClass="fullWidth"
                                        OnRowCommand="grdInteractionsSubCategory_RowCommand">
                                        <Columns>
                                            <asp:HyperLinkField HeaderText="Sub Category Name" DataTextField="subCategoryName">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Category" DataTextField="category">
                                                <ControlStyle Width="100%" />
                                            </asp:HyperLinkField>
                                            <asp:ButtonField CommandName="addRow" Text="Add" ButtonType="Link" />
                                        </Columns>
                                    </asp:GridView>
                                    <table cellspacing="0" class="fullWidth">
                                        <tr>
                                            <td align="left">
                                                <a id="btnSubCategoryPrev" runat="server">Prev</a>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="txtSubCategoryIndex" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <a id="btnSubCategoryNext" runat="server">Next</a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubCategorySearch" />
                                <asp:AsyncPostBackTrigger ControlID="updateCategoriesPostBack" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <h2>
                            Interactions with Pharmacy</h2>
                        <div class="grindView">
                            <asp:GridView ID="grdInteractions" AutoGenerateColumns="False" runat="server" CssClass="fullWidth"
                                OnRowCommand="grdInteractions_RowCommand">
                                <Columns>
                                    <asp:HyperLinkField HeaderText="Chemical Name" DataTextField="chemicalName">
                                        <ControlStyle Width="100%" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField HeaderText="Comment" DataTextField="comment">
                                        <ControlStyle Width="100%" />
                                    </asp:HyperLinkField>
                                    <asp:ButtonField CommandName="deleteRow" Text="Remove" ButtonType="Link" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                    <td valign="top">
                        <h2>
                            Interactions with Sub Category</h2>
                        <div class="grindView">
                            <asp:GridView ID="grdInteractionsSubCategory" AutoGenerateColumns="False" runat="server"
                                CssClass="fullWidth" OnRowCommand="grdInteractionsSubCategory_RowCommand">
                                <Columns>
                                    <asp:HyperLinkField HeaderText="Sub Category Name" DataTextField="subCategoryName">
                                        <ControlStyle Width="100%" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField HeaderText="Comment" DataTextField="comment">
                                        <ControlStyle Width="100%" />
                                    </asp:HyperLinkField>
                                    <asp:ButtonField CommandName="deleteRow" Text="Remove" ButtonType="Link" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="footerMenu" class="footerMenu" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
