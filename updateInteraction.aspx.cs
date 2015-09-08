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
    public partial class updateInteraction : System.Web.UI.Page
    {
        String chemicalName;
        String pharmacyChemicalStartWith = "";
        String subCategoryStartWith = "";
        int subCategoryResultIndex = 0;
        int subCategoryMaxIndex = 0;
        int pharmacyChemicalResultIndex = 0;
        int pharmacyChemicalMaxIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            parseArguments();
            if (!IsPostBack)
            {
                loadMenus();

                Util.Style.setGrindviewStyle(ref grdInteractions);
                Util.Style.setGrindviewStyle(ref grdInteractionsSubCategory);
                Util.Style.setGrindviewStyle(ref  grdPharmacyChemical);
                Util.Style.setGrindviewStyle(ref  grdSubCategory);
                loadInteractions();
            }
            loadChemicalPharmacies();
            loadSubCategories();
        }
        private void loadInteractions()
        {
            loadInteractionPharmacyPharmacy();
            loadInteractionPharmacySubCategory();
        }

        private void loadInteractionPharmacySubCategory()
        {
            DatabaseDataContext db = new DatabaseDataContext();

            try
            {
                var query = (from inter in db.InteractionPharmacySubCategories
                             where (inter.chemicalName == chemicalName)
                             select  inter.subCategoryName.ToString()+"|"+ inter.comment.ToString());

                if (query.Count() > 0)
                {
                    DataTable dt = new DataTable();
                    DataColumn dc;


                    dc = new DataColumn("subCategoryName");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("comment");
                    dt.Columns.Add(dc);



                    String[] res = query.ToArray();
                    foreach (String str in res)
                    {
                        String sname;
                        String comment = "";
                        String[] tmp = str.Split("|".ToCharArray());
                        sname = tmp[0];

                        if (tmp.Length > 1)
                        {
                            comment = tmp[1];
                        }

                        DataRow dr = dt.NewRow();
                        dr["subCategoryName"] = sname;
                        dr["comment"] = comment;
                        dt.Rows.Add(dr);

                    }

                    grdInteractionsSubCategory.DataSource = dt;
                    grdInteractionsSubCategory.DataBind();


                    for (int i = 0; i < grdInteractionsSubCategory.Rows.Count; i++)
                    {
                        TableCell cell0 = grdInteractionsSubCategory.Rows[i].Cells[0];
                        TableCell cell1 = grdInteractionsSubCategory.Rows[i].Cells[1];
                        //TableCell cell2 = grdInteractions.Rows[i].Cells[2];
                        HyperLink field0 = cell0.Controls[0] as HyperLink;
                        HyperLink field1 = cell1.Controls[0] as HyperLink;
                        //HyperLink field2 = cell2.Controls[0] as HyperLink;
                        field0.NavigateUrl = Util.UrlRewriting.encodeUrl("updatePharmacy.aspx?chemicalName=" + field0.Text);

                    }
                }
            }
            catch
            {
                //TODO : ektipioosi sostou lathous
                Response.Write("Error");
            }
        }
        private void loadInteractionPharmacyPharmacy()
        {
            DatabaseDataContext db = new DatabaseDataContext();

            try
            {
                var query = (from inter in db.InteractionPharmacyPharmacies
                             where (inter.pharmacyname1 == chemicalName || inter.pharmacyname2 == chemicalName)
                             select
                                 inter.pharmacyname1.ToString() + "|" + inter.pharmacyname2.ToString() + "|" + inter.comment.ToString());
                if (query.Count() > 0)
                {
                    DataTable dt = new DataTable();
                    DataColumn dc;


                    dc = new DataColumn("chemicalName");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("comment");
                    dt.Columns.Add(dc);



                    String[] res = query.ToArray();
                    foreach (String str in res)
                    {
                        String cname;
                        String comment = "";
                        String[] tmp = str.Split("|".ToCharArray());
                        if (tmp[0].StartsWith(chemicalName))
                        {
                            cname = tmp[1];
                        }
                        else
                        {
                            cname = tmp[0];
                        }
                        if (tmp.Length > 1)
                        {
                            comment = tmp[2];
                        }

                        DataRow dr = dt.NewRow();
                        dr["chemicalName"] = cname;
                        dr["comment"] = comment;
                        dt.Rows.Add(dr);

                    }

                    grdInteractions.DataSource = dt;
                    grdInteractions.DataBind();


                    for (int i = 0; i < grdInteractions.Rows.Count; i++)
                    {
                        TableCell cell0 = grdInteractions.Rows[i].Cells[0];
                        TableCell cell1 = grdInteractions.Rows[i].Cells[1];
                        //TableCell cell2 = grdInteractions.Rows[i].Cells[2];
                        HyperLink field0 = cell0.Controls[0] as HyperLink;
                        HyperLink field1 = cell1.Controls[0] as HyperLink;
                        //HyperLink field2 = cell2.Controls[0] as HyperLink;
                        field0.NavigateUrl = Util.UrlRewriting.encodeUrl("updatePharmacy.aspx?chemicalName=" + field0.Text);

                    }
                }
            }
            catch
            {
                //TODO : ektipioosi sostou lathous
                Response.Write("Error");
            }
        }
        private void loadSubCategories()
        {
            if (subCategoryStartWith.Length < 1)
                return;
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            var query = from subc in db.SubCategories
                        where subc.subCategoryName.StartsWith(subCategoryStartWith)
                        select new
                        {
                            subCategoryName = subc.subCategoryName.Trim(),
                            category = subc.categoryName.Trim()
                        };
            subCategoryMaxIndex = query.Count() / Util.miniResultSize;

            int start = subCategoryResultIndex * Util.miniResultSize;
            txtSubCategoryIndex.Text = (start + 1).ToString() + "-" + (start + grdSubCategory.Rows.Count).ToString() + " of " + query.Count();


            grdSubCategory.DataSource = query.Skip(subCategoryResultIndex * Util.miniResultSize).Take(Util.miniResultSize);
            grdSubCategory.DataBind();


            for (int i = 0; i < grdSubCategory.Rows.Count; i++)
            {
                TableCell cell0 = grdSubCategory.Rows[i].Cells[0];
                TableCell cell1 = grdSubCategory.Rows[i].Cells[1];
                //               TableCell cell2 = grdPharmacyChemical.Rows[i].Cells[2];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;
                //            HyperLink field2 = cell2.Controls[0] as HyperLink;


        //        field0.NavigateUrl = Util.UrlRewriting.encodeUrl("updatePharmacy.aspx?chemicalName=" + field0.Text);
        //        field1.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field1.Text);

            }

            if (subCategoryResultIndex + 1 > subCategoryMaxIndex)
            {
                btnSubCategoryNext.Disabled = true;
                //  btnNext.Visible = false;
            }
            else
            {
                btnSubCategoryNext.HRef = Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (subCategoryResultIndex + 1).ToString() + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex).ToString());
            }
            if (subCategoryResultIndex - 1 < 0)
            {
                btnSubCategoryPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (subCategoryResultIndex - 1 ).ToString() + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex).ToString());
            }
        }
        private void loadChemicalPharmacies()
        {
            if (pharmacyChemicalStartWith.Length < 1)
                return;
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            var query = from phar in db.PharmacyChemicals
                        where phar.chemicalName.StartsWith(pharmacyChemicalStartWith)
                        select new
                        {
                            chemicalName = phar.chemicalName.Trim(),
                            category = phar.SubCategory.categoryName.Trim()
                        };
            pharmacyChemicalMaxIndex = query.Count() / Util.miniResultSize;

            int start = pharmacyChemicalResultIndex * Util.miniResultSize;
            txtPharmacyChemicalIndex.Text = (start + 1).ToString() + "-" + (start + grdPharmacyChemical.Rows.Count).ToString() + " of " + query.Count();


            grdPharmacyChemical.DataSource = query.Skip(pharmacyChemicalResultIndex * Util.miniResultSize).Take(Util.miniResultSize);
            grdPharmacyChemical.DataBind();


            for (int i = 0; i < grdPharmacyChemical.Rows.Count; i++)
            {
                TableCell cell0 = grdPharmacyChemical.Rows[i].Cells[0];
                TableCell cell1 = grdPharmacyChemical.Rows[i].Cells[1];
                //               TableCell cell2 = grdPharmacyChemical.Rows[i].Cells[2];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;
                //            HyperLink field2 = cell2.Controls[0] as HyperLink;


                field0.NavigateUrl = Util.UrlRewriting.encodeUrl("updatePharmacy.aspx?chemicalName=" + field0.Text);
                field1.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field1.Text);

            }

            if (pharmacyChemicalResultIndex + 1 > pharmacyChemicalMaxIndex)
            {
                btnPharmacyChemicalNext.Disabled = true;
                //  btnNext.Visible = false;
            }
            else
            {
                btnPharmacyChemicalNext.HRef = Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (subCategoryResultIndex).ToString() + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex +1).ToString());
            }
            if (pharmacyChemicalResultIndex - 1 < 0)
            {
                btnPharmacyChemicalPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                btnPharmacyChemicalPrev.HRef = Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (subCategoryResultIndex).ToString() + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex - 1).ToString());
            }
        }
        private void parseArguments()
        {
            try
            {
                pharmacyChemicalResultIndex = int.Parse(Request["pharmacyChemicalIndex"]);
            }
            catch
            {
                pharmacyChemicalResultIndex = 0;
            }
            if (txtPharmacyChemicalFilter.Text != "")
            {
                pharmacyChemicalStartWith = txtPharmacyChemicalFilter.Text;
                pharmacyChemicalResultIndex = 0;
            }
            else
            {
                pharmacyChemicalStartWith = Request["pharmacyChemicalStartWith"];

                if (pharmacyChemicalStartWith == null)
                {
                    pharmacyChemicalStartWith = "";

                }
                txtPharmacyChemicalFilter.Text = pharmacyChemicalStartWith;
            }

            try
            {
                subCategoryResultIndex = int.Parse(Request["subCategoryIndex"]);
            }
            catch
            {
                subCategoryResultIndex = 0;
            }
            if (txtSubCategoryFilter.Text != "")
            {
                subCategoryStartWith = txtSubCategoryFilter.Text;
                subCategoryResultIndex = 0;
            }
            else
            {
                subCategoryStartWith = Request["subCategoryStartWith"];

                if (subCategoryStartWith == null)
                {
                    subCategoryStartWith = "";
                }
                txtSubCategoryFilter.Text = subCategoryStartWith;
            }
            chemicalName = Request["chemicalName"];
            if (chemicalName == null)
            {
                Response.Redirect("updateInteractions.aspx");
            }
            h1PharmacyName.InnerText = chemicalName + ": Update Interactions ";
        }
        protected void btnPharmacyChemicalSearch_Click(object sender, EventArgs e)
        {

            pharmacyChemicalStartWith = txtPharmacyChemicalFilter.Text;
            pharmacyChemicalResultIndex = 0;
            Response.Redirect(Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (subCategoryResultIndex).ToString() + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex).ToString()));
        }
        protected void btnSubCategorySearch_Click(object sender, EventArgs e)
        {

            subCategoryStartWith = txtSubCategoryFilter.Text;
            subCategoryResultIndex = 0;
            Response.Redirect(Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (subCategoryResultIndex).ToString() + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex).ToString()));
        }
        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createAdminMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }

        protected void grdInteractions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "deleteRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdInteractions.Rows[i].Cells[0];
                TableCell cell1 = grdInteractions.Rows[i].Cells[1];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;

                interactionDelete(chemicalName, field0.Text,true);
            }
            else if (e.CommandName == "addRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdPharmacyChemical.Rows[i].Cells[0];
                TableCell cell1 = grdPharmacyChemical.Rows[i].Cells[1];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;
                Response.Redirect(Util.UrlRewriting.encodeUrl("createInteraction.aspx?chemicalName=" + chemicalName + "&chemicalName2=" + field0.Text));
            }
        }
        protected void grdInteractionsSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "deleteRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdInteractionsSubCategory.Rows[i].Cells[0];
                TableCell cell1 = grdInteractionsSubCategory.Rows[i].Cells[1];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;

                interactionDelete(chemicalName, field0.Text,false);
            }
            else if (e.CommandName == "addRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdSubCategory.Rows[i].Cells[0];
                TableCell cell1 = grdSubCategory.Rows[i].Cells[1];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;
                Response.Redirect(Util.UrlRewriting.encodeUrl("createInteraction.aspx?chemicalName=" + chemicalName + "&subCategory=" + field0.Text));

            }
        }
        void interactionDelete(String chem1, String chem2, bool isChemical)
        {
            if (isChemical)
            {
                DatabaseDataContext db = new DatabaseDataContext();
                var query = (from inter in db.InteractionPharmacyPharmacies
                             where (inter.pharmacyname1 == chem1 && inter.pharmacyname2 == chem2)
                             select inter);
                if (query.Count() < 1)
                {
                    query = (from inter in db.InteractionPharmacyPharmacies
                             where (inter.pharmacyname1 == chem2 && inter.pharmacyname2 == chem1)
                             select inter);
                }
                InteractionPharmacyPharmacy tmpItem = query.First();
                db.InteractionPharmacyPharmacies.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
                Response.Redirect(Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex).ToString()));
            }
            else
            {
                DatabaseDataContext db = new DatabaseDataContext();
                var query = (from inter in db.InteractionPharmacySubCategories
                             where (inter.chemicalName == chem1 && inter.subCategoryName == chem2)
                             select inter);
                InteractionPharmacySubCategory tmpItem = query.First();
                db.InteractionPharmacySubCategories.DeleteOnSubmit(tmpItem);
                db.SubmitChanges();
                Response.Redirect(Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex).ToString()));
                
            }
            return;
        }
    }
}
