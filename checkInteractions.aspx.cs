using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


namespace PhamacyDB
{

    public partial class checkInteractions : System.Web.UI.Page
    {
        String startWith = "";
        public int resultIndex = 0;

        public int maxIndex = 0;
        DataTable dt;
        String dataTable = "";
        protected void Page_Load(object sender, EventArgs e)
        {

           
            pareseArguments();
            //if (checkInteractionsScriptManager.AsyncPostBackSourceElementID == updateInteractionsPostBack.UniqueID)
            {
                loadInteractions();
            }
            //if (checkInteractionsScriptManager.AsyncPostBackSourceElementID == updatePharmaciesPostBack.UniqueID)
            {
                loadPharmacies();
            }
            if (!this.IsPostBack)
            {
                loadMenus();
                Util.Style.setGrindviewStyle(ref GridView1);
                Util.Style.setGrindviewStyle(ref lstPharmacies);

                txtFilter.Text = startWith;
                txtFilter.Focus();
            }
            //if (checkInteractionsScriptManager.AsyncPostBackSourceElementID == updateCheckInteractionsPostBack.UniqueID)
            {
                checkInteractionsConflicts();
            }

            Page.Title = "checkInteractions.aspx?startWith=" + startWith + "&index=" + (resultIndex).ToString() + "&data=" + setEncode(dt);

        }
        private void checkInteractionsConflicts()
        {
            int interCount = 0;
            interactioTable.InnerHtml = "";
            for(int i=0;i<dt.Rows.Count;i++)
            {
                DatabaseDataContext db = new DatabaseDataContext();
                String cname1;
                cname1 = (from phar in db.PharmacyCommercials
                          where (phar.name.Trim() == (string)dt.Rows[i]["name"] && phar.company.Trim() == (string)dt.Rows[i]["company"] && phar.morph.Trim() == (string)dt.Rows[i]["morph"])
                         select phar.chemicalName).First().Trim();
                String subCat1 = (from phar in db.PharmacyChemicals
                                  where (phar.chemicalName.Trim() == cname1)
                                  select phar.subCategoryName).First().Trim();
                var queryInterPharmacyPharmacy = from inter in db.InteractionPharmacyPharmacies
                                                 where (inter.pharmacyname1.Trim() == cname1 || inter.pharmacyname2.Trim() == cname1)
                            select inter;
                var queryInterPharmacySubCategory = from inter in db.InteractionPharmacySubCategories
                                                    where (inter.chemicalName.Trim() == cname1)
                                                 select inter;
                var queryInterSubCategoryPharmacy = from inter in db.InteractionPharmacySubCategories
                                                    where (inter.subCategoryName.Trim() == subCat1)
                                                 select inter;

                for(int j=0;j<i;j++)
                {
                    String cname2 = (from phar in db.PharmacyCommercials
                                     where (phar.name.Trim() == (string)dt.Rows[j]["name"] && phar.company.Trim() == (string)dt.Rows[j]["company"] && phar.morph.Trim() == (string)dt.Rows[j]["morph"])
                                     select phar.chemicalName).First().Trim();
                    String subCat2 = (from phar in db.PharmacyChemicals
                                      where (phar.chemicalName.Trim() == cname2)
                                      select phar.subCategoryName).First().Trim();
                    if (cname1 == cname2)
                        continue;
                    //Check PharmacyPharmacy Interactions
                    foreach(InteractionPharmacyPharmacy inter in queryInterPharmacyPharmacy)
                    {
                        if (inter.pharmacyname1.Trim() == cname2 || inter.pharmacyname2.Trim() == cname2)
                        {
                            interactioTable.InnerHtml += "<h2>Αλληλεπίδραση #" + interCount++ + "</h2>";

                            interactioTable.InnerHtml += "<div class=\"grindView\">";
                            interactioTable.InnerHtml += "<table><tr><td></td><th>Ονομασία</th><th>Εταιρεία</th><th>Μορφή</th><th>Χημική Ονομασία</th>";
                            interactioTable.InnerHtml += "<th>Αλληλεπίδραση</th></tr><tr class=\"alt1\"><th>Φάρμακο</th><td>";
                            interactioTable.InnerHtml += "<a style=\"display:inline-block;width:100%;\">" + dt.Rows[j]["name"] + "</a></td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[j]["company"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[j]["morph"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + cname2 + "</td>";
                            interactioTable.InnerHtml += "<td rowspan=2 class=\"alt2\">" + inter.comment + "</td>";
                            interactioTable.InnerHtml += "</tr><tr><th>Φάρμακο</th><td>";
                            interactioTable.InnerHtml += "<a style=\"display:inline-block;width:100%;\">" + dt.Rows[i]["name"] + "</a></td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[i]["company"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[i]["morph"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + cname1 + "</td>";
                            interactioTable.InnerHtml += "</tr></table></div>";                            
                        }
                    }
                    //Check Pharmacy SubCategory Interaction
                    foreach (InteractionPharmacySubCategory inter in queryInterPharmacySubCategory)
                    {
                        if (inter.subCategoryName.Trim() == subCat2)
                        {
                            interactioTable.InnerHtml += "<h2>Αλληλεπίδραση #" + interCount++ + "</h2>";

                            interactioTable.InnerHtml += "<div class=\"grindView\">";
                            interactioTable.InnerHtml += "<table><tr><td></td><th>Ονομασία</th><th>Εταιρεία</th><th>Μορφή</th><th>Χημική Ονομασία</th>";
                            interactioTable.InnerHtml += "<th>Αλληλεπίδραση</th></tr><tr class=\"alt1\"><th>Φάρμακο</th><td>";
                            interactioTable.InnerHtml += "<a style=\"display:inline-block;width:100%;\">"+ dt.Rows[j]["name"]+"</a></td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[j]["company"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[j]["morph"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + cname2 + "</td>";
                            interactioTable.InnerHtml += "<td rowspan=2 class=\"alt2\">"+inter.comment+"</td>";
                            interactioTable.InnerHtml += "</tr><tr><th>Φάρμακο</th><td>";
                            interactioTable.InnerHtml += "<a style=\"display:inline-block;width:100%;\">"+dt.Rows[i]["name"]+"</a></td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[i]["company"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[i]["morph"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + cname1 + "</td>";
                            interactioTable.InnerHtml += "</tr></table></div>";
                          
                        }
                    }
                    //Check Pharmacy SubCategory Interaction
                    foreach (InteractionPharmacySubCategory inter in queryInterSubCategoryPharmacy)
                    {
                        if (inter.chemicalName.Trim() == cname2)
                        {
                            interactioTable.InnerHtml += "<h2>Αλληλεπίδραση #" + interCount++ + "</h2>";

                            interactioTable.InnerHtml += "<div class=\"grindView\">";
                            interactioTable.InnerHtml += "<table><tr><td></td><th>Ονομασία</th><th>Εταιρεία</th><th>Μορφή</th><th>Χημική Ονομασία</th>";
                            interactioTable.InnerHtml += "<th>Αλληλεπίδραση</th></tr><tr class=\"alt1\"><th>Φάρμακο</th><td>";
                            interactioTable.InnerHtml += "<a style=\"display:inline-block;width:100%;\">" + dt.Rows[j]["name"] + "</a></td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[j]["company"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[j]["morph"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + cname2 + "</td>";
                            interactioTable.InnerHtml += "<td rowspan=2 class=\"alt2\">" + inter.comment + "</td>";
                            interactioTable.InnerHtml += "</tr><tr><th>Φάρμακο</th><td>";
                            interactioTable.InnerHtml += "<a style=\"display:inline-block;width:100%;\">" + dt.Rows[i]["name"] + "</a></td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[i]["company"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + dt.Rows[i]["morph"] + "</td>";
                            interactioTable.InnerHtml += "<td>" + cname1 + "</td>";
                            interactioTable.InnerHtml += "</tr></table></div>";                            
                        }
                    }
                }
                
               
            }
            
        }
        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }

