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
    public partial class viewCommercialPharmacy : System.Web.UI.Page
    {
        String chemicalName;
        String category;
        String subCategory;
        String name;
        String company;
        String morph;
        public int interactionsResultIndex = 0;
        public int interactionsMaxIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            parseArguments();
            loadMenus();


            loadPharmacy();
            loadInteractions();

            Util.Style.setGrindviewStyle(ref grdInteractions);


        }
        private void loadPharmacy()
        {
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            try
            {
                h1PharmacyName.InnerText = "Φαρμακευτικό Προϊόν " + name;
                lblChemicalName.Text = chemicalName;
                lblCompany.Text = company;
                lblMorp.Text = morph;
                lblName.Text = name;

                lblIndication.Text = (from phar in db.PharmacyChemicals
                                      where phar.chemicalName.Trim() == chemicalName
                                      select phar.indication).First();
            }
            catch
            {
                lblIndication.Text = "-";
            }
            try
            {
                lblGreekChemicalName.Text = (from phar in db.PharmacyChemicals
                                             where phar.chemicalName.Trim() == chemicalName
                                             select phar.greekName).First();
            }
            catch
            {
                lblGreekChemicalName.Text = "-";
            }
            try
            {
                lblUndesirableReaction.Text = (from phar in db.PharmacyChemicals
                                               where phar.chemicalName.Trim() == chemicalName
                                               select phar.undesirableReactions).First();
            }
            catch
            {
                lblUndesirableReaction.Text = "-";
            }
            try
            {
                lblInteractions.Text = (from phar in db.PharmacyChemicals
                                        where phar.chemicalName.Trim() == chemicalName
                                select phar.interactionGeneral).First();
            }
            catch
            {
                lblInteractions.Text = "-";
            }
            try
            {
                lblDoce.Text = (from phar in db.PharmacyChemicals
                                where phar.chemicalName.Trim() == chemicalName
                                select phar.dose).First();
            }
            catch
            {
                lblDoce.Text = "-";
            }
            try
            {
                lblContraIndication.Text = (from phar in db.PharmacyChemicals
                                            where phar.chemicalName.Trim() == chemicalName
                                            select phar.contraIndication).First();
            }
            catch
            {
                lblContraIndication.Text = "-";
            }
            try
            {

                category = (from phar in db.PharmacyChemicals
                            where phar.chemicalName.Trim() == chemicalName
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
                               where phar.chemicalName.Trim() == chemicalName
                               select phar.subCategoryName.Trim()).First();
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
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            try
            {
                interactionsResultIndex = int.Parse(Request["interactionsIndex"]);
            }
            catch
            {
                interactionsResultIndex = 0;
            }

            name = Request["name"];
            company = Request["company"];
            morph = Request["morph"];
            if (name == null || company == null || morph == null)
            {
                //TODO: ERROR Report
                Response.Redirect("Error.aspx");
            }
            chemicalName = (from phar in db.PharmacyCommercials
                            where (phar.name.Trim() == name && phar.company.Trim() == company && phar.morph.Trim() == morph)
                            select phar.chemicalName.Trim()).First();
        }



        private void loadInteractions()
        {
            int cnt = 0;
            DatabaseDataContext db = new DatabaseDataContext();
            try
            {
                DataTable dt = new DataTable();
                DataColumn dc;

                dc = new DataColumn("name");
                dt.Columns.Add(dc);
                dc = new DataColumn("company");
                dt.Columns.Add(dc);
                dc = new DataColumn("morph");
                dt.Columns.Add(dc);
                dc = new DataColumn("chemicalName");
                dt.Columns.Add(dc);
                dc = new DataColumn("comment");
                dt.Columns.Add(dc);
               

                var queryInteractionPharmacyPharmacy = (from inter in db.InteractionPharmacyPharmacies
                                                        where (inter.pharmacyname1.Trim() == chemicalName || inter.pharmacyname2.Trim() == chemicalName)
                             select
                                 inter.pharmacyname1.Trim() + "|" + inter.pharmacyname2.Trim() + "|" + inter.comment.ToString().Trim());
                if (queryInteractionPharmacyPharmacy.Count() > 0)
                {
                    String[] res = queryInteractionPharmacyPharmacy.ToArray();
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
                        PharmacyCommercial[] table = (from phar in db.PharmacyCommercials
                                                      where phar.chemicalName.Trim() == cname
                                            select phar).ToArray();
                        foreach (PharmacyCommercial phar in table)
                        {
                            
                            if(cnt >= interactionsResultIndex * Util.resultSize && cnt < (interactionsResultIndex+1) * Util.resultSize)
                            {
                                
                                    DataRow dr = dt.NewRow();
                                    dr["name"] = phar.name.Trim();
                                    dr["company"] = phar.company.Trim();
                                    dr["morph"] = phar.morph.Trim();
                                    dr["chemicalName"] = phar.chemicalName.Trim();
                                    dr["comment"] = interComment;
                                
                               dt.Rows.Add(dr);
                                
                            }
                            cnt++;
                        }

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
                        
                         var query = (from phar in db.PharmacyCommercials
                                      where phar.PharmacyChemical.subCategoryName == subc.subCategoryName
                                      select phar);
                         foreach (PharmacyCommercial phar in query)
                         {
                             if (cnt >= interactionsResultIndex * Util.resultSize && cnt < (interactionsResultIndex + 1) * Util.resultSize)
                             {

                                 DataRow dr = dt.NewRow();
                                 dr["name"] = phar.name.Trim();
                                 dr["company"] = phar.company.Trim();
                                 dr["morph"] = phar.morph.Trim();
                                 dr["chemicalName"] = phar.chemicalName.Trim();
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
                         var query = (from phar in db.PharmacyCommercials
                                      where phar.chemicalName.Trim() == pharmacy
                                          select phar);
                         foreach (PharmacyCommercial phar in query)
                         {
                             if (cnt >= interactionsResultIndex * Util.resultSize && cnt < (interactionsResultIndex + 1) * Util.resultSize)
                             {

                                 DataRow dr = dt.NewRow();
                                 dr["name"] = phar.name.Trim();
                                 dr["company"] = phar.company.Trim();
                                 dr["morph"] = phar.morph.Trim();
                                 dr["chemicalName"] = phar.chemicalName.Trim();
                                 dr["comment"] = interComment;
                                 
                                 dt.Rows.Add(dr);

                             }
                             cnt++;

                         }
                     }
                 }



                 grdInteractions.DataSource = dt;
                 grdInteractions.DataBind();
                 interactionsMaxIndex = cnt / Util.resultSize;
                 int start = interactionsResultIndex * Util.resultSize;
                 txtInteractionsIndex.Text = (start + 1).ToString() + "-" + (start + grdInteractions.Rows.Count).ToString() + " of " + cnt;


                for (int i = 0; i < grdInteractions.Rows.Count; i++)
                {
                    TableCell cell0 = grdInteractions.Rows[i].Cells[0];
                    TableCell cell1 = grdInteractions.Rows[i].Cells[1];
                    TableCell cell2 = grdInteractions.Rows[i].Cells[2];
                    TableCell cell3 = grdInteractions.Rows[i].Cells[3];
                    HyperLink field0 = cell0.Controls[0] as HyperLink;
                    HyperLink field1 = cell1.Controls[0] as HyperLink;
                    HyperLink field2 = cell2.Controls[0] as HyperLink;
                    HyperLink field3 = cell3.Controls[0] as HyperLink;
                    field0.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + field0.Text + "&company=" + field1.Text + "&morph=" + field2.Text);
                    field3.NavigateUrl = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + field3.Text);

                }

                if (interactionsResultIndex + 1 > interactionsMaxIndex)
                {
                    btnInteractionsNext.Disabled = true;
                    //  btnNext.Visible = false;
                }
                else
                {
                    btnInteractionsNext.HRef = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + name + "&company=" + company + "&morph=" + morph + "&interactionsIndex=" + (interactionsResultIndex + 1).ToString());

                }
                if (interactionsResultIndex - 1 < 0)
                {
                    btnInteractionsPrev.Disabled = true;
                    // btnPrev.Visible = false;
                }
                else
                {
                    btnInteractionsPrev.HRef = Util.UrlRewriting.encodeUrl("viewCommercialPharmacy.aspx?name=" + name + "&company=" + company + "&morph=" + morph + "&interactionsIndex=" + (interactionsResultIndex - 1).ToString());
                }
            }
            catch
            {
                //TODO : ektipioosi sostou lathous
                Response.Write("Error");
              
            }
        }

    }
}
