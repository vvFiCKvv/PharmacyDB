<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatePharmacy.aspx.cs"
    Inherits="PhamacyDB.updatePharmacy" %>

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
        <h1 id="h1PharmacyName" runat="server">
            Update Pharmacy</h1>
        <div class="level0">
            <table class="grindView">
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
                        <asp:TextBox ID="txtChemicalName" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Category:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCategory" runat="server" Enabled="false">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sub Category:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubCategory" runat="server" Enabled="false">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Greek Chemical name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtGreekname" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Indications:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputIndicia" runat="server" TextMode="MultiLine" Rows="3" Width="650"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Contra Indications:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputNonIndicia" runat="server" TextMode="MultiLine" Rows="3"
                            Width="650"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Undesirable reactions:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUndesirableReactions" runat="server" TextMode="MultiLine" Rows="3"
                            Width="650"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Interactions:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInteractions" runat="server" TextMode="MultiLine" Rows="3" Width="650"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Dose:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputDoce" runat="server" TextMode="MultiLine" Rows="3" Width="650"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Update" OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <h1>
            Update Commercial Pharmacy</h1>
        <div class="level0">
            <h2>
                Add Commercial</h2>
            <table>
                <tr>
                    <th>
                        Name
                    </th>
                    <th>
                        Company
                    </th>
                    <th>
                        Morph
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPharmacyName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPharmacyCompany" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPharmacyMorph" runat="server"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPharmacyCommercialAdd" runat="server" Text="Add" OnClick="btnPharmacyCommercialAdd_Click" />
                    </td>
                </tr>
            </table>
            <h2>
                Commercial List</h2>
            <div class="grindView">
                <asp:GridView ID="grdCommercials" AutoGenerateColumns="False" runat="server" 
                    onrowcommand="grdCommercials_RowCommand">
                    <Columns>
                        <asp:HyperLinkField HeaderText="Commercial Name" DataTextField="name">
                            <ControlStyle Width="100%" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField HeaderText="Company" DataTextField="company">
                            <ControlStyle Width="100%" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField HeaderText="Morph" DataTextField="morph">
                            <ControlStyle Width="100%" />
                        </asp:HyperLinkField>
                        <asp:ButtonField CommandName="deleteRow" Text="Remove" ButtonType="Link" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <h1>
            Update Interactions</h1>
        <div class="level0">
            <asp:Button ID="btnUpdateInteractions" runat="server" Text="Update Interactions"
                OnClick="btnUpdateInteractions_Click" />
            <div id="footerMenu" class="footerMenu" runat="server">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
