using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChartDirector;
using chartDataLayer;


namespace PhamacyDB
{
    public partial class statistics : System.Web.UI.Page
    {
        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }
        protected internal void drawChartPie1(WebChartControl viewer, String title)
        {


            PieChart c = new PieChart(900, 380);


            // Set the center of the pie at (300, 140) and the radius to 120 pixels
            c.setPieSize(450, 140, 160);

            c.set3D(20);

            c.addTitle(title);

            // Set label format to display sector label, value and percentage in two lines
            c.setLabelFormat("<*block,width=200*> {label}<*br*>{value|0} ({percent}%)");
            c.setLabelLayout(Chart.SideLayout);


            // Set label style to 10 pts Arial Bold Italic font. Set background color to the
            // same as the sector color, with reduced-glare glass effect and rounded corners.
            ChartDirector.TextBox t = c.setLabelStyle("Arial Bold Italic", 10);
            t.setBackground(Chart.SameAsMainColor, Chart.Transparent, Chart.glassEffect(
                Chart.ReducedGlare));
            t.setRoundedCorners();


            // Use side label layout method
            c.setLabelLayout(Chart.SideLayout);

            String[] labels = new String[viewer.Items.Length];
            double[] data = new double[viewer.Items.Length];
            // Set the pie data and the pie labels
            int i = 0;
            double sum =0;
            foreach (ChartDataLayer item in viewer.Items)
            {
                item.subDataSetSize(item.Count);
                item.subDataGetTable();
                data[i] = item.subDataAverage;
                sum+= item.subDataAverage;
                labels[i++] = item.Text;
            }


            c.setData(data, labels);

            c.addText(700, 320, "Σύνολο:" + sum, "Arial Bold Italic", 12);

            c.setTransparentColor(0xffffff);
            viewer.Image = c.makeWebImage(Chart.PNG);

            viewer.ImageMap = (c.getHTMLImageMap("#", "{label}", "title='{label}: {value|0}({percent}%)'")).Replace("href=\"#?", "href=\"#");

            

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            loadMenus();

            createTable();

            chartCategoryPharmacyCreate();
            chartCategorySubCategoryCreate();

            chartCategoryInteractiosCreate();
            chartCategoryPharmacyCommercialCreate();
            chartCategoryInteractiosCommercialCreate();

        }

