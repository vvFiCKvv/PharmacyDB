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
    public partial class viewCategory : System.Web.UI.Page
    {
        String name;
        String startWith = "";
        public int resultIndex = 0;
        public int maxIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            parseArguments();
            
            loadCategory();
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

        private void parseArguments()
        {
            try
            {
                resultIndex = int.Parse(Request["index"]);
            }
            catch
            {
                resultIndex = 0;
            }
            name = Request["name"];
            if (name == null)
            {
                Response.Redirect("viewCategories.aspx");
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

        private void loadCategory()
        {
            h1CategoryName.InnerText =  name;
            DatabaseDataContext db = new DatabaseDataContext(); db.ObjectTrackingEnabled = false;
            if (startWith != "")
            {
                var query = from phar in db.PharmacyChemicals
                            where (phar.SubCategory.Category.categoryName == name && phar.chemicalName.StartsWith(startWith))
                        orderby phar.chemicalName
                        select new
                        {
                            // name = phar.name,
                            chemicalName = phar.chemicalName.Trim()
                        };
                maxIndex = query.Count() / Util.resultSize;
                GridView1.DataSource = query.Skip(resultIndex * Util.resultSize).Take(Util.resultSize);
                GridView1.DataBind();

                int start = resultIndex * Util.resultSize;
                txtIndex.Text = (start + 1).ToString() + "-" + (start + GridView1.Rows.Count).ToString() + " of " + query.Count();

            }
            else
            {
                var query = from phar in db.PharmacyChemicals
                            where phar.SubCategory.Category.categoryName == name
                        orderby phar.chemicalName
                        select new
                        {
                            // name = phar.name,
                            chemicalName = phar.chemicalName.Trim()
                        };
                maxIndex = query.Count() / Util.resultSize;
                GridView1.DataSource = query.Skip(resultIndex * Util.resultSize).Take(Util.resultSize);
                GridView1.DataBind();

                int start = resultIndex * Util.resultSize;
                txtIndex.Text = (start + 1).ToString() + "-" + (start + GridView1.Rows.Count).ToString() + " of " + query.Count();

            }
         
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TableCell cell0 = GridView1.Rows[i].Cells[0];
                //TableCell cell1 = GridView1.Rows[i].Cells[1];
                HyperLink field0 = cell0.Controls[0] as HyperLink;
                //HyperLink field1 = cell1.Controls[0] as HyperLink;
                field0.NavigateUrl = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + field0.Text);
                //field1.NavigateUrl = Util.UrlRewriting.encodeUrl("viewPharmacy.aspx?chemicalName=" + field1.Text);
            }

            if (resultIndex + 1 > maxIndex)
            {
                btnNext.Disabled = true;
                //  btnNext.Visible = false;
            }
            else
            {
                btnNext.HRef = Util.UrlRewriting.encodeUrl("viewCategory.aspx?startWith=" + startWith + "&name=" + name + "&index=" + (resultIndex +1).ToString());
            
            }
            if (resultIndex - 1 < 0)
            {
                btnPrev.Disabled = true;
                // btnPrev.Visible = false;
            }
            else
            {
                btnPrev.HRef = Util.UrlRewriting.encodeUrl("viewCategory.aspx?startWith=" + startWith + "&name=" + name + "&index=" + (resultIndex -1).ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            resultIndex = 0;
            startWith = txtFilter.Text;

            Response.Redirect(Util.UrlRewriting.encodeUrl("viewCategory.aspx?startWith=" + startWith +"&name=" + name + "&index=" + (resultIndex).ToString()));
        }

       
    }
}
