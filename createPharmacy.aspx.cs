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
    public partial class createPharmacy : System.Web.UI.Page
    {
        String currentCategory = null;
        String startWith = null;
        String pharmacyChemicalStartWith = "";
        int pharmacyChemicalResultIndex = 0;
        int pharmacyChemicalMaxIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            parsePharmacyArguments();

            if (!IsPostBack)
            {
                parseGeneralArguments();
                menu.InnerHtml += Util.Menu.createAdminMenu(Request.Url.LocalPath);
                footerMenu.InnerHtml += Util.Menu.createFooterMenu();

                Util.Style.setGrindviewStyle(ref  grdPharmacyChemical);

                DatabaseDataContext db = new DatabaseDataContext();
                loadCategories(db);
            }


            loadChemicalPharmacies();

        }
        private void parseGeneralArguments()
        {
            try
            {
                currentCategory = Request["category"];
            }
            catch
            {
                currentCategory = null;
            }

            if (currentCategory == null)
            {
                txtInputSubCategory.Enabled = false;
            }
            else
            {
                txtInputCategory.SelectedValue = currentCategory;
            }

            startWith = Request["startWith"];
            if (startWith == null)
            {
                startWith = "";
            }
            txtInputChemicalName.Text = startWith;


        }
        private void parsePharmacyArguments()
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
        }
        private void loadCategories(DatabaseDataContext db)
        {
            foreach (Category cat in db.Categories)
            {

                txtInputCategory.Items.Add(cat.categoryName.Trim());

            }

            if (currentCategory != null)
            {
                var query = (from subcat in db.SubCategories
                             where subcat.categoryName == currentCategory
                             select subcat.subCategoryName);
                txtInputSubCategory.DataSource = query;
                txtInputSubCategory.DataBind();
                txtInputCategory.SelectedValue = currentCategory;
            }
        }

        protected void btnPharmacyChemicalSearch_Click(object sender, EventArgs e)
        {
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
                btnPharmacyChemicalNext.HRef = Util.UrlRewriting.encodeUrl("createPharmacy.aspx?category=" + txtInputCategory.SelectedValue + "&startWith=" + startWith + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex + 1).ToString());
            }
            if (pharmacyChemicalResultIndex - 1 < 0)
            {
                btnPharmacyChemicalPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                btnPharmacyChemicalPrev.HRef = Util.UrlRewriting.encodeUrl("createPharmacy.aspx?category=" + txtInputCategory.SelectedValue + "&startWith=" + startWith + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex - 1).ToString());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();


            PharmacyChemical tmpitem = new PharmacyChemical();

            tmpitem.chemicalName = txtInputChemicalName.Text;

            tmpitem.subCategoryName = (from sub in db.SubCategories
                                       where sub.subCategoryName == txtInputSubCategory.SelectedItem.Text
                                       select sub.subCategoryName).First();




            db.PharmacyChemicals.InsertOnSubmit(tmpitem);
            db.SubmitChanges();
            Response.Redirect(Util.UrlRewriting.encodeUrl("updatePharmacy.aspx?chemicalName=" + tmpitem.chemicalName));

        }

        protected void txtInputCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            startWith = txtInputChemicalName.Text;

            Response.Redirect(Util.UrlRewriting.encodeUrl("createPharmacy.aspx?category=" + txtInputCategory.SelectedValue + "&startWith=" + startWith + "&pharmacyChemicalStartWith=" + pharmacyChemicalStartWith + "&pharmacyChemicalIndex=" + (pharmacyChemicalResultIndex).ToString()));
        }

        protected void grdPharmacyChemical_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdPharmacyChemical.Rows[i].Cells[0];
                TableCell cell1 = grdPharmacyChemical.Rows[i].Cells[1];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;

                Response.Redirect(Util.UrlRewriting.encodeUrl("deleteEntry.aspx?chemicalName=" + field0.Text));
            }
        }


    }
}
