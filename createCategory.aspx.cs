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
    public partial class createCategory : System.Web.UI.Page
    {

        String categoryStartWith = "";
        public int resultCategoryIndex = 0;
        public int maxCategoryIndex = 0;
        String subCategoryStartWith = "";
        public int resultSubCategoryIndex = 0;
        public int maxSubCategoryIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            pareseArguments();
            if (!IsPostBack)
            {
                menu.InnerHtml += Util.Menu.createAdminMenu(Request.Url.LocalPath);
                footerMenu.InnerHtml += Util.Menu.createFooterMenu();
                Util.Style.setGrindviewStyle(ref grdCategory);
                Util.Style.setGrindviewStyle(ref grdSubCategory);

                loadCategory();
                loadSubCategory();
            }

        }

        private void pareseArguments()
        {
            try
            {
                resultCategoryIndex = int.Parse(Request["categoryIndex"]);
            }
            catch
            {
                resultCategoryIndex = 0;
            }
            categoryStartWith = Request["categoryStartWith"];
            if (categoryStartWith == null)
            {
                categoryStartWith = "";
            }
            txtCategoryFilter.Text = categoryStartWith;
            try
            {
                resultSubCategoryIndex = int.Parse(Request["subCategoryIndex"]);
            }
            catch
            {
                resultSubCategoryIndex = 0;
            }
            subCategoryStartWith = Request["subCategoryStartWith"];
            if (subCategoryStartWith == null)
            {
                subCategoryStartWith = "";
            }
            txtSubCategoryFilter.Text = subCategoryStartWith;
        }
        private void loadCategory()
        {
            DatabaseDataContext db = new DatabaseDataContext();
            foreach (Category cat in db.Categories)
            {

                lstCategory.Items.Add(cat.categoryName.Trim());

            }

            //Delete Categories



            var query = from cat in db.Categories
                        where cat.categoryName.StartsWith(categoryStartWith)
                        select new
                        {
                            category = cat.categoryName
                        };
            maxCategoryIndex = query.Count() / Util.resultSize;
            grdCategory.DataSource = query.Skip(resultCategoryIndex * Util.resultSize).Take(Util.resultSize);
            grdCategory.DataBind();

            int start = resultCategoryIndex * Util.resultSize;
            txtCategoryIndex.Text = (start + 1).ToString() + "-" + (start + grdCategory.Rows.Count).ToString() + " of " + query.Count();

           

            if (resultCategoryIndex + 1 > maxCategoryIndex)
            {
                btnCategoryNext.Disabled = true;
                //  btnNext.Visible = false;
            }
            else
            {
                btnCategoryNext.HRef = Util.UrlRewriting.encodeUrl("createCategory.aspx?categoryStartWith=" + categoryStartWith + "&categoryIndex=" + (resultCategoryIndex + 1).ToString() + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (resultSubCategoryIndex).ToString());

            }
            if (resultCategoryIndex - 1 < 0)
            {
                btnCategoryPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                btnCategoryPrev.HRef = Util.UrlRewriting.encodeUrl("createCategory.aspx?categoryStartWith=" + categoryStartWith + "&categoryIndex=" + (resultCategoryIndex - 1).ToString() + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (resultSubCategoryIndex).ToString());
            }
        }

        private void loadSubCategory()
        {

            DatabaseDataContext db = new DatabaseDataContext();
            var query = from cat in db.SubCategories
                        where cat.subCategoryName.StartsWith(subCategoryStartWith)
                        select new
                        {
                            subCategory = cat.subCategoryName,
                            category = cat.categoryName
                        };
            maxSubCategoryIndex = query.Count() / Util.resultSize;
            grdSubCategory.DataSource = query.Skip(resultSubCategoryIndex * Util.resultSize).Take(Util.resultSize);
            grdSubCategory.DataBind();

            int start = resultSubCategoryIndex * Util.resultSize;
            txtSubCategoryIndex.Text = (start + 1).ToString() + "-" + (start + grdSubCategory.Rows.Count).ToString() + " of " + query.Count();

 

            if (resultSubCategoryIndex + 1 > maxSubCategoryIndex)
            {
                btnSubCategoryNext.Disabled = true;
                //  btnNext.Visible = false;
            }
            else
            {
                btnSubCategoryNext.HRef = Util.UrlRewriting.encodeUrl("createCategory.aspx?categoryStartWith=" + categoryStartWith + "&categoryIndex=" + (resultCategoryIndex).ToString() + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (resultSubCategoryIndex + 1).ToString());

            }
            if (resultSubCategoryIndex - 1 < 0)
            {
                btnSubCategoryPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                btnSubCategoryPrev.HRef = Util.UrlRewriting.encodeUrl("createCategory.aspx?categoryStartWith=" + categoryStartWith + "&categoryIndex=" + (resultCategoryIndex).ToString() + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (resultSubCategoryIndex - 1).ToString());
            }
        }
       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();


            Category tmpitem = new Category();
            tmpitem.categoryName = txtInputName.Text;

            db.Categories.InsertOnSubmit(tmpitem);
            db.SubmitChanges();
            Response.Redirect(Util.UrlRewriting.encodeUrl("createCategory.aspx"));
        }


        protected void btnSubCategorySubmit_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();


            SubCategory tmpitem = new SubCategory();
            tmpitem.categoryName = lstCategory.SelectedValue;
            tmpitem.subCategoryName = txtSubCategoryName.Text;
            db.SubCategories.InsertOnSubmit(tmpitem);
            db.SubmitChanges();
            Response.Redirect(Util.UrlRewriting.encodeUrl("createCategory.aspx"));
        }
        protected void btnCategorySearch_Click(object sender, EventArgs e)
        {
            categoryStartWith = txtCategoryFilter.Text;
            Response.Redirect(Util.UrlRewriting.encodeUrl("createCategory.aspx?categoryStartWith=" + categoryStartWith + "&categoryIndex=" + (resultCategoryIndex).ToString() + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (resultSubCategoryIndex).ToString()));

        }
        protected void btnSubCategorySearch_Click(object sender, EventArgs e)
        {
            subCategoryStartWith = txtSubCategoryFilter.Text;
            Response.Redirect(Util.UrlRewriting.encodeUrl("createCategory.aspx?categoryStartWith=" + categoryStartWith + "&categoryIndex=" + (resultCategoryIndex).ToString() + "&subCategoryStartWith=" + subCategoryStartWith + "&subCategoryIndex=" + (resultSubCategoryIndex).ToString()));

        }

        protected void grdSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdSubCategory.Rows[i].Cells[0];
                HyperLink field0 = cell0.Controls[0] as HyperLink;

                Response.Redirect(Util.UrlRewriting.encodeUrl("deleteEntry.aspx?subCategoryName=" + field0.Text));
            }

        }

        protected void grdCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteRow")
            {
                int i = Convert.ToInt32(e.CommandArgument);
                TableCell cell0 = grdCategory.Rows[i].Cells[0];
                HyperLink field0 = cell0.Controls[0] as HyperLink;

                Response.Redirect(Util.UrlRewriting.encodeUrl("deleteEntry.aspx?categoryName=" + field0.Text));
            }
        }

        
    }
}
