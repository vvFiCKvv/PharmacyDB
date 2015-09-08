<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createCategory.aspx.cs"
    Inherits="PhamacyDB.createCategory" %>

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
    <div>
    <h1> Insert (Sub)Categories</h1>
     <table width="100%">
                <tr>
                    <td valign="top">
        <h2>
            new Category</h2>
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
                        Category name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtInputName" runat="server"></asp:TextBox>
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
        </div>
        </td  valign="top">
        <td>
        <h2>
            new Sub Category</h2>
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
                        Category name:
                    </td>
                    <td>
                        <asp:DropDownList ID="lstCategory" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Sub Category name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubCategoryName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSubCategorySubmit" runat="server" Text="Create" OnClick="btnSubCategorySubmit_Click" />
                    </td>
                </tr>
            </table>
                    </td>
        </tr>
        </table>
        <h1>Delete (Sub)Categories</h1>
     <table width="100%">
         <tr>
             <td valign="top">
                <h2>
                    Delete Category</h2>
                    <div class="level0">
            <h2>
                        Search</h2>
                        
                    <asp:TextBox ID="txtCategoryFilter" runat="server" Width="70%" 
                        ontextchanged="btnCategorySearch_Click" AutoPostBack="true" ></asp:TextBox>
                    <asp:Button ID="btnCategorySearch" runat="server" Text="Search" Width="25%" 
                        onclick="btnCategorySearch_Click"  AutoPostBack="true" />
            <h2>Categories Results</h2>
            <div class="grindView">
        <asp:GridView ID="grdCategory" AutoGenerateColumns="False" runat="server" 
                    onrowcommand="grdCategory_RowCommand">
        
            <Columns>
                <asp:HyperLinkField HeaderText="Category Name" DataTextField="category">
                    <ControlStyle Width="100%" />
                </asp:HyperLinkField>
                <asp:ButtonField CommandName="deleteRow" Text="Remove" ButtonType="Link" />
            </Columns>
        </asp:GridView>
        <table cellspacing="0">
            <tr>
                <td align="left">
                    <a ID="btnCategoryPrev" runat="server">Prev</a>
                </td>
                <td align="center">
                <asp:Label ID="txtCategoryIndex" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <a ID="btnCategoryNext" runat="server">Next</a>
                </td>
            </tr>
        </table>
        </div>
        </div>
             </td>
             <td>
                <h2>
                    Delete Sub Category</h2>
                    <div class="level0">
            <h2>
                        Search</h2>
                        
                    <asp:TextBox ID="txtSubCategoryFilter" runat="server" Width="70%" 
                        ontextchanged="btnSubCategorySearch_Click"  AutoPostBack="true" ></asp:TextBox>
                    <asp:Button ID="btnSubCategorySearch" runat="server" Text="Search" Width="25%" 
                        onclick="btnSubCategorySearch_Click"  AutoPostBack="true"/>
            <h2>Sub Categories Results</h2>
            <div class="grindView">
        <asp:GridView ID="grdSubCategory" AutoGenerateColumns="False" runat="server" 
                    onrowcommand="grdSubCategory_RowCommand">
        
            <Columns>
                <asp:HyperLinkField HeaderText="Sub Category Name" DataTextField="subCategory"  >
                    <ControlStyle Width="100%" />
                </asp:HyperLinkField>
                <asp:HyperLinkField HeaderText="Category Name" DataTextField="category">
                    <ControlStyle Width="100%" />
                </asp:HyperLinkField>
                <asp:ButtonField CommandName="deleteRow" Text="Remove" ButtonType="Link" />

               
            </Columns>
        </asp:GridView>
        <table cellspacing="0">
            <tr>
                <td align="left">
                    <a ID="btnSubCategoryPrev" runat="server">Prev</a>
                </td>
                <td align="center">
                <asp:Label ID="txtSubCategoryIndex" runat="server"></asp:Label>
                </td>
                <td align="right">
                    <a ID="btnSubCategoryNext" runat="server">Next</a>
                </td>
            </tr>
        </table>
        </div>
        </div>
             </td>
         </tr>
     </table>
            <div id="footerMenu" class="footerMenu" runat="server">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
