using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhamacyDB
{
    public partial class createInteraction : System.Web.UI.Page
    {
        String InterChemicalName =null;
        String InterChemicalName2 = null;
        String InterSubCategoryName = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            parseArguments();
            if (!IsPostBack)
            {
                loadMenus();
            }
            
        }
        private void parseArguments()
        {
            InterChemicalName = Request["chemicalName"];

            if (InterChemicalName == null)
            {
                InterChemicalName = "";
            }
            InterChemicalName2 = Request["chemicalName2"];

            if (InterChemicalName2 == null)
            {
                InterChemicalName2 = "";
            }
            InterSubCategoryName = Request["subCategory"];

            if (InterSubCategoryName == null)
            {
                InterSubCategoryName = "";
            }
            txtInterP1.Text = InterChemicalName;
            if (InterSubCategoryName != "")
            {
                txtInterP2.Text = InterSubCategoryName;
                txtInterP2Name.Text = "Sub Category: ";
            }
            else if (InterChemicalName2 != null)
            {
                txtInterP2.Text = InterChemicalName2;
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }
        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createAdminMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            if (InterChemicalName2 != "")
            {
                InteractionPharmacyPharmacy tmpItem = new InteractionPharmacyPharmacy();
                tmpItem.pharmacyname1 = InterChemicalName;
                tmpItem.pharmacyname2 = InterChemicalName2;
                tmpItem.comment = txtInterComment.Text;
                db.InteractionPharmacyPharmacies.InsertOnSubmit(tmpItem);
            }
            else if (InterSubCategoryName != null)
            {
                InteractionPharmacySubCategory tmpItem = new InteractionPharmacySubCategory();
                tmpItem.chemicalName = InterChemicalName;
                tmpItem.subCategoryName = InterSubCategoryName;
                tmpItem.comment = txtInterComment.Text;
                db.InteractionPharmacySubCategories.InsertOnSubmit(tmpItem);
            }
            db.SubmitChanges();
            Response.Redirect(Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName="+InterChemicalName));

        }
    }
}
