<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createPharmacy.aspx.cs"
    Inherits="PhamacyDB.createPharmacy" %>

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
        function postBackUpdatePharmacies() {

            __doPostBack("updatePharmaciesPostBack", "");
        }
    </script>

    <div>
        <h1>
            Insert new Pharmacy</h1>
        <div class="level0">
            <table>
                <tr>
                    <th>
                        Property
                    </th>
                    <th>
                        Value
                    </th>
                </tr>
                <tr>
                    <td>
                        Chemical name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputChemicalName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Category:
                    </td>
                    <td>
                        <asp:DropDownList ID="txtInputCategory" runat="server" OnSelectedIndexChanged="txtInputCategory_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sub Category:
                    </td>
                    <td>
                        <asp:DropDownList ID="txtInputSubCategory" runat="server">
                        </asp:DropDownList>
                        <asp:RegularExpressionValidator ID="regularValidator" runat="server" EnableClientScript="false"
                            ControlToValidate="txtInputSubCategory">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Create" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
            <h1>
                Edit Pharmacy</h1>
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
                            CssClass="fullWidth" OnRowCommand="grdPharmacyChemical_RowCommand">
                            <Columns>
                                <asp:HyperLinkField HeaderText="Chemical Name" DataTextField="chemicalName">
                                    <ControlStyle Width="100%" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField HeaderText="Category" DataTextField="category">
                                    <ControlStyle Width="100%" />
                                </asp:HyperLinkField>
                                <asp:ButtonField CommandName="deleteRow" Text="Remove" ButtonType="Link" />
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
        </div>
        <div id="footerMenu" class="footerMenu" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