        private void loadPharmacies()
        {
            if (startWith.Length < 1)
                return;
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            var query = from phar in db.PharmacyCommercials
                        where phar.name.StartsWith(startWith)
                        orderby phar.name
                        select new
                        {
                            name = phar.name.Trim(),
                            company = phar.company.Trim(),
                            morph = phar.morph.Trim()
                        };
            maxIndex = query.Count() / Util.miniResultSize;

            int start = resultIndex * Util.miniResultSize;
            txtIndex.Text = (start + 1).ToString() + "-" + (start + lstPharmacies.Rows.Count).ToString() + " of " + query.Count();


            lstPharmacies.DataSource = query.Skip(resultIndex * Util.miniResultSize).Take(Util.miniResultSize);
            lstPharmacies.DataBind();


            for (int i = 0; i < lstPharmacies.Rows.Count; i++)
            {
                TableCell cell0 = lstPharmacies.Rows[i].Cells[0];
                TableCell cell1 = lstPharmacies.Rows[i].Cells[1];
                TableCell cell2 = lstPharmacies.Rows[i].Cells[2];
                TableCell cell3 = lstPharmacies.Rows[i].Cells[3];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;
                HyperLink field2 = cell2.Controls[0] as HyperLink;
                HyperLink field3 = cell3.Controls[0] as HyperLink;

                DataRow dr = dt.NewRow();

                dr["name"] = field0.Text;
                dr["company"] = field1.Text;
                dr["morph"] = field2.Text;
                dt.Rows.Add(dr);
                field0.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + field0.Text + "&company=" + field1.Text + "&morph=" + field2.Text);

                field3.NavigateUrl = "javascript:updateData('"+setEncode(dt)+"')";

                //field3.NavigateUrl = Util.UrlRewriting.encodeUrl("checkInteractions.aspx?startWith=" + startWith + "&index=" + (resultIndex).ToString() + "&data=" + setEncode(dt));
                dt.Rows.RemoveAt(dt.Rows.Count -1);
                
                // field1.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field1.Text);
                // field2.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field2.Text);

            }