        private void createTable()
        {
            taible1.InnerHtml += "";
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            foreach (Category categ in db.Categories)
            {
                int cnt = 0;
                var query = (from subc in db.SubCategories
                             where subc.categoryName == categ.categoryName
                             select subc);
                cnt = query.Count();
                if (cnt <= 0)
                {
                    continue;
                }
                taible1.InnerHtml += "<h2 id=\"" + categ.categoryName.Trim() + "\">" + categ.categoryName.Trim() + "</h2>";

                taible1.InnerHtml += "<table><tr><th>Υποκατιγορία</th><th>Φάρμακα</th><th>Αλληλεπιδράσεις Φαρμάκων</th><th>Φαρμακευτικά Προϊόντα</th><th>Αλληλεπιδράσεις φαρμακευτικών προϊόντων</th></tr>";
                int i = 0;
                int subCategoryPharmaciesCnt = 0;
                int subCategoryPharmacyInteractionsCnt = 0;
                int subCategoryPharmacyComercialsCnt = 0;
                int subCategoryPharmacyComercialInteractionsCnt = 0;
                foreach (SubCategory subcat in query)
                {
                    if (i++ % 2 == 0)
                    {
                        taible1.InnerHtml += "<tr>";
                    }
                    else
                    {
                        taible1.InnerHtml += "<tr class=\"alt1\">";
                    }
                    int currResult;
                    taible1.InnerHtml += "<td>" + subcat.subCategoryName + "</td>";
                    currResult = countSubCategoryPharmacies(db, subcat);
                    subCategoryPharmaciesCnt += currResult;
                    taible1.InnerHtml += "<td>" + currResult + "</td>";
                    currResult = countSubCategoryPharmacyInteractions(db, subcat);
                    subCategoryPharmacyInteractionsCnt += currResult;
                    taible1.InnerHtml += "<td>" + currResult + "</td>";
                    currResult = countSubCategoryPharmacyComercials(db, subcat);
                    subCategoryPharmacyComercialsCnt += currResult;
                    taible1.InnerHtml += "<td>" + currResult + "</td>";
                    currResult = countSubCategoryPharmacyComercialInteractions(db, subcat);
                    subCategoryPharmacyComercialInteractionsCnt += currResult;
                    taible1.InnerHtml += "<td>" + currResult + "</td>";
                    taible1.InnerHtml += "</tr>";


                }
                taible1.InnerHtml += "<tr><th  class=\"alt1\">Σύνολο</th><th class=\"alt1\">" + subCategoryPharmaciesCnt +
                    "</th><th  class=\"alt1\">" + subCategoryPharmacyInteractionsCnt +
                    "</th><th  class=\"alt1\">" + subCategoryPharmacyComercialsCnt +
                    "</th><th  class=\"alt1\">" + subCategoryPharmacyComercialInteractionsCnt + "</th></tr>";
                taible1.InnerHtml += "</table>";

                /*enumarate Chart*/
                ChartDataLayer data;
                data = chartCategoryInteractiosCommercial.add(categ.categoryName.Trim());
                data.update(subCategoryPharmacyComercialInteractionsCnt);

            }

        }
        private static int countSubCategoryPharmacyInteractions(DatabaseDataContext db, SubCategory subCat)
        {

            int interCnt = 0;
            var query = (from inter in db.InteractionPharmacyPharmacies
                         where inter.PharmacyChemical1.SubCategory.subCategoryName == subCat.subCategoryName
                         select inter.PharmacyChemical);
            interCnt += query.Count();
            var query0 = (from inter in db.InteractionPharmacyPharmacies
                          where inter.PharmacyChemical.SubCategory.subCategoryName == subCat.subCategoryName
                          select inter.PharmacyChemical1);
            interCnt += query0.Count();
            var query1 = (from inter in db.InteractionPharmacySubCategories
                          where inter.PharmacyChemical.SubCategory.subCategoryName == subCat.subCategoryName
                          select inter.subCategoryName);
            foreach (String subCatQ2 in query1)
            {
                var query3 = from phar in db.PharmacyChemicals
                             where phar.subCategoryName == subCatQ2
                             select phar;
                interCnt += query3.Count();

            }
            var query2 = (from inter in db.InteractionPharmacySubCategories
                          where inter.SubCategory.subCategoryName == subCat.subCategoryName
                          select inter);
            interCnt += query2.Count();
            return interCnt;
        }
        private static int countSubCategoryPharmacyComercialInteractions(DatabaseDataContext db, SubCategory subCat)
        {
            int interCnt = 0;
            var query = (from phar in db.PharmacyCommercials
                         where phar.PharmacyChemical.SubCategory.subCategoryName == subCat.subCategoryName
                         select phar);
            foreach (PharmacyCommercial phar in query)
            {
                String pharCname = phar.chemicalName; 
                String pharSubCatName =(from chem in db.PharmacyChemicals
                                        where chem.chemicalName == pharCname
                            select chem.subCategoryName).First();

                var query1 = (from inter in db.InteractionPharmacyPharmacies
                              where inter.pharmacyname1 == pharCname
                              select inter.PharmacyChemical1);
                if (query1.Count() > 0)
                {
                    foreach (PharmacyChemical chem in query1)
                    {
                        interCnt += countPharmacyComercials(db, chem);
                    }
                }

                var query2 = (from inter in db.InteractionPharmacyPharmacies
                              where inter.pharmacyname2 == pharCname
                              select inter.PharmacyChemical);
                if (query2.Count() > 0)
                {
                    foreach (PharmacyChemical chem in query2)
                    {
                        interCnt += countPharmacyComercials(db, chem);
                    }
                }

                var query3 = (from inter in db.InteractionPharmacySubCategories
                              where inter.chemicalName == pharCname
                              select inter.SubCategory);
                if (query3.Count() > 0)
                {
                    foreach (SubCategory subcat in query3)
                    {
                        interCnt += countSubCategoryPharmacyComercials(db, subcat);
                    }
                }

                var query4 = (from inter in db.InteractionPharmacySubCategories
                              where inter.subCategoryName == pharSubCatName
                              select inter.PharmacyChemical);
                if (query4.Count()>0)
                {
                    foreach (PharmacyChemical chem in query4)
                    {
                        interCnt += countPharmacyComercials(db, chem);
                    }
                }





            }

            return interCnt;
        }
        private static int countPharmacyComercials(DatabaseDataContext db, PharmacyChemical chem1)
        {
            int cnt = 0;
            var query = (from phar in db.PharmacyCommercials
                         where phar.chemicalName == chem1.chemicalName
                         select phar);
            cnt = query.Count();
            return cnt;
        }

