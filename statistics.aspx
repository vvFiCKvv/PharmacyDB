<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="statistics.aspx.cs" Inherits="PhamacyDB.statistics" %>

<%@ Register Assembly="chartDataLayer" Namespace="chartDataLayer" TagPrefix="cc1" %>
<%@ Register Assembly="netchartdir" Namespace="ChartDirector" TagPrefix="chart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <meta name="description" content="Pharmacy DB.">
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="menu" class="menu" runat="server">
    </div>
    <form id="form1" runat="server">
    <div>
        <h1>
            Κατηγορίες</h1>
        <div class="level0">
        <div id="taible1" class="grindView" runat="server"> </div> 
        </div>
        <h1>
            Γραφήματα</h1>
        <center>
            <div class="level0">
                <h2>
                    Διάγραμμα υποκατιγοριών ανα κατηγορία
                </h2>
                <br />
                <cc1:WebChartControl ID="chartCategorySubCategory" runat="server"></cc1:WebChartControl>
                <h2>
                    Διάγραμμα Φαρμάκων ανα κατηγορία
                </h2>
                <br />
                <cc1:WebChartControl ID="chartCategoryPharmacy" runat="server"></cc1:WebChartControl>
                <h2>
                    Διάγραμμα αλληλεπιδράσεων φαρμάκων ανα κατηγορία
                </h2>
                <br />
                <cc1:WebChartControl ID="chartCategoryInteractios" runat="server"></cc1:WebChartControl>
                <h2>
                    Διάγραμμα Φαρμακευτικών προϊόντων ανα κατηγορία
                </h2>
                <br />
                <cc1:WebChartControl ID="chartCategoryPharmacyCommercial" runat="server"></cc1:WebChartControl>
                <h2>
                    Διάγραμμα αλληλεπιδράσεων Φαρμακευτικών προϊόντων ανα κατηγορία
                </h2>
                <br />
                <cc1:WebChartControl ID="chartCategoryInteractiosCommercial" runat="server"></cc1:WebChartControl>
            </div>
        </center>
        <div id="footerMenu" class="footerMenu" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
