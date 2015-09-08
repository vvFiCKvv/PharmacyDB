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
    public partial class viewCategories : System.Web.UI.Page
    {
        public int resultIndex = 0;
        String startWith = "";
        public int maxIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            pareseArguments();
            loadCategories();
            if (!this.IsPostBack)
            {
                loadMenus();
                Util.Style.setGrindviewStyle(ref GridView1);
                               
                txtFilter.Text = startWith;
                txtFilter.Focus();
            }
        }

        private void loadMenus()
        {
            menu.InnerHtml += Util.Menu.createMenu(Request.Url.LocalPath);
            footerMenu.InnerHtml += Util.Menu.createFooterMenu();
        }

        private void pareseArguments()
        {
            try
            {
                resultIndex = int.Parse(Request["index"]);
            }
            catch
            {
                resultIndex = 0;
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
        private void loadCategories()
        {
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            IOrderedQueryable<PhamacyDB.Category> query;
            if (startWith != "")
            {
                query = from cat in db.Categories
                        where cat.categoryName.StartsWith(startWith)
                        orderby cat.categoryName
                        select cat;
            }
            else
            {
                query = from cat in db.Categories
                        orderby cat.categoryName
                        select cat;
            }
            maxIndex = query.Count() / Util.resultSize;
            GridView1.DataSource = query.Skip(resultIndex * Util.resultSize).Take(Util.resultSize);
            GridView1.DataBind();

            int start = resultIndex * Util.resultSize;
            txtIndex.Text = (start+1).ToString() + "-" + (start + GridView1.Rows.Count).ToString() + " of " + query.Count();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TableCell cell0 = GridView1.Rows[i].Cells[0];
               
                HyperLink field0 = cell0.Controls[0] as HyperLink;

                field0.NavigateUrl = Util.UrlRewriting.encodeUrl("viewCategory.aspx?name=" + field0.Text);

            }

            if (resultIndex + 1 > maxIndex)
            {
                btnNext.Disabled = true;
                //  btnNext.Visible = false;
            }
            else
            {
                btnNext.HRef = Util.UrlRewriting.encodeUrl("viewCategories.aspx?startWith=" + startWith + "&index=" + (resultIndex + 1).ToString());

            }
            if (resultIndex - 1 < 0)
            {
                btnPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                btnPrev.HRef = Util.UrlRewriting.encodeUrl("viewCategories.aspx?startWith=" + startWith + "&index=" + (resultIndex -1).ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            resultIndex = 0;
            startWith = txtFilter.Text;
            
            Response.Redirect(Util.UrlRewriting.encodeUrl("viewCategories.aspx?startWith=" + startWith + "&index=" + (resultIndex).ToString()));
        }
       
           
    }
}