            if (resultIndex + 1 > maxIndex)
            {
                btnNext.Disabled = true;
                //  btnNext.Visible = false;
            }
            else
            {
                btnNext.HRef = "javascript:updatePharmaciesIndex('" + (resultIndex + 1).ToString() + "');";
               

            }
            if (resultIndex - 1 < 0)
            {
                btnPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                btnPrev.HRef = "javascript:updatePharmaciesIndex('" + (resultIndex - 1).ToString() + "');";
            }

            
        }
        String setEncode(DataTable dt)
        {
            String res = "";
            foreach(DataRow dr in dt.Rows)
            {
                res += dr["name"]+"<??-??>";
                res += dr["company"] + "<??-??>";
                res += dr["morph"] + "<??_??>";
            }
            return Util.Compress(res);
        }
        DataTable getDecode(String strComp)
        {
            String str = Util.Decompress(strComp);
            DataTable dt = new DataTable();
            String[] delimString = new String[1];
            delimString[0] = "<??-??>";
            String[] rowString = new String[1];
            rowString[0] = "<??_??>";

            DataColumn dc;
            dt = new DataTable();
            dc = new DataColumn("name");
            dt.Columns.Add(dc);
            dc = new DataColumn("company");
            dt.Columns.Add(dc);
            dc = new DataColumn("morph");
            dt.Columns.Add(dc);
            if (str == null || str == "")
                return dt;
            
            String[] rows = str.Split(rowString, StringSplitOptions.None);
            foreach (String row in rows)
            {
                String[] Cols = row.Split(delimString, StringSplitOptions.None);
                if (Cols.Count() < 3)
                    continue;
                DataRow dr = dt.NewRow();

                dr["name"] = Cols[0];
                dr["company"] = Cols[1];
                dr["morph"] = Cols[2];
                dt.Rows.Add(dr);

            }
            return dt;
        }
        private void loadInteractions()
        {
          
                dt = getDecode(dataTable);
                if (dataTable == "")
                {
                    DataRow dr = dt.NewRow();

                    dr["name"] = "";
                    dr["company"] = "";
                    dr["morph"] = "";
                    dt.Rows.Add(dr);
                    
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    dt.Rows.Remove(dt.Rows[0]);

                    GridView1.Rows[0].Visible = false;
                    GridView1.Rows[0].Controls.Clear();
                    return;
                }

                GridView1.DataSource = dt;
                GridView1.DataBind();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TableCell cell0 = GridView1.Rows[i].Cells[0];
                TableCell cell1 = GridView1.Rows[i].Cells[1];
                TableCell cell2 = GridView1.Rows[i].Cells[2];
                TableCell cell3 = GridView1.Rows[i].Cells[3];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;
                HyperLink field2 = cell2.Controls[0] as HyperLink;
                HyperLink field3 = cell3.Controls[0] as HyperLink;

                DataTable dt1 = getDecode(dataTable);
                dt1.Rows.RemoveAt(i);
                field0.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + field0.Text + "&company=" + field1.Text + "&morph=" + field2.Text);

                field3.NavigateUrl = "javascript:updateData('" + setEncode(dt1) + "')";
                //field3.NavigateUrl = Util.UrlRewriting.encodeUrl("checkInteractions.aspx?startWith=" + startWith + "&index=" + (resultIndex).ToString() + "&data=" + setEncode(dt1));
                
                
                // field1.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field1.Text);
                // field2.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field2.Text);

            }

                // hide row


        }
        private void pareseArguments()
        {

            if (hiddenPharmaciesIndex.Value != "")
            {
                int localIndex = int.Parse(hiddenPharmaciesIndex.Value);
                resultIndex = localIndex;
            }
            else
            {
                try
                {
                    resultIndex = int.Parse(Request["index"]);
                }
                catch
                {
                    resultIndex = 0;
                }
            }
            if (hiddenData.Value != "")
            {
                dataTable = hiddenData.Value;
            }
            else
            {
                dataTable = Request["data"];
                if (dataTable == null)
                {
                    dataTable = "";
                }
            }

            if (txtFilter.Text != "")
            {
                startWith = txtFilter.Text;
                resultIndex = 0;
            }
            else
            {
                startWith = Request["startWith"];
                if (startWith == null)
                {
                    startWith = "";
                }
            }
            
        }

        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Write("OK");
            startWith = txtFilter.Text;
            resultIndex = 0;
            Response.Redirect(Util.UrlRewriting.encodeUrl("checkInteractions.aspx?startWith=" + startWith + "&index=" + (resultIndex).ToString() + "&data=" + setEncode(dt)));

        }




    }
}
