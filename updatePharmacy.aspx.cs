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
    public partial class updatePharmacy : System.Web.UI.Page
    {
        String chemicalName;
        String category;
        String subCategory;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            parseArguments();
            if (!IsPostBack)
            {
                loadMenus();
                
                Util.Style.setGrindviewStyle(ref  grdCommercials);
                loadPharmacy();
                loadCommercials();
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
              
                grdCommercials.DataSource = query;
                grdCommercials.DataBind();

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

            }
            catch
            {
                //TODO : ektipioosi sostou lathous
                Response.Write("Error");
            }
        }
   
        private void loadPharmacy()
        {
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            try
            {
                h1PharmacyName.InnerText = "Update Pharmacy " + chemicalName;
                txtChemicalName.Text = chemicalName;
                txtInputIndicia.Text = (from phar in db.PharmacyChemicals
                                      where phar.chemicalName == chemicalName
                                      select phar.indication).First();
            }
            catch
            {
                txtInputIndicia.Text = "";
            }
            try
            {
                txtGreekname.Text = (from phar in db.PharmacyChemicals
                                             where phar.chemicalName == chemicalName
                                     select phar.greekName).First().Trim();
            }
            catch
            {
                txtGreekname.Text = "";
            }
            try
            {
                txtUndesirableReactions.Text = (from phar in db.PharmacyChemicals
                                               where phar.chemicalName == chemicalName
                                                select phar.undesirableReactions).First().Trim();
            }
            catch
            {
                txtUndesirableReactions.Text = "";
            }
            try
            {
                txtInputDoce.Text = (from phar in db.PharmacyChemicals
                                where phar.chemicalName == chemicalName
                                     select phar.dose).First().Trim();
            }
            catch
            {
                txtInputDoce.Text = "";
            }
            try
            {
                txtInteractions.Text = (from phar in db.PharmacyChemicals
                                     where phar.chemicalName == chemicalName
                                     select phar.interactionGeneral).First().Trim();
            }
            catch
            {
                txtGreekname.Text = "";
            }
            try
            {
                txtInputNonIndicia.Text = (from phar in db.PharmacyChemicals
                                            where phar.chemicalName == chemicalName
                                           select phar.contraIndication).First().Trim();
            }
            catch
            {
                txtInputNonIndicia.Text = "";
            }
            try
            {

                category = (from phar in db.PharmacyChemicals
                            where phar.chemicalName == chemicalName
                            select phar.SubCategory.categoryName).First().Trim();
                txtCategory.Text = category;
               
            }
            catch
            {
                txtCategory.Text = "";
            }
            try
            {

                subCategory = (from phar in db.PharmacyChemicals
                            where phar.chemicalName == chemicalName
                               select phar.subCategoryName).First().Trim();
                txtSubCategory.Text = subCategory;

            }
            catch
            {
                txtSubCategory.Text = "";
            }
        }
        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createAdminMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }

        private void parseArguments()
        {
            
            chemicalName = Request["chemicalName"];
            if (chemicalName == null)
            {
                Response.Redirect("createPharmacy.aspx");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            PharmacyChemical tmpItem;
            PharmacyChemical OriginalItem;
            tmpItem = (from phar in db.PharmacyChemicals
             where phar.chemicalName == chemicalName
                       select phar).FirstOrDefault();
            OriginalItem = (from phar in db.PharmacyChemicals
                            where phar.chemicalName == chemicalName
                            select phar).FirstOrDefault();
            tmpItem.indication = txtInputIndicia.Text;
            tmpItem.contraIndication = txtInputNonIndicia.Text;
            tmpItem.dose = txtInputDoce.Text;
            tmpItem.greekName = txtGreekname.Text;
            tmpItem.interactionGeneral = txtInteractions.Text;
            tmpItem.undesirableReactions = txtUndesirableReactions.Text;
            
         //   db.PharmacyChemicals.Attach(tmpItem, OriginalItem);   
            db.SubmitChanges();
            
        }
       

        protected void btnPharmacyCommercialAdd_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            PharmacyCommercial newItem = new PharmacyCommercial();
            newItem.chemicalName = chemicalName;
            newItem.company = txtPharmacyCompany.Text;
            newItem.morph = txtPharmacyMorph.Text;
            newItem.name = txtPharmacyName.Text;
            db.PharmacyCommercials.InsertOnSubmit(newItem);
            db.SubmitChanges();
            Response.Redirect(Util.UrlRewriting.encodeUrl("updatePharmacy.aspx?chemicalName=" + chemicalName ));

        }

        protected void btnUpdateInteractions_Click(object sender, EventArgs e)
        {
            Response.Redirect(Util.UrlRewriting.encodeUrl("updateInteraction.aspx?chemicalName=" + chemicalName));
        }

        protected void grdCommercials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdCommercials.Rows[i].Cells[0];
                TableCell cell1 = grdCommercials.Rows[i].Cells[1];
                TableCell cell2 = grdCommercials.Rows[i].Cells[2];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                HyperLink field1 = cell1.Controls[0] as HyperLink;
                HyperLink field2 = cell2.Controls[0] as HyperLink;

                Response.Redirect(Util.UrlRewriting.encodeUrl("deleteEntry.aspx?name=" + field0.Text + "&company=" + field1.Text + "&morph=" + field2.Text));
            }
        }
    }
}