        private static int countSubCategoryPharmacies(DatabaseDataContext db, SubCategory subCat)
        {
            int cnt = 0;
            var query = (from phar in db.PharmacyChemicals
                         where phar.subCategoryName == subCat.subCategoryName
                         select phar);
            cnt = query.Count();
            return cnt;
        }
        private static int countSubCategoryPharmacyComercials(DatabaseDataContext db, SubCategory subCat)
        {
            int cnt = 0;
            var query = (from phar in db.PharmacyCommercials
                         where phar.PharmacyChemical.subCategoryName == subCat.subCategoryName
                         select phar);
            cnt = query.Count();
            return cnt;
        }
        private void chartCategoryPharmacyCreate()
        {
            chartCategoryPharmacy.layerType = ChartDataLayer.ChartlayerType.Pie;
            ChartDataLayer data;

            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            foreach (Category categ in db.Categories)
            {
                int cnt = countCategoryPharmacies(db, categ);
                if (cnt > 0)
                {
                    data = chartCategoryPharmacy.add(categ.categoryName.Trim());
                    data.update(cnt);
                }


            }
            drawChartPie1(chartCategoryPharmacy, "");

        }

        private static int countCategoryPharmacies(DatabaseDataContext db, Category categ)
        {
            int cnt = 0;
            var query = (from phar in db.PharmacyChemicals
                         where phar.SubCategory.categoryName == categ.categoryName
                         select phar);
            cnt = query.Count();
            return cnt;
        }
        private void chartCategorySubCategoryCreate()
        {
            chartCategorySubCategory.layerType = ChartDataLayer.ChartlayerType.Pie;
            ChartDataLayer data;

            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            foreach (Category categ in db.Categories)
            {
                int cnt = countCategorySubcategories(db, categ);
                if (cnt > 0)
                {
                    data = chartCategorySubCategory.add(categ.categoryName.Trim());
                    data.update(cnt);
                }


            }
            drawChartPie1(chartCategorySubCategory, "");

        }

        private static int countCategorySubcategories(DatabaseDataContext db, Category categ)
        {
            int cnt = 0;
            var query = (from subc in db.SubCategories
                         where subc.categoryName == categ.categoryName
                         select subc);
            cnt = query.Count();
            return cnt;
        }

        private void chartCategoryInteractiosCreate()
        {
            chartCategoryInteractios.layerType = ChartDataLayer.ChartlayerType.Pie;
            ChartDataLayer data;

            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            foreach (Category categ in db.Categories)
            {
                int interCnt = 0;
                var query = (from inter in db.InteractionPharmacyPharmacies
                             where inter.PharmacyChemical1.SubCategory.categoryName == categ.categoryName
                             select inter.PharmacyChemical);
                interCnt += query.Count();
                var query0 = (from inter in db.InteractionPharmacyPharmacies
                              where inter.PharmacyChemical.SubCategory.categoryName == categ.categoryName
                              select inter.PharmacyChemical1);
                interCnt += query0.Count();
                var query1 = (from inter in db.InteractionPharmacySubCategories
                              where inter.PharmacyChemical.SubCategory.categoryName == categ.categoryName
                              select inter.subCategoryName);
                foreach (String subCatQ2 in query1)
                {
                    var query3 = from phar in db.PharmacyChemicals
                                 where phar.subCategoryName == subCatQ2
                                 select phar;
                    interCnt += query3.Count();

                }
                var query2 = (from inter in db.InteractionPharmacySubCategories
                              where inter.SubCategory.categoryName == categ.categoryName
                              select inter);
                interCnt += query2.Count();

                if (interCnt > 0)
                {
                    data = chartCategoryInteractios.add(categ.categoryName.Trim());
                    data.update(interCnt);
                }


            }
            drawChartPie1(chartCategoryInteractios, "");

        }
        private void chartCategoryPharmacyCommercialCreate()
        {
            chartCategoryPharmacyCommercial.layerType = ChartDataLayer.ChartlayerType.Pie;
            ChartDataLayer data;

            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            foreach (Category categ in db.Categories)
            {

                var query = (from phar in db.PharmacyCommercials
                             where phar.PharmacyChemical.SubCategory.categoryName == categ.categoryName
                             select phar);
                if (query.Count() > 0)
                {
                    data = chartCategoryPharmacyCommercial.add(categ.categoryName.Trim());
                    data.update(query.Count());
                }


            }
            drawChartPie1(chartCategoryPharmacyCommercial, "");

        }
        private void chartCategoryInteractiosCommercialCreate()
        {
            chartCategoryInteractiosCommercial.layerType = ChartDataLayer.ChartlayerType.Pie;
            
            drawChartPie1(chartCategoryInteractiosCommercial, "");

        }


    }
}
