<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="viewCommercialPharmacy.aspx.cs" Inherits="PhamacyDB.viewCommercialPharmacy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <div id="menu" class="menu" runat="server"></div>
    <form id="form1" runat="server">
    <div>
   <h1 id="h1PharmacyName" runat="server">
            Φαρμακευτικό Προϊόν</h1>
           <div class="level0">
        <h2>
            Ιδιότητες</h2>
            <div class="grindView">
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
                    Ονομασία:
                </td>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Εταιρεία:
                </td>
                <td>
                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Μορφή:
                </td>
                <td>
                    <asp:Label ID="lblMorp" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Χημική Ονομασία:
                </td>
                <td>
                    <asp:Label ID="lblChemicalName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Ελληνική Χημική Ονομασία:
                </td>
                <td>
                    <asp:Label ID="lblGreekChemicalName" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    Κατηγορία:
                </td>
                <td>
                    <a ID="lblCategory" runat="server" onclick="lblCategory_Click"></a>
                </td>
            </tr>
            <tr>
                <td>
                    Υποκατηγορία:
                </td>
                <td>
                    <a ID="lblSubCategory" runat="server" onclick="lblSubCategory_Click"></a>
                </td>
            </tr>
           
        </table>
        </div>
        <h2>
            Οδηγίες</h2>
            <div class="grindView">
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
                    Ενδείξεις:
                </td>
                <td>
                    <asp:Label ID="lblIndication" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Αντενδείξεις:
                </td>
                <td>
                    <asp:Label ID="lblContraIndication" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Ανεπιθύμητες Ενέργειες:
                </td>
                <td>
                    <asp:Label ID="lblUndesirableReaction" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Αλληλεπιδράσεις:
                </td>
                <td>
                    <asp:Label ID="lblInteractions" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Δοσολογία - Προσοχή στην χορήγηση:
                </td>
                <td>
                    <asp:Label ID="lblDoce" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <h2>
            Αλληλεπιδράσεις</h2>
      <div class="grindView">
       <asp:GridView ID="grdInteractions" AutoGenerateColumns="False" runat="server" >
            <Columns>
                <asp:HyperLinkField HeaderText="Ονομασία" DataTextField="name"  >
                    <ControlStyle Width="100%" />
                </asp:HyperLinkField>
                <asp:HyperLinkField HeaderText="Εταιρεία" DataTextField="company">
                    <ControlStyle Width="100%" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="Μορφή" DataTextField="Morph">
                    <ControlStyle Width="100%" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="Χημική Ονομασία" DataTextField="chemicalName">
                    <ControlStyle Width="100%" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="Επισημάνσεις" DataTextField="comment">
                    <ControlStyle Width="100%" />
                    </asp:HyperLinkField>
               
            </Columns>
        </asp:GridView>
        <table cellspacing="0">
            <tr>
                <td align="left">
                    <a ID="btnInteractionsPrev" runat="server">Prev</a>
                </td>
                <td align="center">
                <asp:Label ID="txtInteractionsIndex" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <a ID="btnInteractionsNext" runat="server">Next</a>
                </td>
            </tr>
        </table>
        </div>
            </div>
    <div id="footerMenu" class="footerMenu" runat="server"></div> 

    </div>
    </form>
</body>
</html>
