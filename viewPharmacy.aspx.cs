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
    public partial class viewPharmacy : System.Web.UI.Page
    {
        String chemicalName;
        String category;
        String subCategory;
        public int interactionsResultIndex = 0;
        public int interactionsMaxIndex = 0;
        public int commercialResultIndex = 0;
        public int commercialMaxIndex = 0;

        private void loadInteractions()
        {
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            int cnt = 0;
            try
            {
                DataTable dt = new DataTable();
                DataColumn dc;

                dc = new DataColumn("name");
                dt.Columns.Add(dc);
                dc = new DataColumn("chemicalName");
                dt.Columns.Add(dc);
                dc = new DataColumn("category");
                dt.Columns.Add(dc);
                dc = new DataColumn("comment");
                dt.Columns.Add(dc);

                var queryInteractionPharmacyPharmac = (from inter in db.InteractionPharmacyPharmacies
                             where (inter.pharmacyname1 == chemicalName || inter.pharmacyname2 == chemicalName)
                             select
                                 inter.pharmacyname1 + "|" + inter.pharmacyname2 + "|" + inter.comment.ToString());
                if (queryInteractionPharmacyPharmac.Count() > 0)
                {



                    String[] res = queryInteractionPharmacyPharmac.ToArray();
                    foreach (String str in res)
                    {
                        String cname;
                        String[] tmp = str.Split("|".ToCharArray());
                        if (tmp[0].StartsWith(chemicalName))
                        {
                            cname = tmp[1];
                        }
                        else
                        {
                            cname = tmp[0];
                        }
                        String interComment = tmp[2];

                        if (cnt >= interactionsResultIndex * Util.resultSize && cnt < (interactionsResultIndex + 1) * Util.resultSize)
                        {
                            DataRow dr = dt.NewRow();
                            dr["chemicalName"] = cname;
                            dr["category"] = (from phar in db.PharmacyChemicals
                                              where phar.chemicalName == cname
                                              select phar.SubCategory.categoryName).First();
                            dr["comment"] = interComment;

                            dt.Rows.Add(dr);
                        }
                        cnt++;

                    }

                }

                var queryInteractionPharmacySubCategory = (from inter in db.InteractionPharmacySubCategories
                                                           where inter.chemicalName.Trim() == chemicalName
                                                           select inter.SubCategory);
                if (queryInteractionPharmacySubCategory.Count() > 0)
                {

                    foreach (SubCategory subc in queryInteractionPharmacySubCategory)
                    {
                        String interComment = (from inter in db.InteractionPharmacySubCategories
                                               where (inter.chemicalName.Trim() == chemicalName && inter.subCategoryName.Trim() == subc.subCategoryName)
                                               select inter.comment).First();

                        var query = (from phar in db.PharmacyChemicals
                                     where phar.subCategoryName == subc.subCategoryName
                                     select phar);
                        foreach (PharmacyChemical phar in query)
                        {
                            if (cnt >= interactionsResultIndex * Util.resultSize && cnt < (interactionsResultIndex + 1) * Util.resultSize)
                            {

                                DataRow dr = dt.NewRow();
                                
                                dr["chemicalName"] = phar.chemicalName.Trim();
                                dr["category"] = (from pharm in db.PharmacyChemicals
                                                  where pharm.chemicalName == phar.chemicalName
                                              select pharm.SubCategory.categoryName).First();
                                dr["comment"] = interComment;
                                dt.Rows.Add(dr);

                            }
                            cnt++;

                        }
                    }
                }

                var queryInteractionSubCategoryPharmacy = (from inter in db.InteractionPharmacySubCategories
                                                           where (inter.subCategoryName.Trim() == subCategory)
                                                           select inter.chemicalName.Trim());
                if (queryInteractionSubCategoryPharmacy.Count() > 0)
                {
                    foreach (String pharmacy in queryInteractionSubCategoryPharmacy)
                    {
                        String interComment = (from inter in db.InteractionPharmacySubCategories
                                               where (inter.chemicalName.Trim() == pharmacy && inter.subCategoryName.Trim() == subCategory)
                                               select inter.comment).First();
                        var query = (from phar in db.PharmacyChemicals
                                     where phar.chemicalName.Trim() == pharmacy
                                     select phar);
                        foreach (PharmacyChemical phar in query)
                        {
                            if (cnt >= interactionsResultIndex * Util.resultSize && cnt < (interactionsResultIndex + 1) * Util.resultSize)
                            {

                                DataRow dr = dt.NewRow();
                                
                                dr["chemicalName"] = phar.chemicalName.Trim();
                                dr["category"] = (from pharm in db.PharmacyChemicals
                                                  where pharm.chemicalName == phar.chemicalName
                                                  select pharm.SubCategory.categoryName).First();
                                dr["comment"] = interComment;

                                dt.Rows.Add(dr);

                            }
                            cnt++;

                        }
                    }
                }


                    grdInteractions.DataSource = dt;
                    grdInteractions.DataBind();

                    int start = interactionsResultIndex * Util.resultSize;
                    txtInteractionsIndex.Text = (start+1).ToString() + "-" + (start + grdInteractions.Rows.Count).ToString() + " of " + queryInteractionPharmacyPharmac.Count();

                    for (int i = 0; i < grdInteractions.Rows.Count; i++)
                    {
                        TableCell cell0 = grdInteractions.Rows[i].Cells[0];
                        TableCell cell1 = grdInteractions.Rows[i].Cells[1];
                        //TableCell cell2 = grdInteractions.Rows[i].Cells[2];
                        HyperLink field0 = cell0.Controls[0] as HyperLink;
                        HyperLink field1 = cell1.Controls[0] as HyperLink;
                        //HyperLink field2 = cell2.Controls[0] as HyperLink;
                        field0.NavigateUrl = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + field0.Text);
                        field1.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field1.Text);
                        //field2.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field2.Text);

                    }

                    if (interactionsResultIndex + 1 > interactionsMaxIndex)
                    {
                        btnInteractionsNext.Disabled = true;
                        //  btnNext.Visible = false;
                    }
                    else
                    {
                        btnInteractionsNext.HRef = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + chemicalName + "&interactionsIndex=" + (interactionsResultIndex + 1).ToString() + "&commercialsIndex=" + (commercialResultIndex).ToString());

                    }
                    if (interactionsResultIndex - 1 < 0)
                    {
                        btnInteractionsPrev.Disabled = true;
                        // btnPrev.Visible = false;
                    }
                    else
                    {
                        btnInteractionsPrev.HRef = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + chemicalName + "&interactionsIndex=" + (interactionsResultIndex - 1).ToString() + "&commercialsIndex=" + (commercialResultIndex).ToString());
                    }
                
            }
            catch
            {
                //TODO : ektipioosi sostou lathous
                Response.Write("Error");
            }
        }
        private void loadCommercials()
        {
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            try
            {
                var query = (from com in db.PharmacyCommercials
                             where (com.chemicalName == chemicalName)
                             select new
                             {

                                 name = com.name.Trim(),
                                 company = com.company.Trim(),
                                 morph = com.morph.Trim()
                             });
                commercialMaxIndex = query.Count() / Util.resultSize;
                grdCommercials.DataSource = query.Skip(commercialResultIndex * Util.resultSize).Take(Util.resultSize);
                grdCommercials.DataBind();


                int start = commercialResultIndex * Util.resultSize;
                lblCommercialsIndex.Text = (start+1).ToString() + "-" + (start + grdCommercials.Rows.Count).ToString() + " of " + query.Count();

                for (int i = 0; i < grdCommercials.Rows.Count; i++)
                {
                    TableCell cell0 = grdCommercials.Rows[i].Cells[0];
                    TableCell cell1 = grdCommercials.Rows[i].Cells[1];
                    TableCell cell2 = grdCommercials.Rows[i].Cells[2];
                    HyperLink field0 = cell0.Controls[0] as HyperLink;
                    HyperLink field1 = cell1.Controls[0] as HyperLink;
                    HyperLink field2 = cell2.Controls[0] as HyperLink;
                    field0.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + field0.Text + "&company=" + field1.Text + "&morph=" + field2.Text);
                    field1.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + field0.Text + "&company=" + field1.Text + "&morph=" + field2.Text);
                    field2.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + field0.Text + "&company=" + field1.Text + "&morph=" + field2.Text);
                }

                if (commercialResultIndex + 1 > commercialMaxIndex)
                {
                    btnCommercialsNext.Disabled = true;
                    //  btnNext.Visible = false;
                }
                else
                {
                    btnCommercialsNext.HRef = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + chemicalName + "&interactionsIndex=" + (interactionsResultIndex).ToString() + "&commercialsIndex=" + (commercialResultIndex + 1).ToString());

                }
                if (interactionsResultIndex - 1 < 0)
                {
                    btnCommercialsPrev.Disabled = true;
                    // btnPrev.Visible = false;
                }
                else
                {
                    btnCommercialsPrev.HRef = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + chemicalName + "&interactionsIndex=" + (interactionsResultIndex).ToString() + "&commercialsIndex=" + (commercialResultIndex - 1).ToString());
                }
            }
            catch
            {
                //TODO : ektipioosi sostou lathous
                Response.Write("Error");
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            parseArguments();
            loadMenus();


            loadPharmacy();
            loadCommercials();
            loadInteractions();

            Util.Style.setGrindviewStyle(ref grdInteractions);
            Util.Style.setGrindviewStyle(ref grdCommercials);
        }

        private void loadPharmacy()
        {
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            try
            {
                h1PharmacyName.InnerText = "Φάρμακο " + chemicalName;
                lblChemicalName.Text = chemicalName;

                lblIndication.Text = (from phar in db.PharmacyChemicals
                                   where phar.chemicalName == chemicalName
                                   select phar.indication).First();
                }
            catch
            {
                lblIndication.Text = "-";
            }
            try
            {
                lblGreekChemicalName.Text = (from phar in db.PharmacyChemicals
                                where phar.chemicalName == chemicalName
                                select phar.greekName).First();
            }
            catch
            {
                lblGreekChemicalName.Text = "-";
            }
            try
            {
                lblUndesirableReaction.Text = (from phar in db.PharmacyChemicals
                                             where phar.chemicalName == chemicalName
                                             select phar.undesirableReactions).First();
            }
            catch
            {
                lblUndesirableReaction.Text = "-";
            }
            try
            {
                lblInteractions.Text = (from phar in db.PharmacyChemicals
                                where phar.chemicalName == chemicalName
                                select phar.interactionGeneral).First();
            }
            catch
            {
                lblInteractions.Text = "-";
            }
            try
            {
                lblDoce.Text = (from phar in db.PharmacyChemicals
                                where phar.chemicalName == chemicalName
                                select phar.dose).First();
            }
            catch
            {
                lblDoce.Text = "-";
            }
             try
             {   lblContraIndication.Text = (from phar in db.PharmacyChemicals
                                      where phar.chemicalName == chemicalName
                                      select phar.contraIndication).First();
             }
            catch
            {
                 lblContraIndication.Text = "-";
            }
            try
            {

                category = (from phar in db.PharmacyChemicals
                                    where phar.chemicalName == chemicalName
                            select phar.SubCategory.categoryName).First();
                lblCategory.InnerText = category;
                lblCategory.HRef = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + category);
            
            }
            catch
            {
                lblCategory.InnerText = "-";
            }
            try
            {

                subCategory = (from phar in db.PharmacyChemicals
                            where phar.chemicalName == chemicalName
                            select phar.subCategoryName).First();
                lblSubCategory.InnerText = subCategory;
                lblSubCategory.HRef = Util.UrlRewriting.encodeUrl("viewSubCategory.aspx?name=" + subCategory);

            }
            catch
            {
                lblSubCategory.InnerText = "-";
            }
        }

        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }

        private void parseArguments()
        {
            try
            {
                interactionsResultIndex = int.Parse(Request["interactionsIndex"]);
            }
            catch
            {
                interactionsResultIndex = 0;
            }
            try
            {
                commercialResultIndex = int.Parse(Request["commercialsIndex"]);
            }
            catch
            {
                commercialResultIndex  = 0;
            }
            chemicalName = Request["chemicalName"];
            if (chemicalName == null)
            {
                Response.Redirect("viewPharmacies.aspx");
            }
        }
   
    }
}
